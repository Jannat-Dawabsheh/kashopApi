using kashop.bll.Service;
using kashop.dal.Repository;
using kashop.dal.Utils;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace kashop.pl
{
    public static class AppConfiguration
    {
        public static void Config(IServiceCollection Services)
        {
            Services.AddScoped<ICategoryRepository, CategoryRepository>();
            Services.AddScoped<ICategoryService, CategoryService>();
            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<ISeedData, RoleSeedData>();
            Services.AddScoped<ISeedData, UserSeedData>();
            Services.AddTransient<IEmailSender, EmailSender>();

            Services.AddScoped<IFileServices, FileService>();
            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<IProductRepository, ProductRepository>();
            Services.AddScoped<ITokenService, TokenService>();

        }
    }
}
