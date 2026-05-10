using System.Web;
using System.Web.Mvc;
using System.IO;
namespace Location.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFile(int locateurId, HttpPostedFileBase fileSignature)
        {
            try
            {
                if (Request.Files.Count > 0)
            {    // recuperer le fichier et le sauvegarder 
                
                HttpFileCollectionBase files = Request.Files;

                var file = files[0];
                var fileName = Path.GetFileName(file.FileName);
                
                var path = HttpContext.Server.MapPath("~/Accueil/img/") + fileName;
                //ajouter le  fichier dans le repertoire
                file.SaveAs(path);
                //ajouter le  fichier dans la bd

                ViewBag.Message = "File Uploaded Successfully!!";
                   
                }
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
              
            }

            return new JsonResult { Data = new { Statut = true, Message = "" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }
    }
}