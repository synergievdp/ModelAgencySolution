using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data.Repositories {
    public interface IGenericRepository<T> where T : class {
        public void Insert(T obj);
        public void Delete(object id);
        public void Update(T obj);
    }
}
