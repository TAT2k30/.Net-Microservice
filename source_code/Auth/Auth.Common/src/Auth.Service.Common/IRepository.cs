using System.Linq.Expressions;
using Auth.Service.Common.Entities;

namespace Auth.Service.Common.Repository
{
    public interface IRepository<T> where T : IEntity
    {
        Task CreateAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindByConditionAsync(Expression<Func<T, bool>> expression);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<T> FindByUserNameAsync(string userName);
        Task<T> FindByEmailAsync(string email);
        Task<bool> ValidateCredentialsAsync(string userName, string password);
        Task<IEnumerable<string>> GetRoleAsync(Guid id);
    }
}