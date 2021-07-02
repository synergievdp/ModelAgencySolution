using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data.Entities {
    public class Invite {
        [ForeignKey("Model")]
        public string ModelId { get; set; }
        public ModelUser Model { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }
        public InviteState OrganizerAccepted { get; set; }
        public InviteState InviteeAccepted { get; set; }
    }
}
