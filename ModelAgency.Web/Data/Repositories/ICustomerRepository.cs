using Microsoft.EntityFrameworkCore.Query;
using ModelAgency.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data.Repositories {
    public interface ICustomerRepository : IGenericRepository<CustomerUser> {
        public CustomerUser Get(Expression<Func<CustomerUser, bool>> filter = null, bool events = false);
        public IEnumerable<CustomerUser> GetAll(Expression<Func<CustomerUser, bool>> filter = null, bool events = false);
    }
}
