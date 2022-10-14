
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PYP_Book.Application.Common.Interfaces;
using PYP_Book.Infrastructure.Common.Repositories;
using PYP_Book.Infrastructure.Data;
using PYP_Book.Infrastructure.Services;
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

            //serviceCollection.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            //serviceCollection.AddScoped<ApplicationDbContextInitialiser>();

            //serviceCollection
            //     .AddDefaultIdentity<ApplicationUser>()
            //     .AddRoles<IdentityRole>()
            //     .AddEntityFrameworkStores<ApplicationDbContext>();

            //serviceCollection.AddIdentityServer()
            //    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            service.AddTransient<ICategoryRepository, CategoryRepository>();
            service.AddTransient<IBookRepository, BookRepository>();
            service.AddTransient<IDiscountRepository, DiscountRepository>();
            service.AddTransient<IAuthorRepository, AuthorRepository>();
            service.AddTransient<IUnitOfWork, UnitOFWork>();
            service.AddTransient<IFileUploadService, FileUploadService>();

            //service.AddAuthentication()
            //    .AddIdentityServerJwt();

            return service;
        }
    }
}
