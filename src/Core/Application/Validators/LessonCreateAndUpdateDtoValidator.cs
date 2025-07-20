using Application.DTOs.Lesson;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class LessonCreateAndUpdateDtoValidator : AbstractValidator<LessonCreateAndUpdateDto>
    {
        public LessonCreateAndUpdateDtoValidator()
        {
           
            RuleFor(x => x.LessonCode)
                .NotEmpty().WithMessage("Lesson code is required.")
                .MaximumLength(3).WithMessage("Lesson code must be at most 3 characters long.");

        
            RuleFor(x => x.LessonName)
                .NotEmpty().WithMessage("Lesson name is required.")
                .MaximumLength(30).WithMessage("Lesson name must be at most 30 characters long.");

            
            RuleFor(x => x.Class)
                .InclusiveBetween(1, 9).WithMessage("Class must be between 1 and 9.");

          
            RuleFor(x => x.TeacherFirstName)
                .NotEmpty().WithMessage("Teacher's first name is required.")
                .MaximumLength(20).WithMessage("Teacher's first name must be at most 20 characters long.");

           
            RuleFor(x => x.TeacherLastName)
                .NotEmpty().WithMessage("Teacher's last name is required.")
                .MaximumLength(20).WithMessage("Teacher's last name must be at most 20 characters long.");
        }
    }
}

