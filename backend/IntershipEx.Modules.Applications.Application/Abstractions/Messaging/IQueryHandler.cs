using Modules.Common.Domain.Abstractions;
using MediatR;

namespace IntershipEx.Modules.Applications.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
