using Microsoft.EntityFrameworkCore.Query;
using ModelAgency.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data.Repositories {
    public interface IEventRepository : IGenericRepository<Event> {
        public Event Get(Expression<Func<Event, bool>> filter = null, bool invites = false);
        public IEnumerable<Event> GetAll(Expression<Func<Event, bool>> filter = null, bool invites = false);
    }
}
