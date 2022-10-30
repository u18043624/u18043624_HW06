using System;
using System.Collections.Generic;
using System.Data;

using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using u18043624_HW06.Models;
//check which used, remove ones that arent
using PagedList;
using PagedList.Mvc;
using System.Configuration;
using System.Data.SqlClient;


namespace u18043624_HW06.Views
{
    public class productsController : Controller
    {
        BikeStoresEntities db = new BikeStoresEntities();

        // GET: products
        public ActionResult Index(string search, int? i)
        {
            var products = db.products.Include(p => p.brand).Include(p => p.category);
            return View(db.products.Where(x => x.product_name.StartsWith(search) || search == null).ToList().ToPagedList(i ?? 1, 10));
        }

        // GET: products/Details/5
        [HttpPost]
        public ActionResult Details(int tempId)
        {

            return PartialView("Details", db.products.Find(tempId));
            /*
            product prod = db.products.Where(x=> x.product_id == tempId).FirstOrDefault();
            ProductsCRUDClass prodClass = new ProductsCRUDClass();
            prodClass.product_id = Convert.ToInt32(prod.product_id);
            prodClass.product_name = prod.product_name;
            prodClass.model_year = prod.model_year;
            prodClass.list_price = prod.list_price;
            prodClass.category.category_name = prod.category.category_name;
            prodClass.brand.brand_name = prod.brand.brand_name;
            
            return PartialView(prodClass);
            */
        }
        /*
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        */
        // GET: products/Create
        public PartialViewResult Create()
        {
            //ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name");
            //ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name");
            return PartialView("Create", new Models.ProductsCRUDClass());
        }

        public JsonResult Create(product prod)
        {
            db.products.Add(prod);
            db.SaveChanges();
            return Json(prod, JsonRequestBehavior.AllowGet);
        }

        // POST: products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,product_name,brand_id,category_id,model_year,list_price")] product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name", product.brand_id);
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", product.category_id);
            return View(product);
        }
        */
        // GET: products/Edit/5

        /*
    public PartialViewResult Edit(int? id)
    {

        //product prod = db.products.Where(x=> x.product_id == )
        //return PartialView(product);

    }
    */

        /*
        // POST: products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,product_name,brand_id,category_id,model_year,list_price")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name", product.brand_id);
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name", product.category_id);
            return View(product);
        }
        */
        // GET: products/Delete/5
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
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
