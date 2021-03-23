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
    public class SigninController : ApiController
    {
        Contextclass contextclass = new Contextclass();
        [HttpPost]
        public HttpResponseMessage signup([FromBody] User_details userdetails)
        {
            try
            {
                using(Contextclass contextclass=new Contextclass())
                {
                   var mailverification = emailexits(userdetails.Email);
                    if(mailverification)
                    {
                        return Request.CreateResponse(HttpStatusCode.NoContent, "email_already_exits");
                    }
                    userdetails.user_id = Guid.NewGuid();
                    userdetails.Verification = false;
                    userdetails.Created_on = DateTime.Now.Date;
                    contextclass.user_Details.Add(userdetails);
                    contextclass.SaveChanges();
                    senduserverificationmail(userdetails.Email, userdetails.user_id);
                    var message = Request.CreateResponse(HttpStatusCode.Created,userdetails.user_id);
                    return message;
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public bool emailexits(string email_address)
        {
            Contextclass contextclass = new Contextclass();
            var Emailduplication = contextclass.user_Details.Where(x => x.Email == email_address).FirstOrDefault();
            return Emailduplication != null;
        }

        public void senduserverificationmail(string email_address,Guid activation_code)
        {
            var Generateuserverificationlink = "/Userverification/userverification?activationcode=" + activation_code;
            var link = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, Generateuserverificationlink);
            var frommail = new MailAddress("laleshraj716@gmail.com", "UserBlog");
            var fromemailpassword = "ranjitha716";
            var toemailaddress = new MailAddress(email_address);
            var smtp = new SmtpClient();
            smtp.Host= "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(frommail.Address,fromemailpassword);
            var Message = new MailMessage(frommail, toemailaddress);
            Message.Subject = "UserBlog--->User verification";
            Message.Body="<br/>Please click on the below link for account activation"+
                 "<br/><br/><a href=" + link + ">" + link + "</a>";
            Message.IsBodyHtml = true;
            smtp.Send(Message);
                         
        }

        public HttpResponseMessage google_login([FromBody] User_details userdetails)
        {
            try
            {
                
                userdetails.user_id = Guid.NewGuid();
                userdetails.Created_on = DateTime.Now;
                var emailverification = emailexits(userdetails.Email);
                if (emailverification)
                {
                    var activationkey = contextclass.user_Details.Where(x => x.Email == userdetails.Email).Select(x => x.user_id).FirstOrDefault();
                    return Request.CreateResponse(HttpStatusCode.Accepted,activationkey);
                }
                contextclass.user_Details.Add(userdetails);
                contextclass.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK,userdetails.user_id);
            }
            catch(HttpResponseException ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex);
            }
         
            
        }
    }
    
}
