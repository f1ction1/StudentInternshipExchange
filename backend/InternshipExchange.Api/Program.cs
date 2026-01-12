using Humanizer;
using InternshipEx.Modules.Auth.Application.Events.Domain;
using InternshipEx.Modules.Auth.Infrastructure;
using InternshipEx.Modules.Practices.Application.Events;
using InternshipEx.Modules.Practices.Application.UseCases.Dictionaries.GetAllInternshipDictionaries;
using InternshipEx.Modules.Practices.Endpoints;
using InternshipEx.Modules.Practices.Infrastructure;
using InternshipEx.Modules.Users.Application.Events;
using InternshipEx.Modules.Users.Endpoints;
using InternshipEx.Modules.Users.Infrastructure;
using InternshipEx.Recommendations.Infrastructure;
using InternshipEx.Recommendations.Infrastructure.EventConsumers;
using InternshipExchange.Api.ModelBinders.CommaSeparated;
using InternshipExchange.Api.UseCases.GetInternshipApplicants;
using Modules.Common.Application;
using Modules.Common.Infrastructure.Middleware;
using Modules.Common.Infrastructure.Services;
using IntershipEx.Modules.Applications.Application.Events;
using IntershipEx.Modules.Applications.Infrastructure;
using IntershipEx.Modules.Applications.Infrastructure.Controllers.Application;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;

//force to a specific language (for Humanizer)
var cultureInfo = new CultureInfo("en");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // your React dev server
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // if you need cookies / auth headers
        });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

//Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field. Example: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9'",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthModule(builder.Configuration);
builder.Services.AddUserModule(builder.Configuration);
builder.Services.AddApplicationModule(builder.Configuration);
builder.Services.AddPracticesModule(builder.Configuration);
builder.Services.AddRecommendationModule(builder.Configuration);

builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new CommaSeparatedModelBinderProvider());
});
builder.Services.AddControllers().
                AddApplicationPart(typeof(UserController).Assembly); // cant connect inside AddUserModule
builder.Services.AddControllers().
                AddApplicationPart(typeof(EmployerInternshipController).Assembly); // cant connect inside AddInternshipModule
builder.Services.AddControllers().
                AddApplicationPart(typeof(ApplicationController).Assembly); // cant connect inside AddApplicationModule

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.AddConsumers(typeof(UserRegisteredDomaintEventConsumer).Assembly); // Add Consumers from Auth Module
    busConfigurator.AddConsumers(typeof(UserRegisteredIntegrationEventConsumer).Assembly); // Add Consumers from Users Module
    busConfigurator.AddConsumers(typeof(InternshipAddedToFavoriteConsumer).Assembly); // Add Consumers from Practices Module
    busConfigurator.AddConsumers(typeof(InternshipAddedToFavoriteIntegrationEventConsumer).Assembly); // Add Consumers from Recommendation Module
    busConfigurator.AddConsumers(typeof(ApplicationAppliedDomainEventConsumer).Assembly); // Add Consumers from Application Module
    busConfigurator.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

//JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetValue<string>("AppSettings:Issuer"),
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetValue<string>("AppSettings:Audience"),
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("AppSettings:Token")!))
    };
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
//orchestration
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetInternshipApplicantsQuery).Assembly));

var app = builder.Build();

//orchestration
app.MapGet("/api/orchestration/applicants/{internshipId}", async (Guid internshipId, ISender sender) =>
{
    var result = await sender.Send(new GetInternshipApplicantsQuery(internshipId));
    if(result.IsFailure)
    {
        return Results.BadRequest(result.Error);
    }
    return Results.Ok(result.Value);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
