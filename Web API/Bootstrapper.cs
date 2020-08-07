using AutoMapper;
using smartStoreApi.Models.Configuration;
using smartStoreApi.Repositories;
using smartStoreApi.Repositories.Interfaces;
using smartStoreApi.Services;
using smartStoreApi.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace smartStoreApi
{
    public static class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAuthenticateService, AuthenticateService>();
            services.AddSingleton<IMailService, MailService>();
            services.AddSingleton<IDbConnectionFactory>(new DbConnectionFactory(configuration.GetConnectionString("database")));
            services.AddSingleton<ILoginService, LoginService>();
            services.AddSingleton<ILoginRepository, LoginRepository>();
            services.AddSingleton<IPasswordHashService, PasswordHashService>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IUserService, UserService>();
            services.AddOptions();
            services.Configure<JwtDetails>(options => configuration.GetSection("JwtDetails").Bind(options));
            services.Configure<MailSettings>(options => configuration.GetSection("MailSettings").Bind(options));
            services.AddAutoMapper(typeof(Startup).Assembly);
        }
    }
}