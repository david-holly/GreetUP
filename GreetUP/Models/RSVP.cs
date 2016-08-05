using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreetUP.Models
{
    public class RSVP
    {
        public int RSVPID { get; set; }

    //Navigation Properties
    //linking EventId to Event table---linking ApplicationUserId to ApplicationUser table
        public virtual Event Event { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}