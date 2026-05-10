using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace Location.Controllers
{
    [Authorize(Roles = "Admin, Super utilisateur")]
    public class TypeUsagersController : Controller
    {
        private BDCovidCEMTLEntities db = new BDCovidCEMTLEntities();

        // GET: TypeUsagers
        public ActionResult Index()
        {
            return View(db.TypeUsagers.ToList());
        }

        // GET: TypeUsagers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeUsagers typeUsagers = db.TypeUsagers.Find(id);
            if (typeUsagers == null)
            {
                return HttpNotFound();
            }
            return View(typeUsagers);
        }

        // GET: TypeUsagers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TypeUsagers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NomTypeUsager,DescTypeUsager")] TypeUsagers typeUsagers)
        {
            if (ModelState.IsValid)
            {
                db.TypeUsagers.Add(typeUsagers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeUsagers);
        }

        // GET: TypeUsagers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeUsagers typeUsagers = db.TypeUsagers.Find(id);
            if (typeUsagers == null)
            {
                return HttpNotFound();
            }
            return View(typeUsagers);
        }

        // POST: TypeUsagers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NomTypeUsager,DescTypeUsager")] TypeUsagers typeUsagers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeUsagers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeUsagers);
        }

        // GET: TypeUsagers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypeUsagers typeUsagers = db.TypeUsagers.Find(id);
            if (typeUsagers == null)
            {
                return HttpNotFound();
            }
            return View(typeUsagers);
        }

        // POST: TypeUsagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TypeUsagers typeUsagers = db.TypeUsagers.Find(id);
            db.TypeUsagers.Remove(typeUsagers);
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
