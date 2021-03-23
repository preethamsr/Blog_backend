using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog_backend.Models;

namespace Blog_backend.Controllers
{
    public class UserverificationController : Controller
    {
        // GET: Userverification
        public ActionResult userverification(Guid activationcode)
        {
            using(Contextclass contextclass=new Contextclass())
            {
                var verfication_to_true = contextclass.user_Details.Where(x => x.user_id == activationcode).FirstOrDefault();
                verfication_to_true.Verification = true;
                contextclass.SaveChanges();
                return View();
            }
            
        }
    }
}