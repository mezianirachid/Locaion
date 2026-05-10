using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Location.DAL;

namespace Location.Controllers
{
    [Authorize]
    public class MeublesController : Controller
    {
        private ApplicationConnection db = new ApplicationConnection();
        private string userNameConnected = System.Web.HttpContext.Current.User.Identity.Name;
        // GET: Meubles
        public ActionResult Index()
        {
            return View(db.Meubles.ToList());
        }

        // GET: Meubles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meubles meubles = db.Meubles.Find(id);
            if (meubles == null)
            {
                return HttpNotFound();
            }
            return View(meubles);
        }

        // GET: Meubles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Meubles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Meubles meubles)
        {
            if (ModelState.IsValid)
            {
                meubles.CreatedBy = userNameConnected;
                meubles.CreatedOn = DateTime.Now;
                db.Meubles.Add(meubles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meubles);
        }

        // GET: Meubles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meubles meubles = db.Meubles.Find(id);
            if (meubles == null)
            {
                return HttpNotFound();
            }
            return View(meubles);
        }

        // POST: Meubles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Meubles meubles)
        {
            if (ModelState.IsValid)
            {
                meubles.CreatedBy = userNameConnected;
                meubles.CreatedOn = DateTime.Now;
                db.Entry(meubles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(meubles);
        }

        // GET: Meubles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meubles meubles = db.Meubles.Find(id);
            if (meubles == null)
            {
                return HttpNotFound();
            }
            return View(meubles);
        }

        // POST: Meubles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meubles meubles = db.Meubles.Find(id);
            db.Meubles.Remove(meubles);
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
        public ActionResult Activer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meubles item = db.Meubles.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                item.Statut = "A";
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public ActionResult Desactiver(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meubles item = db.Meubles.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                item.Statut = "I";
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public ActionResult ExportToExcel()
        {
            var resultats = db.Meubles.OrderBy(d => d.Nom);//Declaration de la variable demandes           
            var data = resultats.Select(d => new
            {
                Nom = d.Nom,
                Description = d.Description
            });
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable("Liste meubles");
                dt.Columns.AddRange(new DataColumn[2] {
                new DataColumn("Nom"),
                new DataColumn("Description"), 
            });
                foreach (var item in data)
                {
                    dt.Rows.Add(item.Nom,
                                item.Description                                
                                );
                }

                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Meubles.xlsx");
                }
            }
        }
    }
}
