using Microsoft.EntityFrameworkCore.Query;
using ModelAgency.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data.Repositories {
    public class EventRepository : GenericRepository<Event>, IEventRepository {

        public EventRepository(ApplicationDbContext dbContext) : base(dbContext) {

        }

        public Event GetById(int id, Func<IQueryable<Event>, IIncludableQueryable<Event, object>> includes = null) {
            return includes(table).FirstOrDefault(ev => ev.Id == id);
        }
    }
}
