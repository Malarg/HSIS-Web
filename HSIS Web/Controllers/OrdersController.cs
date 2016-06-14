using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HSIS_Web.Models;
using Excel = Microsoft.Office.Interop.Excel;

namespace HSIS_Web.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void CreateReport()
        {
            Excel.Application app = new Excel.Application();
            var workbook = app.Workbooks.Add(Type.Missing);
            var worksheet = workbook.Worksheets.Add(Type.Missing);
            worksheet.Cells[1, 1].Value2 = "Title";
            worksheet.Cells[1, 2].Value2 = "Profit";
            worksheet.Cells[1, 3].Value2 = "Income";
            worksheet.Cells[1, 4].Value2 = "Date";
            worksheet.Cells[1, 5].Value2 = "Product";
            worksheet.Cells[1, 6].Value2 = "Vendor";
            worksheet.Cells[1, 7].Value2 = "Client";
            var orders = db.Orders.ToList();
            for (var i = 0; i < orders.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value2 = orders[i].Title;
                worksheet.Cells[i + 2, 2].Value2 = orders[i].Profit;
                worksheet.Cells[i + 2, 3].Value2 = orders[i].Income;
                worksheet.Cells[i + 2, 4].Value2 = orders[i].Date.ToString();
                worksheet.Cells[i + 2, 5].Value2 = orders[i].Product?.Title;
                worksheet.Cells[i + 2, 6].Value2 = orders[i].Vendor?.FullName;
                worksheet.Cells[i + 2, 7].Value2 = orders[i].Client?.FullName;
            }
            workbook.SaveAs("C:\\Users\\malar\\Downloads\\Отчет.xlsx");
            app.Quit();
        }

        // GET: Orders
        [Authorize(Roles = "Admin,Vendor,Client")]
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Client).Include(o => o.Product).Include(o => o.Vendor);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        [Authorize(Roles = "Admin,Vendor")]
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Title");
            ViewBag.VendorId = new SelectList(db.Vendors, "Id", "FirstName");
            return View();
        }

        // POST: Orders/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Vendor")]
        public ActionResult Create([Bind(Include = "Id,Title,Profit,Income,Date,ProductId,VendorId,ClientId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName", order.ClientId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Title", order.ProductId);
            ViewBag.VendorId = new SelectList(db.Vendors, "Id", "FirstName", order.VendorId);
            return View(order);
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "Admin,Vendor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName", order.ClientId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Title", order.ProductId);
            ViewBag.VendorId = new SelectList(db.Vendors, "Id", "FirstName", order.VendorId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Vendor")]
        public ActionResult Edit([Bind(Include = "Id,Title,Profit,Income,Date,ProductId,VendorId,ClientId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FirstName", order.ClientId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Title", order.ProductId);
            ViewBag.VendorId = new SelectList(db.Vendors, "Id", "FirstName", order.VendorId);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin,Vendor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
