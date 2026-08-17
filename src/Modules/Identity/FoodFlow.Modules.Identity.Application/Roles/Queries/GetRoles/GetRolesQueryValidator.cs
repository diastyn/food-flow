using FluentValidation;

namespace FoodFlow.Modules.Identity.Application.Roles.Queries.GetRoles;

public sealed class GetRolesQueryValidator : AbstractValidator<GetRolesQuery>
{
    public GetRolesQueryValidator()
    {
        _ = RuleFor(q => q.Page)
            .GreaterThan(0)
            .WithMessage("Page must be greater than 0.");

        _ = RuleFor(q => q.PageSize)
            .GreaterThan(0)
            .WithMessage("PageSize must be greater than 0.");
    }
}
