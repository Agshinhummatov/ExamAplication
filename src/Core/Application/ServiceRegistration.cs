using Application.Mapping;
using FluentValidation.AspNetCore;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Application;

public static class ServiceRegistration
{
    public static void AddApplicationService(this IServiceCollection services, IConfiguration confiq)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<Application.Validators.LessonCreateAndUpdateDtoValidator>();

        services.AddValidatorsFromAssemblyContaining<Application.Validators.StudentCreateAndUpdateDtoValidator>();

       
        services.AddValidatorsFromAssemblyContaining<Application.Validators.ExamCreateAndUpdateDtoValidator>();
    }
}
