using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog_backend.Models
{
    public class Media
    {
        public Guid Media_ID { get; set; }
        public string Mediatype { get; set; }
        public int Mediasize { get; set; }
        public byte[] Images { get; set; }
        public byte[] Videos { get; set; }
        public DateTime Uploadedon { get; set; }
        public Guid User_id { get; set; }
        public Guid Post_id { get; set; }
    }
}