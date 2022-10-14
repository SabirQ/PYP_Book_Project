
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Application.Common.Interfaces.Repositories;
using PYP_Book.Application.Common.Interfaces.Services;
using PYP_Book.Infrastructure.Common;
using PYP_Book.Infrastructure.Common.Repositories;
using PYP_Book.Infrastructure.Common.Services;
using PYP_Book.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Book.Infrastructure.ServiceRegistration
{
    public static class ConfigurationServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection service, IConfiguration configuration)
        {
            //serviceCollection.AddScoped<AuditableEntitySaveChangesInterceptor>();
            service.AddDbContext<AppDbContext>(options =>
                   options.UseSqlServer(configuration.GetConnectionString("Default"),
                       builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            service.AddTransient<ICategoryRepository, CategoryRepository>();
            service.AddTransient<IBookRepository, BookRepository>();
            service.AddTransient<IDiscountRepository, DiscountRepository>();
            service.AddTransient<IAuthorRepository, AuthorRepository>();
            service.AddTransient < ILanguageRepository,LanguageRepository>();
            service.AddTransient < IFormatRepository ,FormatRepository>();
            service.AddTransient < ICommentRepository ,CommentRepository>();
            service.AddTransient<ISettingRepository, SettingRepository>();

            service.AddTransient<IFileUploadService, FileUploadService>();
            service.AddTransient<IIdentityService, IdentityService>();

            service.AddTransient<IUnitOfWork, UnitOFWork>();

            return service;
        }
    }
}
