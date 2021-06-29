using Microsoft.EntityFrameworkCore.Query;
using ModelAgency.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data.Repositories {
    public class CustomerRepository : GenericRepository<CustomerUser>, ICustomerRepository {

        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext) {

        }

        public CustomerUser GetById(string id, Func<IQueryable<CustomerUser>, IIncludableQueryable<CustomerUser, object>> includes = null) {
            return includes(table).FirstOrDefault(customer => customer.Id == id);
        }
    }
}
