using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreetUP.Models
{
    public class Event
    {
        public int EventID { get; set; }
        [MaxLength(50)]
        public string EventName { get; set; }
        [MaxLength(100)]
        public string Time { get; set; }
        [MaxLength(50)]
        public string Date { get; set; }
        public string Description { get; set; }
        

        //Navigation Properties 
        //linking ApplicationUserId to ApplicationUser table
        public virtual ApplicationUser ApplicationUser { get; set; }
        //linking Event table to RSVP table
        public virtual ICollection<RSVP> RSVPs { get; set; }
    }
}