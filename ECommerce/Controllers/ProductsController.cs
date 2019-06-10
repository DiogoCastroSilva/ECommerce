using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ECommerce.DAL;
using ECommerce.Models;

namespace ECommerce.Controllers
{
    public class ProductsController : Controller
    {
        private ProductContext db = new ProductContext();

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session["CultureECommerce"] != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["CultureECommerce"].ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["CultureECommerce"].ToString());
            }
        }

        // GET: Products
        public ViewResult Index(string id, string type)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(type))
            {
                Session["Genre"] = null;
                return View(db.Product.ToList());
            }

            var product = from p in db.Product
                          select p;


            if (!string.IsNullOrEmpty(Session["Genre"] as string) && type != "gender")
            {
                Genders genders = (Genders)Enum.Parse(typeof(Genders), Session["Genre"].ToString());
                product = product.Where(p => p.Gender == genders);
            }

            switch (type)
            {
                case "gender":
                    Genders genders = (Genders)Enum.Parse(typeof(Genders), id);
                    product = product.Where(p => p.Gender == genders);
                    Session["Genre"] = id;
                    break;
                case "category":
                    Categorys categorys = (Categorys)Enum.Parse(typeof(Categorys), id);
                    product = product.Where(p => p.Category == categorys);
                    break;
                case "color":
                    Colors colors = (Colors)Enum.Parse(typeof(Colors), id);
                    product = product.Where(p => p.Color == colors);
                    break;
            }



            return View(product.ToList());
        }

        public ViewResult ProductDetails(int id)
        {

            if (id < 0)
            {
                return View();
            }

            var product = from p in db.Product
                          select p;
            product = product.Where(p => p.ID == id);

            return View(product.ToList());
        }

        public ActionResult AddProduct(int id)
        {
            Product product = db.Product.Find(id);

            if (product != null)
            {

                Cart cart = new Cart();
                cart.CartProducts = product;

                db.Cart.Add(cart);
                db.SaveChanges();
                Session["CartLenght"] = db.Cart.Count();
            }

            return RedirectToAction("ProductDetails", "Products", new { id });
        }

        public ViewResult Bag()
        {
            var cart = from c in db.Cart
                       select c.CartProducts;
            var ids = from c in db.Cart
                      select c.ID;
            List<Cart> cartP = new List<Cart>();
            int subtotal = 0;

            foreach (var itemCart in cart)
            {
                var item = cartP.Find(x => x.CartProducts.ID == itemCart.ID);
                if (item == null)
                {
                    cartP.Add(new Cart { CartProducts = itemCart, NumberOfProducts = 1 });
                    subtotal += int.Parse(itemCart.Price);
                }
                else
                {
                    foreach (var cartProduct in cartP)
                    {
                        if (cartProduct.CartProducts.ID == itemCart.ID)
                        {
                            cartProduct.NumberOfProducts += 1;
                            subtotal += int.Parse(itemCart.Price);
                        }
                    }
                }


            }

            int total = 0;
            if (subtotal > -1)
            {
                total = subtotal;
            }
            ViewBag.Subtotal = subtotal;
            ViewBag.Total = total;



            //return View(products.ToList());
            return View(cartP.ToList());
        }

        public ActionResult RemoveFromBag(int id)
        {
            var cart = from c in db.Cart
                       select c;

            Cart cartItem = cart.FirstOrDefault(x => x.CartProducts.ID == id);
            if (cartItem != null)
            {
                db.Cart.Remove(cartItem);
                db.SaveChanges();
            }

            Session["CartLenght"] = db.Cart.Count();
            return RedirectToAction("Bag");
        }

        public PartialViewResult _FilterPartial()
        {
            return PartialView();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
