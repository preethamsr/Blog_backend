using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using Blog_backend.Models;

namespace Blog_backend.Controllers
{
    public class LoginController : ApiController
    {
        public HttpResponseMessage Login([FromBody] User_details userdetails) 
        {          
            
                using(Contextclass contextclass=new Contextclass())
                {
                    bool Isvalid = contextclass.user_Details.Any(x => x.Email ==userdetails.Email && x.Password == userdetails.Password && x.Verification == true);
                    if(Isvalid)
                    {
                        var Activationcode = contextclass.user_Details.Where(x => x.Email == userdetails.Email).Select(x => x.user_id).Single();
                        var message = Request.CreateResponse(HttpStatusCode.OK,Activationcode);
                        return message;
                    }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent,"User_not_found");
                }
            }                     
        }

        [HttpPost]
        public HttpResponseMessage Forgotpassword(string Email)
        {
            try 
            {
                using (Contextclass contextclass = new Contextclass())
                {
                    Random r = new Random();
                    int Num = r.Next();
                    string OTP = Num.ToString();
                    bool emailexits = contextclass.user_Details.Any(x => x.Email == Email);
                    if(emailexits)
                    {
                        var Generateuserverificationlink = "/Resetpassword/Resetpassword?Email=" +Email;
                        var link = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, Generateuserverificationlink);
                        var frommail = new MailAddress("laleshraj716@gmail.com", "UserBlog");
                        var fromemailpassword = "ranjitha716";
                        var toemailaddress = new MailAddress(Email);
                        var smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(frommail.Address, fromemailpassword);
                        var Message = new MailMessage(frommail, toemailaddress);
                        Message.Subject = "UserBlog--->Password Reset";
                        Message.Body = "<br/>click on the below link for Password Reset" +
                             "<br/><br/><a href=" + link + ">" + link + "</a>" +
                             "<br/><br/><h3>One time password</h3>" +
                             "<br/>" + OTP + "";
                        Message.IsBodyHtml = true;
                        smtp.Send(Message);
                        var SaveOtp = contextclass.user_Details.Where(x => x.Email == Email).FirstOrDefault();
                        SaveOtp.Password_reset_OTP = OTP;
                        contextclass.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, "Email successfully sent to email");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Not found");
                    }
                  
                }
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex);
            }
           
        }
    }
}
