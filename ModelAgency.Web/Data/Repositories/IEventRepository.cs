using Microsoft.EntityFrameworkCore.Query;
using ModelAgency.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data.Repositories {
    public interface IEventRepository : IGenericRepository<Event> {
        public Event GetById(int id, Func<IQueryable<Event>, IIncludableQueryable<Event, object>> includes = null);
    }
}
