using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class CustomerSupportController : Controller
    {
        // GET: CustomerSupport

        public ActionResult Index(string viewToRender)
        {
            if (string.IsNullOrEmpty(viewToRender))
            {
                ViewBag.ViewToRender = "_ContactUs";
            }
            else
            {
                ViewBag.ViewToRender = viewToRender;
            }
            return View();
        }

        public ActionResult _ContactUs()
        {
            return View();
        }

        public ActionResult _HowToShop()
        {
            return View();
        }

        public ActionResult _OrdersAndShipping()
        {
            return View();
        }

        public ActionResult _PaymentAndPricing()
        {
            return View();
        }

        public ActionResult _ReturnsAndRefunds()
        {
            return View();
        }

        public ActionResult _FAQ()
        {
            return View();
        }

        public ActionResult _TermsAndConditions()
        {
            return View();
        }

        public ActionResult _PrivacyAndPolicy()
        {
            return View();
        }
    }
}