using Expensify.API.DTOs.AuthDTOs;
using FluentValidation;

namespace Expensify.API.Utility.Validators.Filters.Auth;

public class LoginUserDTOValidator : AbstractValidator<LoginUserDTO>
{
    public LoginUserDTOValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();

        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
    }
}
