//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Blog_backend
{
    using System;
    using System.Collections.Generic;
    
    public partial class user_details
    {
        public System.Guid user_id { get; set; }
        public string First_name { get; set; }
        public string Middle_name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Dob { get; set; }
        public string Role { get; set; }
        public Nullable<System.DateTime> Created_on { get; set; }
        public Nullable<bool> Verification { get; set; }
        public string Password_reset_OTP { get; set; }
    }
}
