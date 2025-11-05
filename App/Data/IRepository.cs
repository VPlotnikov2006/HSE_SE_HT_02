namespace App.Data;

/// <summary>
/// Abstract repository interface
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T>
{
    /// <summary>
    /// Search object by id
    /// </summary>
    /// <param name="Id">Object id</param>
    /// <returns>Object with current <paramref name="Id"/> or <see cref="null"/></returns>
    public T? GetById(Guid Id);

    /// <summary>
    /// Get all objects, stored in repository
    /// </summary>
    /// <returns></returns>
    public IReadOnlyCollection<T> GetAll();

    /// <summary>
    /// Add new object in repository
    /// </summary>
    /// <param name="obj">New object</param>
    /// <returns><see cref="true"/> if operation successfully finished, otherwise <see cref="false"/></returns>
    public bool Add(T obj);

    /// <summary>
    /// Remove object from repository
    /// </summary>
    /// <param name="id">Object id</param>
    /// <returns><see cref="true"/> if operation successfully finished, otherwise <see cref="false"/></returns>
    public bool Delete(Guid id);
}
