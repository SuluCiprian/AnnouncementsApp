using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AnnouncementsApp.Persistence
{
    public interface IRepository<T> where T : class
    {
        void Insert(T entity);
        void Delete(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> SearchFor(Expression<Func<T, bool>> predicate);
    }
}
