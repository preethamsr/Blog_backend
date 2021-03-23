using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blog_backend.Models
{
    [Table("user_details")]
    public class User_details
    {
        [Key]
        public Guid user_id { get; set; }
        public string First_name { get; set; }
        public string Middle_name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
      //  public Nullable<DateTime> Dob { get; set; }
        public string Role { get; set; }
        public DateTime Created_on  { get; set;}
        public Boolean Verification { get; set; }
        public string Password_reset_OTP { get; set; }
     
    }

    
}