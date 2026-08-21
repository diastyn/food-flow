using MediatR;

namespace FoodFlow.BuildingBlocks.Application.Queries;

public interface IQueryRequest<out TResponse> : IRequest<TResponse>;
