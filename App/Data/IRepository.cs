namespace App.Data;

public interface IRepository<T>
{
    public T? GetById(Guid Id);
    public IReadOnlyCollection<T> GetAll();
    public bool Add(T obj);
    public bool Delete(Guid id);
}
