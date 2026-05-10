using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Location.Controllers
{
    [Authorize(Roles = "Admin, Super utilisateur")]
    public class SitesController : Controller
    {
        private BDCovidCEMTLEntities db = new BDCovidCEMTLEntities();
        private string userNameConnected = System.Web.HttpContext.Current.User.Identity.Name;
        // GET: Sites
        public ActionResult Index()
        {
            return View(db.Sites.ToList());
        }

        // GET: Sites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sites sites = db.Sites.Find(id);
            if (sites == null)
            {
                return HttpNotFound();
            }
            return View(sites);
        }

        // GET: Sites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NomSite,EmplacementSite,TelephoneSite")] Sites sites)
        {
            if (ModelState.IsValid)
            {
                db.Sites.Add(sites);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sites);
        }

        // GET: Sites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sites sites = db.Sites.Find(id);
            if (sites == null)
            {
                return HttpNotFound();
            }
            return View(sites);
        }

        // POST: Sites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NomSite,EmplacementSite,TelephoneSite")] Sites sites)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sites).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sites);
        }

        // GET: Sites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sites sites = db.Sites.Find(id);
            if (sites == null)
            {
                return HttpNotFound();
            }
            return View(sites);
        }

        // POST: Sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sites sites = db.Sites.Find(id);
            db.Sites.Remove(sites);
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
