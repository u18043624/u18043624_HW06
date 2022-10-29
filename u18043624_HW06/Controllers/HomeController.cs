using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using u18043624_HW06.Models

namespace u18043624_HW06.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Orders()
		{
			return View();
		}

	}
}