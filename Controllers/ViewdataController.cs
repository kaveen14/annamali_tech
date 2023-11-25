using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using pro4.Models;

namespace pro4.Controllers
{
    public class ViewdataController : Controller
    {
        // GET: Viewdata
       
       
        public ActionResult dashboard()
        {
            if (Session["id"] != null & Session["pass"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","login");
            }
        }
        public ActionResult dataview()
        {
           
            if (Session["id"] != null & Session["pass"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","login");
            }
        }
         datalink dl = new datalink();
        
       [HttpPost]
        public JsonResult datatable(string db1)
        {
            Session["datab"] = db1;
            return Json(JsonConvert.SerializeObject(dl.List1(@Session["datab"].ToString())), JsonRequestBehavior.AllowGet);
        }
        /* public string database(string datab, string datay)
           {
               if (datab != null && datay != null)
               {
                   Session["datab"] = datab;
                   Session["datay"] = datay;
                   return "Valid";
               }
               else
               {
                   return "Invalid";
               }
               /* try {
                    //   return Json(JsonConvert.SerializeObject(dl.datacheck(datab,datay)), JsonRequestBehavior.AllowGet);
                   return dl.datacheck(datab,datay);
                }
                catch(Exception ex)
                {
                    throw ex;
                }

           }*/



        public ActionResult loaddata()
        {
          //  Session["dyear"] = datay;
                 if(Session["id"] != null & Session["pass"] != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "login");
                }
            
        }
        public string datayear(string datay)
        {
            try
            {
                Session["datay"] = datay;
                return "valid";
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        string data_b = "", data_y = "";
        public JsonResult tbody(string cbcd, string Pagesize, string Pageindex)
        {
            data_b = Session["datab"].ToString();
            data_y = Session["datay"].ToString();

            try
            {
                var data = dl.tbody(data_b,data_y,cbcd, Pagesize, Pageindex);
                var result = new { Results = data.Item1, Total = data.Item2 };
                return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}