using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ModelAgency.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data.Repositories {
    public class CustomerRepository : GenericRepository<CustomerUser>, ICustomerRepository {

        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext) {

        }

        public IEnumerable<CustomerUser> GetAll(Expression<Func<CustomerUser, bool>> filter = null, bool events = false) {
            IQueryable<CustomerUser> query = table;
            if (events)
                query = query.Include(customer => customer.Events).ThenInclude(ev => ev.Invites);
            if (filter != null)
                query = query.Where(filter);
            return query.ToList();
        }

        public CustomerUser Get(Expression<Func<CustomerUser, bool>> filter, bool events = false) {
            IQueryable<CustomerUser> query = table;
            if (events)
                query = query.Include(customer => customer.Events).ThenInclude(ev => ev.Invites);
            return query.FirstOrDefault(filter);
        }
    }
}
