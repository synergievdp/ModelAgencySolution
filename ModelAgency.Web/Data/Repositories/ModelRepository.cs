using Microsoft.EntityFrameworkCore.Query;
using ModelAgency.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data.Repositories {
    public class ModelRepository : GenericRepository<ModelUser>, IModelRepository {

        public ModelRepository(ApplicationDbContext dbContext) : base(dbContext) {
                
        }

        public ModelUser GetById(string id, Func<IQueryable<ModelUser>, IIncludableQueryable<ModelUser, object>> includes = null) {
            return includes(table).FirstOrDefault(model => model.Id == id);
        }
    }
}
