using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> GetByID(object id);
        Task Delete(TEntity entity);
        Task Delete(object id);
    }
}