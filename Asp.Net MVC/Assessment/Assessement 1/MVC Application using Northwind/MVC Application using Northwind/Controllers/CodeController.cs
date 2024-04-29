using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Application_using_Northwind.Controllers
{
    public class CodeController : Controller
    {

        private static NorthwindDataContext db = new NorthwindDataContext();

        // GET: Code
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GermanCustomers()
        {
            var germanCustomers = (from c in db.Customers
                                   where c.Country == "Germany"
                                   select c).ToList();
            return View(germanCustomers);
        }

        // Action method to return customer details with an orderId==10248
        public ActionResult CustomerDetailsWithOrderId()
        {
            var customer = (from c in db.Customers
                            where c.Orders.Any(o => o.OrderID == 10248)
                            select c).FirstOrDefault();
            return View(customer);
        }
    }
}