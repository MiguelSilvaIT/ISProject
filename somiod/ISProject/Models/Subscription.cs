using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISProject.Models
{
    public class Subscription
    {


        public int id { get; set; }
        public string name { get; set; }
        public DateTime creation_dt { get; set; }

        public int parent { get; set; }

        public string event_type { get; set; }

        public string endpoint { get; set; }
    }
}