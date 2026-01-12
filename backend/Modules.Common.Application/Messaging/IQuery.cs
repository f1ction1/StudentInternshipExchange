using Modules.Common.Domain.Abstractions;
using MediatR;

namespace Modules.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
