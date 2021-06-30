using Microsoft.EntityFrameworkCore.Query;
using ModelAgency.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data.Repositories {
    public interface IModelRepository : IGenericRepository<ModelUser> {
        public ModelUser Get(Expression<Func<ModelUser, bool>> filter = null, bool photos = false, bool invites = false);
        public IEnumerable<ModelUser> GetAll(Expression<Func<ModelUser, bool>> filter = null, bool photos = false, bool invites = false);
    }
}
