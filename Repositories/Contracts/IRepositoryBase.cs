using System.Linq.Expressions;

namespace Repositories.EFCore
{
    public interface IRepositoryBase<T>
    {
        //CRUD
        IQueryable<T> FindAll(bool trackCahnges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackCahnges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
