using IDC.Kite.Business.Versions.v1.User;
using Microsoft.Extensions.DependencyInjection;


namespace IDC.Kite.Api
{
    public static class AppServices
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IUserFacade, UserFacade>();
        }
    }
}
