using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ModelAgency.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data.Repositories {
    public class ModelRepository : GenericRepository<ModelUser>, IModelRepository {

        public ModelRepository(ApplicationDbContext dbContext) : base(dbContext) {
                
        }

        public IEnumerable<ModelUser> GetAll(Expression<Func<ModelUser, bool>> filter = null, bool photos = false, bool invites = false) {
            IQueryable<ModelUser> query = table;
            if (photos)
                query = query.Include(model => model.Photos);
            if (invites)
                query = query.Include(model => model.Invites).ThenInclude(invite => invite.Event);
            if (filter != null)
                query = query.Where(filter);
            return query.ToList();
        }

        public ModelUser Get(Expression<Func<ModelUser, bool>> filter, bool photos = false, bool invites = false) {
            IQueryable<ModelUser> query = table;
            if (photos)
                query = query.Include(model => model.Photos);
            if (invites)
                query = query.Include(model => model.Invites).ThenInclude(invite => invite.Event);
            return query.FirstOrDefault(filter);
        }
    }
}
