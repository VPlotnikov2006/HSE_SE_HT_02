using Microsoft.Extensions.DependencyInjection;

namespace App;

public static class ServiceProviderKeyedExtensions
{
    public static Dictionary<object, T> GetKeyedServicesDictionary<T>(
        this IServiceProvider provider,
        IEnumerable<object> keys)
        where T : class
    {
        var dict = new Dictionary<object, T>();

        foreach (var key in keys)
        {
            var service = provider.GetKeyedService<T>(key);
            if (service is not null)
                dict[key] = service;
        }

        return dict;
    }

    public static Dictionary<KeyType, T> GetKeyedServicesDictionary<KeyType, T>(
        this IServiceProvider provider,
        IEnumerable<KeyType> keys)
        where T : class
        where KeyType : notnull
    {
        var dict = new Dictionary<KeyType, T>();

        foreach (var key in keys)
        {
            var service = provider.GetKeyedService<T>(key);
            if (service is not null)
                dict[key] = service;
        }

        return dict;
    }
}
