using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data.Entities {
    public class Event {
        public int Id { get; set; }
        public string Name { get; set; }
        public EventType EventType { get; set; }
        public CustomerUser Organizer { get; set; }
        public bool Private { get; set; }
        public List<Invite> Invites { get; set; }
    }
}
