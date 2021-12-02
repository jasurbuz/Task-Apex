using X.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskApix.Dtos;

namespace TaskApix.Services.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null, bool tracking = false);

        Task<IPagedList<T>> GetPagedList(
            RequestParams requestParams,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null,
            bool tracking = false);

        Task<IPagedList<T>> SearchPagedList(
            RequestParams requestParams,
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null,
            bool tracking = false);

        Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null,
            bool tracking = false);

        Task Insert(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}
