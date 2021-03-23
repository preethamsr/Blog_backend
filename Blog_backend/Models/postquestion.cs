using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog_backend.Models
{
    [Table("Postquestion")]
    public class postquestion
    {
        [Key]
        public Guid PostID { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public string fileName { get; set; }
        public int fileSize { get; set; }
        public string fileType { get; set; }
        public string fileAsBase64 { get; set; }
        public byte[] fileasbytearray { get; set; }
        public Guid userid { get; set; }
        public DateTime? createdon { get; set; }
        public DateTime? Modifiedon { get; set; }
    }
}