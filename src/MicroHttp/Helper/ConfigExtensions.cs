using MicroHttp.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MicroHttp.Helper
{
    public static class ConfigExtensions
    {
        public static void AddIHttp(this IServiceCollection services)
        {
            services.AddSingleton<IMicroHttp, MicroHttp>();
        }
    }
}
