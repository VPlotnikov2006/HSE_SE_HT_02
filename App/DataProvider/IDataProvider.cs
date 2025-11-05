namespace App.DataProvider;

/// <summary>
/// Interface for requesting data from user (GUI, API, Console ... )
/// </summary>
public interface IDataProvider
{
    /// <summary>
    /// Returns value of type T
    /// </summary>
    /// <typeparam name="T">Type of value, requested from user</typeparam>
    /// <param name="prompt">Help string displayed to user</param>
    /// <param name="options">Style to request data</param>
    /// <returns>User value</returns>
    public T? GetValue<T>(
        string? prompt = null,
        GetDataOptions options = GetDataOptions.Default
    ) where T : IParsable<T>;

    /// /// <summary>
    /// Returns value of type T in specified borders
    /// </summary>
    /// <typeparam name="T">Type of value, requested from user</typeparam>
    /// <param name="min">Lower limit of value (<see cref="null"/> = unlimited)</param>
    /// <param name="max">Upper limit of value (<see cref="null"/> = unlimited)</param>
    /// <param name="prompt">Help string displayed to user</param>
    /// <param name="options">Style to request data</param>
    /// <returns>User value</returns>
    public T GetLimitedValue<T>(
        T? min,
        T? max,
        string? prompt = null,
        GetDataOptions options = GetDataOptions.Default
    ) where T : struct, IParsable<T>, IComparable<T>;

    //TODO: Documentation
    public T SelectValue<T>(IReadOnlyCollection<T> options, string? prompt = null);

    //TODO: Documentation
    public IEnumerable<T> SelectValue<T>(Group<T> options, string? prompt = null);
}