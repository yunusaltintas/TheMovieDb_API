using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDB.Data.DTOs.Valitador
{
    public class UserDtoValidator:AbstractValidator<UserDTO>
    {
        public UserDtoValidator()
        {
            RuleFor(o => o.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(o => o.Password).NotEmpty().NotNull();
        }
    }
}
