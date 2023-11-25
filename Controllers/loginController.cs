using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pro4.Models;

namespace pro4.Controllers
{
    public class loginController : Controller
    {
        // GET: login
        public ActionResult Index()
        {
            Session.Remove("id");
            Session.Remove("pass");
            return View();
        }

        login l = new login();
        [HttpPost]
        public JsonResult logincheckcon(int id, string pass)
        {
            try
            {
                var res = l.Logincheckup(id, pass);
                Session["id"] = res.id;
                Session["pass"] = res.pass;
                Session["Name"] = res.name;
               
                return Json(res.result);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}