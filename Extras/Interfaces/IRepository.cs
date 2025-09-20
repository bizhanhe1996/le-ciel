using LeCiel.Database.Models;

namespace LeCiel.Extras.Interfaces;

public interface IRepository<T, U>
    where T : BaseModel
{
    Task<T?> CreateAsync(T model);
    Task<List<T>> IndexAsync(int page, int pageSize);
    Task<T?> FindAsync(uint id);
    Task<T?> UpdateAsync(uint id, U dto);
    Task<T?> DeleteAsync(uint id);
};
