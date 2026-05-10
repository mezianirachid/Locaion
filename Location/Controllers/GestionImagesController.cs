using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Location.Controllers
{
    [Authorize]
    public class GestionImagesController : Controller
    {
        // GET: FestionImages

        public ActionResult Achat()
        {           
            return View();
        }
        [HttpPost]
        public ActionResult Achat(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    var fileName = Path.GetFileName(file.FileName);
                    var path = HttpContext.Server.MapPath("~/Accueil/img/") + fileName;
                    //ajouter le  fichier dans le repertoire
                    file.SaveAs(path);
                    //ajouter le  fichier dans la bd
                    ViewBag.Message = "Fichier téléversé avec succès!!"; 
                    string pathName = "~/Accueil/img/" + fileName;
                    ViewBag.PathName = pathName;
                   
                }
                catch
                {
                    ViewBag.Message = "Echec de téléversement du fichier!!";

                }
            }

            return View();
        }

        public ActionResult Location()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Location(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    var fileName = Path.GetFileName(file.FileName);
                    var path = HttpContext.Server.MapPath("~/Accueil/img/") + fileName;
                    //ajouter le  fichier dans le repertoire
                    file.SaveAs(path);
                    //ajouter le  fichier dans la bd
                    ViewBag.Message = "Fichier téléversé avec succès!!";
                    string pathName = "~/Accueil/img/" + fileName;
                    ViewBag.PathName = pathName;
                }
                catch
                {
                    ViewBag.Message = "Echec de téléversement du fichier!!";

                }
            }

            return View();
        }
    }
}