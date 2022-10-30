using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using u18043624_HW06.Models;

namespace u18043624_HW06.Controllers
{
	public class HomeController : Controller
	{

		private BikeStoresEntities db = new BikeStoresEntities();

		public ActionResult Orders(int? id, string search, int? i)
		{
			var orderIds = db.order_items.Select(x => x.order_id).ToList();
			//var orders = 
			//var orders = db.order_items.Where(p => p.order_id == order_items.order_id).ToList();
			return View(db.order_items.Where(x => x.order.order_date.ToString().StartsWith(search) || search == null).ToList().ToPagedList(i ?? 1, 10)); //converted to string, is right?
		}

		public ActionResult Report()
		{
			return View();
		}

	}
}