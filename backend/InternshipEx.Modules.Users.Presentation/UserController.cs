using InternshipEx.Modules.Users.Application.UseCases.UpsertProfile;
using InternshipEx.Modules.Users.Application.UseCases.UpsertStudentData;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Common.Infrastructure.Services;
using InternshipEx.Modules.Users.Application.UseCases.GetProfile;
using InternshipEx.Modules.Users.Application.UseCases.GetStudent;
using Microsoft.AspNetCore.Http;
using InternshipEx.Modules.Users.Application.UseCases.UploadCv;
using InternshipEx.Modules.Users.Application.UseCases.GetCvBase64;
using InternshipEx.Modules.Users.Application.UseCases.CompleteEmployerProfile;
using InternshipEx.Modules.Users.Application.UseCases.GetEmployer;
using InternshipEx.Modules.Users.Domain.DTOs;
using InternshipEx.Modules.Users.Application.UseCases.EditEmployer;
using InternshipEx.Modules.Users.Application.UseCases.IsCompleteEmployerProfile;
using InternshipEx.Modules.Users.Application.UseCases.IsCompleteProfile;

namespace InternshipEx.Modules.Users.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("upsert-profile")]
        [Authorize(Roles = "student, employer")]
        public async Task<IActionResult> UpsertProfile([FromBody] UpsertProfileCommand command, CancellationToken cancellationToken)
        {
            await _sender.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpPost("upsert-student")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> UpsertStudent([FromBody] UpsertStudentDataCommand command, CancellationToken cancellationToken)
        {
            await _sender.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpGet("profile")]
        [Authorize(Roles = "student, employer")]
        public async Task<IActionResult> GetProfile()
        {
            var result = await _sender.Send(new GetProfileQuery());
            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("profile/is-complete")]
        [Authorize(Roles = "student, employer")]
        public async Task<IActionResult> IsCompleteProfile()
        {
            var result = await _sender.Send(new IsCompleteProfileQuery());
            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet("student")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> GetStudent()
        {
            var result = await _sender.Send(new GetStudentQuery());
            if(result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPost("upload-cv")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> UploadCv(IFormFile cv)
        {
            byte[] cvFile = await ConvertFormFileService.ConvertToByteArrAsync(cv);
            var command = new UploadCvCommand(cv.ContentType, cvFile);
            await _sender.Send(command);
            return NoContent();
        }

        [HttpGet("student/cv")]
        [Authorize(Roles = "student")]
        public async Task<IActionResult> GetCv()
        {
            var cvBase64 = await _sender.Send(new GetCvQuery());
            if (cvBase64 == null)
            {
                return NoContent();
            }
            return Ok(new { cvFile = "data:application/pdf;base64," + cvBase64 });
        }

        [HttpGet("student/cv/{studentId}")]
        [Authorize(Roles = "employer")]
        public async Task<IActionResult> GetCv(Guid studentId)
        {
            var cvBase64 = await _sender.Send(new GetCvQuery(studentId));
            if (cvBase64 == null)
            {
                return NoContent();
            }
            return Ok(new { cvFile = "data:application/pdf;base64," + cvBase64 });
        }

        [HttpPost("upsert-employer-profile")]
        [Authorize(Roles = "employer")]
        public async Task<IActionResult> UpsertEmployerProfile([FromBody] UpsertEmployerProfileCommand command, CancellationToken cancellationToken)
        {
            await _sender.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpGet("employer")]
        [Authorize(Roles = "employer")]
        public async Task<IActionResult> GetEmployer()
        {
            var employer = await _sender.Send(new GetEmployerQuery());
            return Ok(employer);
        }

        [HttpPut("employer/edit")]
        [Authorize(Roles = "employer")]
        public async Task<IActionResult> EditEmployer([FromBody] EmployerDto employerDto)
        {
            await _sender.Send(new EditEmployerCommand(employerDto));
            return NoContent();
        }

        [HttpGet("employer/{id}/is-complete")] // should be deleted, beacause of deprecation - new version uses the logged in user id
        [Authorize(Roles = "employer")]
        public async Task<IActionResult> IsCompleteEmployerProfile(Guid id)
        {
            var isComplete = await _sender.Send(new IsCompleteEmployerProfileCommand(id));
            return Ok(new { isComplete });
        }
    }
}
