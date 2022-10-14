using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PYP_Book.Application.Common.Behaviors;
using System.Reflection;

namespace PYP_Book.Application.ServiceRegistration;
public static class ConfigurationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection service)
    {
        service.AddMediatR(Assembly.GetExecutingAssembly());
        service.AddAutoMapper(Assembly.GetExecutingAssembly());
        service.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        service.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        return service;
    }
}