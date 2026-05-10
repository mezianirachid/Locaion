using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Location.Controllers
{
    [Authorize(Roles = "Admin, Super utilisateur")]
    public class PrioritesController : Controller
    {
        private BDCovidCEMTLEntities db = new BDCovidCEMTLEntities();

        // GET: Priorites
        public ActionResult Index()
        {
            return View(db.Priorites.ToList());
        }

        // GET: Priorites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Priorites priorites = db.Priorites.Find(id);
            if (priorites == null)
            {
                return HttpNotFound();
            }
            return View(priorites);
        }

        // GET: Priorites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Priorites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NomPriorite,DescrPriorite,CouleurPriorite,StatutPriorite")] Priorites priorites)
        {
            if (ModelState.IsValid)
            {
                db.Priorites.Add(priorites);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(priorites);
        }

        // GET: Priorites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Priorites priorites = db.Priorites.Find(id);
            if (priorites == null)
            {
                return HttpNotFound();
            }
            return View(priorites);
        }

        // POST: Priorites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NomPriorite,DescrPriorite,CouleurPriorite,StatutPriorite")] Priorites priorites)
        {
            if (ModelState.IsValid)
            {
                db.Entry(priorites).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(priorites);
        }

        // GET: Priorites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Priorites priorites = db.Priorites.Find(id);
            if (priorites == null)
            {
                return HttpNotFound();
            }
            return View(priorites);
        }

        // POST: Priorites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Priorites priorites = db.Priorites.Find(id);
            db.Priorites.Remove(priorites);
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
