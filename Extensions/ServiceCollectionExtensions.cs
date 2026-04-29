using Microsoft.Extensions.DependencyInjection;

namespace ZawatSys.MicroLib.Notification.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNotificationServices(this IServiceCollection services)
    {
        return services;
    }
}
