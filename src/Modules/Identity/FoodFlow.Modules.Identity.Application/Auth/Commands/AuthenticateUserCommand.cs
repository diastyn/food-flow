using FoodFlow.BuildingBlocks.Results;
using FoodFlow.Modules.Identity.Domain.Auth.Contracts;
using MediatR;

namespace FoodFlow.Modules.Identity.Application.Auth.Commands;

/// <summary>
/// Команда аутентификации пользователя по логину и паролю.
/// При успехе возвращает пару токенов: access + refresh.
/// </summary>
/// <param name="Username">Имя пользователя (логин).</param>
/// <param name="Password">Пароль в открытом виде.</param>
public sealed record AuthenticateUserCommand(
    string Username,
    string Password) : IRequest<Result<AuthToken>>;
