using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
namespace BP.Validations
{
    public class ContactValidator:AbstractValidator<Models.ContactDto>
    {
        public ContactValidator()
        {
            RuleFor(x=>x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .GreaterThan(100).WithMessage("Id must be greater than 100.");
        }
    }
}