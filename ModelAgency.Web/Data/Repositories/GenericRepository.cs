using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data.Repositories {
    public class GenericRepository<T> : IGenericRepository<T> where T : class {
        protected readonly ApplicationDbContext dbContext;
        protected readonly DbSet<T> table;

        public GenericRepository(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
            table = dbContext.Set<T>();
        }
        protected IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null) {
            IQueryable<T> query = table;

            if (filter != null) {
                query = query.Where(filter);
            }

            if (includes != null) {
                query = includes(query);
            }

            if (orderBy != null) {
                return orderBy(query).ToList();
            } else {
                return query.ToList();
            }
        }

        public void Delete(object id) {
            T existing = table.Find(id);
            table.Remove(existing);
            dbContext.SaveChanges();
        }

        public void Insert(T obj) {
            table.Add(obj);
            dbContext.SaveChanges();
        }

        public void Update(T obj) {
            table.Attach(obj);
            dbContext.Entry(obj).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
