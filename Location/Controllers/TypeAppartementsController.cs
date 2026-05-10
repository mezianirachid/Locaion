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
    public class TypeAppartementsController : Controller
    {
        private ApplicationConnection db = new ApplicationConnection();
        private string userNameConnected = System.Web.HttpContext.Current.User.Identity.Name;
        // GET: TypeAppartements
        public ActionResult Index()
        {
            return View(db.TypeAppartements.ToList());
        }

        // GET: TypeAppartements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeAppartements typeAppartements = db.TypeAppartements.Find(id);
            if (typeAppartements == null)
            {
                return HttpNotFound();
            }
            return View(typeAppartements);
        }

        // GET: TypeAppartements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeAppartements/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NomTypeAppartement,DescriptionTypeAppartement,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,StatutTypeAppartement")] TypeAppartements typeAppartements)
        {
            if (ModelState.IsValid)
            {
                typeAppartements.CreatedBy = userNameConnected;
                typeAppartements.CreatedOn = DateTime.Now;
                db.TypeAppartements.Add(typeAppartements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeAppartements);
        }

        // GET: TypeAppartements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeAppartements typeAppartements = db.TypeAppartements.Find(id);
            if (typeAppartements == null)
            {
                return HttpNotFound();
            }
            return View(typeAppartements);
        }

        // POST: TypeAppartements/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NomTypeAppartement,DescriptionTypeAppartement,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,StatutTypeAppartement")] TypeAppartements typeAppartements)
        {
            if (ModelState.IsValid)
            {
                typeAppartements.ModifiedBy = userNameConnected;
                typeAppartements.ModifiedOn = DateTime.Now;
                db.Entry(typeAppartements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeAppartements);
        }

        // GET: TypeAppartements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeAppartements typeAppartements = db.TypeAppartements.Find(id);
            if (typeAppartements == null)
            {
                return HttpNotFound();
            }
            return View(typeAppartements);
        }

        // POST: TypeAppartements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeAppartements typeAppartements = db.TypeAppartements.Find(id);
            db.TypeAppartements.Remove(typeAppartements);
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
            TypeAppartements item = db.TypeAppartements.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                item.StatutTypeAppartement = "A";
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
            TypeAppartements item = db.TypeAppartements.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            else
            {
                item.StatutTypeAppartement = "I";
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult ExportToExcel()
        {
            var resultats = db.TypeAppartements.OrderBy(d => d.NomTypeAppartement);//Declaration de la variable demandes           
            var data = resultats.Select(d => new
            {
                Nom = d.NomTypeAppartement,
                Description = d.DescriptionTypeAppartement
            });
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable("Liste types appartements");
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
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Types appartements.xlsx");
                }
            }
        }
    }
}
