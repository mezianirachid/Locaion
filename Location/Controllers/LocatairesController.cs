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
    public class LocatairesController : Controller
    {
        private ApplicationConnection db = new ApplicationConnection();
        private string userNameConnected = System.Web.HttpContext.Current.User.Identity.Name;
        // GET: Locataires
        public ActionResult Index()
        {
            return View(db.Locataires.ToList());
        }

        // GET: Locataires/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locataires locataires = db.Locataires.Find(id);
            if (locataires == null)
            {
                return HttpNotFound();
            }
            return View(locataires);
        }

        // GET: Locataires/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locataires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Locataires locataires)
        {
            if (ModelState.IsValid)
            {
                locataires.CreatedBy = userNameConnected;
                locataires.CreatedOn = DateTime.Now;
                db.Locataires.Add(locataires);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(locataires);
        }

        // GET: Locataires/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locataires locataires = db.Locataires.Find(id);
            if (locataires == null)
            {
                return HttpNotFound();
            }
            return View(locataires);
        }

        // POST: Locataires/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Locataires locataires)
        {
            if (ModelState.IsValid)
            {
                locataires.ModifiedBy = userNameConnected;
                locataires.ModifiedOn = DateTime.Now;
                db.Entry(locataires).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locataires);
        }

        // GET: Locataires/Delete/5
        // GET: Locataires/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locataires locataires = db.Locataires.Find(id);
            if (locataires == null)
            {
                return HttpNotFound();
            }

            // Récupérer tous les baux où le locataire est impliqué (Locataire principal ou co-locataire)
            var bauxAssocies = db.Baux.Where(b => b.LocataireId == id || b.CoLocataireId == id).ToList();
            ViewBag.BauxAssocies = bauxAssocies;

            return View(locataires);
        }

        // POST: Locataires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // POST: Locataires/Delete/5    
        public ActionResult DeleteConfirmed(int id)
        {
            Locataires locataire = db.Locataires.Find(id);
            if (locataire == null)
            {
                return HttpNotFound();
            }

            // Vérifier les références dans Baux (LocataireId ou CoLocataireId)
            bool aDesBaux = db.Baux.Any(b => b.LocataireId == id || b.CoLocataireId == id);
            if (aDesBaux)
            {
                // Recharger la liste des baux pour l'affichage
                var bauxAssocies = db.Baux.Where(b => b.LocataireId == id || b.CoLocataireId == id).ToList();
                ViewBag.BauxAssocies = bauxAssocies;

                ModelState.AddModelError("", "Impossible de supprimer ce locataire car il est associé à un ou plusieurs baux.");
                return View("Delete", locataire);
            }

            db.Locataires.Remove(locataire);
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
            var resultats = db.Locataires.OrderBy(d => d.NomLocataire);//Declaration de la variable demandes           
            var data = resultats.Select(d => new
            {
                Nom = d.NomLocataire,
                Prenom = d.PrenomLocataire,
                Tel = d.TeLocataire,
                Courriel = d.CourrielLocataire,                

            });
            using (XLWorkbook wb = new XLWorkbook())
            {
                DataTable dt = new DataTable("Liste locataires");
                dt.Columns.AddRange(new DataColumn[4] {
                new DataColumn("Nom"),
                new DataColumn("Prenom"),
                new DataColumn("Téléphonee"),              
                new DataColumn("Courriel responsable "),
                
            });
                foreach (var item in data)
                {
                    dt.Rows.Add(item.Nom,
                                item.Prenom,
                                item.Tel,
                                item.Courriel                              
                                );
                }

                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Locataires.xlsx");
                }
            }
        }
    }
}
