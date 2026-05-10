using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using Location.DAL;
 

namespace Location.Controllers
{
    [Authorize]
    public class AppareilsController : Controller
    {
        private ApplicationConnection db = new ApplicationConnection();
        private string userNameConnected = System.Web.HttpContext.Current.User.Identity.Name;
        // GET: Appareils
        public ActionResult Index()
        {
            return View(db.Appareils.ToList());
        }

        // GET: Appareils/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appareils appareils = db.Appareils.Find(id);
            if (appareils == null)
            {
                return HttpNotFound();
            }
            return View(appareils);
        }

        // GET: Appareils/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Appareils/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Appareils appareil)
        {
            if (ModelState.IsValid)
            {
                appareil.CreatedBy = userNameConnected;
                appareil.CreatedOn = DateTime.Now;
                db.Appareils.Add(appareil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appareil);
        }

        // GET: Appareils/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appareils appareils = db.Appareils.Find(id);
            if (appareils == null)
            {
                return HttpNotFound();
            }
            return View(appareils);
        }

        // POST: Appareils/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NomAppareil,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,DescriptionAppareil,StatutAppareil")] Appareils appareil)
        {
            if (ModelState.IsValid)
            {
                appareil.ModifiedBy = userNameConnected;
                appareil.ModifiedOn = DateTime.Now;
                db.Entry(appareil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appareil);
        }

        // GET: Appareils/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appareils appareils = db.Appareils.Find(id);
            if (appareils == null)
            {
                return HttpNotFound();
            }
            return View(appareils);
        }

        // POST: Appareils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appareils appareils = db.Appareils.Find(id);
            db.Appareils.Remove(appareils);
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
            Appareils item = db.Appareils.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                item.StatutAppareil = "A";
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
            Appareils item = db.Appareils.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                item.StatutAppareil = "I";
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public ActionResult ExportToExcel()
        {
            var resultats = db.Appareils.OrderBy(d => d.NomAppareil);//Declaration de la variable demandes           
            var data = resultats.Select(d => new
            {
                Nom = d.NomAppareil,
                Description = d.DescriptionAppareil,
               

            });
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable("Liste appareils");
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
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Appareils.xlsx");
                }
            }
        }
    }
}
