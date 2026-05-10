using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Location.Models; using Location.DAL;          // Namespace de vos entités (d'après l'edmx)


using MailKit.Net.Imap;
using MailKit.Search;
using System.Configuration;
using MailKit;
// ... vos autres usings
namespace Location.Controllers
{
    [Authorize(Roles = "Admin")]   // Seuls les administrateurs peuvent accéder à ces actions
    public class AdminController : Controller
    {
        private ApplicationConnection db = new ApplicationConnection(); // Contexte EF

        [AllowAnonymous]
        public JsonResult GetApartments()
        {
            var appartements = db.Appartements
                .Where(a => a.AppartementImages.Any())
                .Select(a => new
                {
                    a.Id,
                    a.Titre,
                    a.Description,
                    a.PrixPublic,
                    a.Superficie,
                    NbEtages = a.NbEtages,
                    a.NbSalleBain,
                    a.AdresseAppartement,
                    a.Badge,
                    a.StatutAppartement,
            // Image principale (première)
            Images = a.AppartementImages.OrderBy(i => i.IsPrimary ? 0 : 1).Select(i => i.ImagePath).FirstOrDefault(),
                    ImagesCount = a.AppartementImages.Count(),
            // Liste complète des images (pour la galerie)
            AllImages = a.AppartementImages.Select(i => i.ImagePath).ToList()
                })
                .ToList();

            return Json(appartements, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/GetApartment/5
        public JsonResult GetApartment(int id)
        {
            var appartement = db.Appartements
                .Where(a => a.Id == id)
                .Select(a => new
                {
                    a.Id,
                    a.Titre,
                    a.Description,
                    a.PrixPublic,
                    a.Superficie,
                    a.NbEtages,
                    a.NbSalleBain,
                    a.AdresseAppartement,
                    a.Badge,
                    a.StatutAppartement,
            // Toutes les images (chemins)
            Images = a.AppartementImages.Select(i => i.ImagePath).ToList()
                })
                .FirstOrDefault();

            if (appartement == null)
                return Json(new { success = false, message = "Appartement introuvable." }, JsonRequestBehavior.AllowGet);

            return Json(appartement, JsonRequestBehavior.AllowGet);
        }

      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddApartment(Appartements model, IEnumerable<HttpPostedFileBase> imageFiles)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { success = false, message = "Données invalides : " + string.Join(", ", errors) });
            }

            try
            {
                // Sauvegarde de l'appartement d'abord pour obtenir l'Id
                model.CreatedBy = User.Identity.Name;
                model.CreatedOn = DateTime.Now;
                model.ModifiedBy = User.Identity.Name;
                model.ModifiedOn = DateTime.Now;

                db.Appartements.Add(model);
                db.SaveChanges();

                // Traitement des images
                if (imageFiles != null && imageFiles.Any())
                {
                    bool isFirst = true;
                    foreach (var file in imageFiles)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                            var folder = Server.MapPath("~/UploadedFiles");
                            if (!Directory.Exists(folder))
                                Directory.CreateDirectory(folder);

                            var path = Path.Combine(folder, fileName);
                            file.SaveAs(path);

                            var image = new AppartementImages
                            {
                                AppartementId = model.Id,
                                ImagePath = "/UploadedFiles/" + fileName,
                                IsPrimary = isFirst, // première image = principale
                                CreatedOn = DateTime.Now
                            };
                            db.AppartementImages.Add(image);
                            isFirst = false;
                        }
                    }
                    db.SaveChanges();
                }

                return Json(new { success = true, message = "Annonce ajoutée avec succès." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erreur lors de l'ajout : " + ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditApartment(Appartements model, IEnumerable<HttpPostedFileBase> imageFiles)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { success = false, message = "Données invalides : " + string.Join(", ", errors) });
            }

            try
            {
                var existing = db.Appartements.Find(model.Id);
                if (existing == null)
                    return Json(new { success = false, message = "Appartement introuvable." });

                // Mise à jour des champs
                existing.Titre = model.Titre;
                existing.Description = model.Description;
                existing.PrixPublic = model.PrixPublic;
                existing.Superficie = model.Superficie;
                existing.NbEtages = model.NbEtages;
                existing.NbSalleBain = model.NbSalleBain;
                existing.AdresseAppartement = model.AdresseAppartement;
                existing.Badge = model.Badge;
                existing.StatutAppartement = model.StatutAppartement;
                existing.ModifiedBy = User.Identity.Name;
                existing.ModifiedOn = DateTime.Now;

                // Ajout des nouvelles images
                if (imageFiles != null && imageFiles.Any())
                {
                    bool isFirst = !db.AppartementImages.Any(i => i.AppartementId == model.Id);
                    foreach (var file in imageFiles)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                            var folder = Server.MapPath("~/UploadedFiles");
                            if (!Directory.Exists(folder))
                                Directory.CreateDirectory(folder);

                            var path = Path.Combine(folder, fileName);
                            file.SaveAs(path);

                            var image = new AppartementImages
                            {
                                AppartementId = model.Id,
                                ImagePath = "/UploadedFiles/" + fileName,
                                IsPrimary = isFirst, // si c'est la première image de l'appartement, elle devient principale
                                CreatedOn = DateTime.Now
                            };
                            db.AppartementImages.Add(image);
                            isFirst = false;
                        }
                    }
                }

                db.SaveChanges();
                return Json(new { success = true, message = "Annonce modifiée avec succès." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erreur lors de la modification : " + ex.Message });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteApartment(int id)
        {
            try
            {
                var appartement = db.Appartements.Include(a => a.AppartementImages).FirstOrDefault(a => a.Id == id);
                if (appartement == null)
                    return Json(new { success = false, message = "Appartement introuvable." });

                // Supprimer les fichiers physiques
                foreach (var image in appartement.AppartementImages)
                {
                    var path = Server.MapPath("~" + image.ImagePath);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }

                db.Appartements.Remove(appartement); // Les images seront supprimées par cascade ou manuellement si vous avez désactivé la cascade
                db.SaveChanges();

                return Json(new { success = true, message = "Annonce supprimée avec succès." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erreur lors de la suppression : " + ex.Message });
            }
        }


 
    public JsonResult GetUnreadEmailCount()
    {
        try
        {
            // Lecture des paramètres en clair depuis web.config
            string server = ConfigurationManager.AppSettings["EmailServer"];
            int port = int.Parse(ConfigurationManager.AppSettings["EmailPort"]);
            bool useSsl = bool.Parse(ConfigurationManager.AppSettings["EmailUseSsl"]);
            string username = ConfigurationManager.AppSettings["EmailUsername"];
            string password = ConfigurationManager.AppSettings["EmailPassword"];

            using (var client = new ImapClient())
            {
                // Connexion au serveur IMAP
                client.Connect(server, port, useSsl);
                client.Authenticate(username, password);

                // Ouvrir la boîte de réception
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                // Rechercher les messages non lus
                var query = SearchQuery.NotSeen;
                var uids = inbox.Search(query);
                int unreadCount = uids.Count;

                client.Disconnect(true);
                return Json(unreadCount, JsonRequestBehavior.AllowGet);
            }
        }
        catch (Exception )
        {
            // En cas d'erreur (mauvais identifiants, serveur injoignable...)
            // Retourne -1 pour indiquer une erreur (vous pouvez afficher un badge grisé côté client)
            return Json(-1, JsonRequestBehavior.AllowGet);
        }
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