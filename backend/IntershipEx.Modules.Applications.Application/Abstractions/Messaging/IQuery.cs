using Modules.Common.Domain.Abstractions;
using MediatR;

namespace IntershipEx.Modules.Applications.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
