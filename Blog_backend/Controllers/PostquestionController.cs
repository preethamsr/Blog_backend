using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Blog_backend.Models;


namespace Blog_backend.Controllers
{
    public class PostquestionController : ApiController
    {
        public HttpResponseMessage postquestion([FromBody] postquestion postquestion,Guid activationdcode)
        {
            try
            {
                using(Contextclass contextclass=new Contextclass())
                {
                    postquestion.PostID = Guid.NewGuid();
                    postquestion.userid = activationdcode;
                    postquestion.createdon = DateTime.Now;
                    if(postquestion.fileAsBase64.Contains(","))
                    {
                        postquestion.fileAsBase64 = postquestion.fileAsBase64.Substring(postquestion.fileAsBase64.IndexOf(",") + 1);
                    }
                    postquestion.fileasbytearray = Convert.FromBase64String(postquestion.fileAsBase64);
                    postquestion.fileAsBase64 = null;
                    contextclass.postquestions.Add(postquestion);
                    contextclass.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.OK, "Successfully_posted");
                    return message;
                }
                
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

      
        }
    }
}
