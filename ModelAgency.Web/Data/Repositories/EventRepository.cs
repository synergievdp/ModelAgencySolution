using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ModelAgency.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data.Repositories {
    public class EventRepository : GenericRepository<Event>, IEventRepository {

        public EventRepository(ApplicationDbContext dbContext) : base(dbContext) {

        }

        public IEnumerable<Event> GetAll(Expression<Func<Event, bool>> filter = null, bool invites = false) {
            IQueryable<Event> query = table;
            if (invites)
                query = query.Include(ev => ev.Invites);
            if (filter != null)
                query = query.Where(filter);
            return query.ToList();
        }

        public Event Get(Expression<Func<Event, bool>> filter, bool invites = false) {
            IQueryable<Event> query = table;
            if (invites)
                query = query.Include(ev => ev.Invites);
            return query.FirstOrDefault(filter);
        }
    }
}
