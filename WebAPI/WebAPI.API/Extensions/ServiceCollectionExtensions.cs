using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using WebAPI.API.Converters;
using WebAPI.API.Converters.Interfaces;
using WebAPI.API.Models.Documents;
using WebAPI.Business.Contracts;
using WebAPI.Business.Converters;
using WebAPI.Business.Services;
using WebAPI.Data;
using WebAPI.Data.Contracts;
using WebAPI.Data.Repositories;

namespace WebAPI.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRegistrations(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddDataRegistrations();

            serviceCollection
                .AddBusinessRegistrations();

            serviceCollection
                .AddApiRegistrations();
        }

        private static void AddDataRegistrations(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddScoped<DbContext, ApplicationContext>();

            serviceCollection
                .AddScoped<IUnitOfWork, UnitOfWork>();

            serviceCollection
                .AddScoped<IUserRepository, UserRepository>();

            serviceCollection
                .AddScoped<ICompanyRepository, CompanyRepository>();
        }

        private static void AddBusinessRegistrations(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddScoped<IUserService, UserService>();

            serviceCollection
                .AddScoped<ICompanyService, CompanyService>();

            serviceCollection
                .AddSingleton<IUserConverter, UserConverter>();

            serviceCollection
                .AddSingleton<ICompanyConverter, CompanyConverter>();
        }

        private static void AddApiRegistrations(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<IUserDocumentConverter, UserDocumentConverter>();

            serviceCollection
                .AddSingleton<ICompanyDocumentConverter, CompanyDocumentConverter>();

            serviceCollection
                .AddTransient<IValidator<UserDocument>, UserDocumentValidator>();

            serviceCollection
                .AddTransient<IValidator<CompanyDocument>, CompanyDocumentValidator>();
        }
    }
}