using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelAgency.Web.Data.Entities {
    public class Invite {
        public int Id { get; set; }
        public ModelUser Invitee { get; set; }
        public Event Event { get; set; }
        public InviteState OrganizerAccepted { get; set; }
        public InviteState InviteeAccepted { get; set; }
    }
}
