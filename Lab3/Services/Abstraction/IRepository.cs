namespace Lab3.Services.Abstraction;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
}