using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Blog_backend.Models;

namespace Blog_backend.Controllers
{
    public class ResetpaswordController : ApiController
    {
        public HttpResponseMessage otpverfication(string otp)
        {
            try
            {
              using(Contextclass contextclass=new Contextclass())
                {
                    bool OTPverification = contextclass.user_Details.Any(x => x.Password_reset_OTP == otp);
                    if(OTPverification)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        public HttpResponseMessage newpassword(User_details userdetails)
        {
            try
            {
                using(Contextclass contextclass=new Contextclass())
                {
                    var user_data = contextclass.user_Details.Where(x => x.Email == userdetails.Email).FirstOrDefault();
                    string password = userdetails.Password;
                    user_data.Password= passwordencryption(password);
                    contextclass.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public static string passwordencryption(string password)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
