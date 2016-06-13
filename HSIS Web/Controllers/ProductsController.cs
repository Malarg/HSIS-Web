using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HSIS_Web.Models;

namespace HSIS_Web.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index(string sortOrder, string searchString)
        {
            var products1 = db.Products.Include(p => p.Shell);
            var products = from p in products1 select p;
            ViewBag.ShellSort = sortOrder == "shellSortDescending" ? "shellSort" : "shellSortDescending";
            ViewBag.TitleSort = sortOrder == "titleSortDescending" ? "titleSort" : "titleSortDescending";
            ViewBag.TypeSort = sortOrder == "typeSortDescending" ? "typeSort" : "typeSortDescending";
            ViewBag.PurchacePriceSort = sortOrder == "purchacePriceSortDescending" ? "purchacePriceSort" : "purchacePriceSortDescending";
            ViewBag.SellingPriceSort = sortOrder == "sellingPriceSortDescending" ? "sellingPriceSort" : "sellingPriceSortDescending";
            ViewBag.QuantitySort = sortOrder == "quantitySortDescending" ? "quantitySort" : "quantitySortDescending";
            ViewBag.DeskriptionSort = sortOrder == "deskriptionSortDescending" ? "deskriptionSort" : "deskriptionSortDescending";
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Title.Contains(searchString)
                                       || s.Type.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "shellSort":
                {
                    products = products.OrderBy(p => p.Shell.Title);
                } break;
                case "shellSortDescending":
                {
                    products = products.OrderByDescending(p => p.Shell.Title);
                } break;
                case "titleSort":
                {
                    products = products.OrderBy(p => p.Title);
                } break;
                case "titleSortDescending":
                {
                    products = products.OrderByDescending(p => p.Title);
                } break;
                case "typeSort":
                {
                    products = products.OrderBy(p => p.Type);
                } break;
                case "typeSortDescending":
                {
                    products = products.OrderByDescending(p => p.Type);
                } break;
                case "purchacePriceSort":
                {
                    products = products.OrderBy(p => p.PurchacePrice);
                } break;
                case "purchacePriceSortDescending":
                {
                    products = products.OrderByDescending(p => p.PurchacePrice);
                } break;
                case "sellingPriceSort":
                {
                    products = products.OrderBy(p => p.SellingPrice);
                } break;
                case "sellingPriceSortDescending":
                {
                    products = products.OrderByDescending(p => p.SellingPrice);
                } break;
                case "quantitySort":
                {
                    products = products.OrderBy(p => p.Quantity);
                } break;
                case "quantitySortDescending":
                {
                    products = products.OrderByDescending(p => p.Quantity);
                } break;
                case "deskriptionSort":
                {
                    products = products.OrderBy(p => p.Deskription);
                } break;
                case "deskriptionSortDescending":
                {
                    products = products.OrderByDescending(p => p.Deskription);
                } break;
            }
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ShellId = new SelectList(db.Shells, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Vendor")]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Title,Type,PurchacePrice,SellingPrice,Quantity,Deskription,ShellId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShellId = new SelectList(db.Shells, "Id", "Title", product.ShellId);
            return View(product);
        }

        [Authorize(Roles = "Vendor")]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShellId = new SelectList(db.Shells, "Id", "Title", product.ShellId);
            return View(product);
        }

        // POST: Products/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Vendor")]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Title,Type,PurchacePrice,SellingPrice,Quantity,Deskription,ShellId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShellId = new SelectList(db.Shells, "Id", "Title", product.ShellId);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Vendor")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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