using MediatR;

namespace FoodFlow.BuildingBlocks.Application.Commands;

public interface ICommandRequest<out TResponse> : IRequest<TResponse>;
