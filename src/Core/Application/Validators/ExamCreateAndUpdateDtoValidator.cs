using Application.DTOs.Exam;
using Application.Features.Commands.Exam;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class ExamCreateAndUpdateDtoValidator : AbstractValidator<ExamCreateAndUpdateDto>
    {
        public ExamCreateAndUpdateDtoValidator()
        {
            RuleFor(x => x.LessonCode)
             .NotEmpty().WithMessage("Lesson code is required.")
             .MaximumLength(3).WithMessage("Lesson code must be at most 3 characters long.");

            RuleFor(x => x.StudentNumber)
                .NotEmpty().WithMessage("Student number is required.")
                .InclusiveBetween(1, 99999).WithMessage("Student number must be between 1 and 99999.");

            RuleFor(x => x.ExamDate)
                .NotEmpty().WithMessage("Exam date is required.")
                .GreaterThanOrEqualTo(DateTime.Today.AddYears(-1))
                .WithMessage("Exam date cannot be too old.");

            RuleFor(x => x.Grade)
                .InclusiveBetween(0, 9).WithMessage("Grade must be between 0 and 9.");

            RuleFor(x => x.LessonId)
                .GreaterThan(0).WithMessage("Lesson must be selected.");
        }
    }
}
