using Microsoft.EntityFrameworkCore.Query;
using ModelAgency.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data.Repositories {
    public interface ICustomerRepository : IGenericRepository<CustomerUser> {
        public CustomerUser GetById(string id, Func<IQueryable<CustomerUser>, IIncludableQueryable<CustomerUser, object>> includes = null);
    }
}
