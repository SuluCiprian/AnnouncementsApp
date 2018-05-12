using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AnnouncementsApp.Persistence;
using System.Linq;
using System.Linq.Expressions;

namespace AnnouncementsApp.Persistence.EF
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public void Insert(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public IEnumerable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }
    }
}
