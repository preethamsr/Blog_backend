using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Blog_backend.Models
{
    public class Contextclass:DbContext
    {
        public DbSet<User_details> user_Details { get; set; }
        public DbSet<postquestion> postquestions { get; set; }
    }
}