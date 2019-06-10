using ECommerce.DAL;
using ECommerce.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{

    public class HomeController : BaseController
    {

        private ProductContext db = new ProductContext();

        //initilizing culture on controller initialization
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session["CultureECommerce"] != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["CultureECommerce"].ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["CultureECommerce"].ToString());
            }
           
        }

        public ActionResult Index()
        {
            Session["CartLenght"] = db.Cart.Count();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            Session["CultureECommerce"] = culture;
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["CultureECommerce"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("CultureECommerce");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(Request.UrlReferrer.ToString());//RedirectToAction("Index");
        }
    }
}