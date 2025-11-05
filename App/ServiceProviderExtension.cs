using Microsoft.Extensions.DependencyInjection;

namespace App;

/// <summary>
/// Extensions class for <see cref="ServiceProvider"/>
/// </summary>
public static class ServiceProviderKeyedExtensions
{
    /// <summary>
    /// Returns dict of keyed services
    /// </summary>
    /// <typeparam name="T">Type of keyed services</typeparam>
    /// <param name="provider">Extended type</param>
    /// <param name="keys">Collection of keys</param>
    /// <returns></returns>
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

    /// <summary>
    /// Returns dict of keyed services (with specified <typeparamref name="KeyType"/>)
    /// </summary>
    /// <typeparam name="T">Type of keyed services</typeparam>
    /// <typeparam name="KeyType">Type of keys</typeparam>
    /// <param name="provider">Extended type</param>
    /// <param name="keys">Collection of keys</param>
    /// <returns></returns>
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
