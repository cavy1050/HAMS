using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace HAMS.Frame.Control.Login.Models
{
    public class LoginContentModelValidator:AbstractValidator<LoginContentModel>
    {
        public LoginContentModelValidator()
        {
            RuleFor(info => info.Account).NotEmpty().WithMessage("账户不能为空!");
            RuleFor(info => info.Password).NotEmpty().WithMessage("密码不能为空!");
        }
    }
}
