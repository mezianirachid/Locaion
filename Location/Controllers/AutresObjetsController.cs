using ClosedXML.Excel;
using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Location.DAL;
namespace Location.Controllers
{
    [Authorize]
    public class AutresObjetsController : Controller
    {
        private ApplicationConnection db = new ApplicationConnection();
        private string userNameConnected = System.Web.HttpContext.Current.User.Identity.Name;
        // GET: AutresObjets
        public ActionResult Index()
        {
            return View(db.AutresObjets.ToList());
        }

        // GET: AutresObjets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutresObjets autresObjets = db.AutresObjets.Find(id);
            if (autresObjets == null)
            {
                return HttpNotFound();
            }
            return View(autresObjets);
        }

        // GET: AutresObjets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutresObjets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,Description")] AutresObjets autresObjets)
        {
            if (ModelState.IsValid)
            {
                autresObjets.CreatedBy = userNameConnected;
                autresObjets.CreatedOn = DateTime.Now;
                db.AutresObjets.Add(autresObjets);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(autresObjets);
        }

        // GET: AutresObjets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutresObjets autresObjets = db.AutresObjets.Find(id);
            if (autresObjets == null)
            {
                return HttpNotFound();
            }
            return View(autresObjets);
        }

        // POST: AutresObjets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,Description")] AutresObjets autresObjets)
        {
            if (ModelState.IsValid)
            {
                autresObjets.ModifiedBy = userNameConnected;
                autresObjets.ModifiedOn = DateTime.Now;
                db.Entry(autresObjets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autresObjets);
        }

        // GET: AutresObjets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AutresObjets autresObjets = db.AutresObjets.Find(id);
            if (autresObjets == null)
            {
                return HttpNotFound();
            }
            return View(autresObjets);
        }

        // POST: AutresObjets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AutresObjets autresObjets = db.AutresObjets.Find(id);
            db.AutresObjets.Remove(autresObjets);
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
            AutresObjets item = db.AutresObjets.Find(id);
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
            AutresObjets item = db.AutresObjets.Find(id);
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
            var resultats = db.AutresObjets.OrderBy(d => d.Nom);//Declaration de la variable demandes           
            var data = resultats.Select(d => new
            {
                Nom = d.Nom,
                Description = d.Description,
               

            });
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable("Liste autres inclusions");
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
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Autres inclusions.xlsx");
                }
            }
        }
    }
}
