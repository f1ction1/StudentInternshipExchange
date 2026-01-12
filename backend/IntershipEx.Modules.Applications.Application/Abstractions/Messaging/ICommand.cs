using Modules.Common.Domain.Abstractions;
using MediatR;

namespace IntershipEx.Modules.Applications.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}

public interface IBaseCommand // for logging purposes
{

}