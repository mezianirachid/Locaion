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
    public class MoyenPayementsController : Controller
    {
        private ApplicationConnection db = new ApplicationConnection();
        private string userNameConnected = System.Web.HttpContext.Current.User.Identity.Name;
        // GET: MoyenPayements
        public ActionResult Index()
        {
            return View(db.MoyenPayements.ToList());
        }

        // GET: MoyenPayements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoyenPayements moyenPayements = db.MoyenPayements.Find(id);
            if (moyenPayements == null)
            {
                return HttpNotFound();
            }
            return View(moyenPayements);
        }

        // GET: MoyenPayements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MoyenPayements/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NomMoyenPayement,DescriptionMoyenPayement,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,StatutMoyenPayement")] MoyenPayements moyenPayements)
        {
            if (ModelState.IsValid)
            {
                moyenPayements.CreatedBy = userNameConnected;
                moyenPayements.CreatedOn = DateTime.Now;
                db.MoyenPayements.Add(moyenPayements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(moyenPayements);
        }

        // GET: MoyenPayements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoyenPayements moyenPayements = db.MoyenPayements.Find(id);
            if (moyenPayements == null)
            {
                return HttpNotFound();
            }
            return View(moyenPayements);
        }

        // POST: MoyenPayements/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NomMoyenPayement,DescriptionMoyenPayement,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,StatutMoyenPayement")] MoyenPayements moyenPayements)
        {
            if (ModelState.IsValid)
            {
                moyenPayements.ModifiedBy = userNameConnected;
                moyenPayements.ModifiedOn = DateTime.Now;
                db.Entry(moyenPayements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(moyenPayements);
        }

        // GET: MoyenPayements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoyenPayements moyenPayements = db.MoyenPayements.Find(id);
            if (moyenPayements == null)
            {
                return HttpNotFound();
            }
            return View(moyenPayements);
        }

        // POST: MoyenPayements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MoyenPayements moyenPayements = db.MoyenPayements.Find(id);
            db.MoyenPayements.Remove(moyenPayements);
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
            Locataires item = db.Locataires.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                item.StatutLocataire = "A";
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
            Locataires item = db.Locataires.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                item.StatutLocataire = "I";
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public ActionResult ExportToExcel()
        {
            var resultats = db.MoyenPayements.OrderBy(d => d.NomMoyenPayement);//Declaration de la variable demandes           
            var data = resultats.Select(d => new
            {
                Nom = d.NomMoyenPayement,
                Description = d.DescriptionMoyenPayement,
              

            });
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable("Liste moyens de payements");
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
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Moyens de payements.xlsx");
                }
            }
        }
    }
}
