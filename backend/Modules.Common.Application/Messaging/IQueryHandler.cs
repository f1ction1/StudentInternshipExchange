using Modules.Common.Domain.Abstractions;
using MediatR;

namespace Modules.Common.Application.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
