using System;
using System.Collections.Generic;
using System.Data;

using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using u18043624_HW06.Models.ViewModels;
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
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            var products = db.products.Include(p => p.brand).Include(p => p.category);
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.product_name.Contains(searchString));
            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(products.ToList().ToPagedList(pageNumber, pageSize));
        }

        // GET: products/Details/5
        [HttpPost]
        public ActionResult Details(int tempId)
        {
            product ProductDB = db.products.Where(x => x.product_id == tempId).FirstOrDefault();
            DetailsVM newProduct = new DetailsVM();
            newProduct.product_name = ProductDB.product_name;
            newProduct.model_year = ProductDB.model_year;
            newProduct.list_price = ProductDB.list_price;
            newProduct.brand_name = ProductDB.brand.brand_name;
            newProduct.category_name = ProductDB.category.category_name;
            newProduct.Quantities = (
                        from stock in db.stocks.ToList()
                        join store in db.stores.ToList() on stock.store_id equals store.store_id
                        where stock.product_id == tempId
                        group stock by stock.store.store_name into groupedStores
                        select new QuantityVM
                        {
                            storeName = groupedStores.Key,
                            quantity = (int)groupedStores.Sum(t => t.quantity)
                        }).ToList();
            return new JsonResult { Data = new { product = newProduct }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        
        // GET: products/Create
        public ActionResult Create()
        {
            ViewBag.brand_id = new SelectList(db.brands, "brand_id", "brand_name");
            ViewBag.category_id = new SelectList(db.categories, "category_id", "category_name");
            return View();
        }

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
        public JsonResult Edit(int? id)
        {
            product product = db.products.Find(id);
            var SerializedProduct = new product
            {
                product_id = product.product_id,
                product_name = product.product_name,
                category_id = product.category_id,
                brand_id = product.brand_id,
                list_price = product.list_price,
                model_year = product.model_year,
                //brands = db.brands.ToList().Select(x => new brand { brand_id = x.brand_id, brand_name = x.brand_name }).ToList(),
                //categories = db.categories.ToList().Select(x => new category { category_id = x.category_id, category_name = x.category_name }).ToList()
            };
            return new JsonResult { Data = new { product = SerializedProduct }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public JsonResult Edit([Bind(Include = "product_id,product_name,brand_id,category_id,model_year,list_price")] product product)
        {
            var message = "";
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    message = "200 OK";
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return new JsonResult { Data = new { status = message }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        // GET: products/Delete/5

        public ActionResult Delete(int? id)
        {
            product ProductDB = db.products.Where(x => x.product_id == id).FirstOrDefault();
            DetailsVM newProduct = new DetailsVM();
            newProduct.model_year = ProductDB.model_year;
            newProduct.product_name = ProductDB.product_name;
            newProduct.list_price = ProductDB.list_price;
            newProduct.brand_name = ProductDB.brand.brand_name;
            newProduct.category_name = ProductDB.category.category_name;
            return new JsonResult { Data = new { product = newProduct }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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
