using Application.DTOs.Student;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class StudentCreateAndUpdateDtoValidator : AbstractValidator<StudentCreateAndUpdateDto>
    {
        public StudentCreateAndUpdateDtoValidator()
        {
            RuleFor(x => x.StudentNumber)
             .InclusiveBetween(0, 99999) 
             .WithMessage("Student number must be a 5-digit positive number.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(30).WithMessage("First name cannot be longer than 30 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(30).WithMessage("Last name cannot be longer than 30 characters.");

            RuleFor(x => x.Class)
                .InclusiveBetween(1, 99) 
                .WithMessage("Class must be a positive number between 1 and 99.");
        }
    }
    
}
