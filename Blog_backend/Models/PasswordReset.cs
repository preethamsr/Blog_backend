using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog_backend.Models
{
    public class PasswordReset
    {
        
        public string Password_reset_OTP { get; set; }
        public string Password { get; set; }
    }
}