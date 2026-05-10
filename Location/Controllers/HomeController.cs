using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Location.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult KeepSessionAlive()
        {
            return Json(new { success = true, message = "Session active." });
        }

        public ActionResult Default()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "À propos de ImmoLoc.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contactez-nous.";
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public JsonResult SaveApartment()
        {
            try
            {
                string title = (Request.Form["title"] ?? "").Trim();
                string location = (Request.Form["location"] ?? "").Trim();
                string description = (Request.Form["description"] ?? "").Trim();
                string badge = (Request.Form["badge"] ?? "").Trim();

                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(location))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Le titre et l'emplacement sont obligatoires."
                    });
                }

                decimal price;
                int bedrooms;
                int bathrooms;
                int area;

                decimal.TryParse(Request.Form["price"], out price);
                int.TryParse(Request.Form["bedrooms"], out bedrooms);
                int.TryParse(Request.Form["bathrooms"], out bathrooms);
                int.TryParse(Request.Form["area"], out area);

                if (price <= 0 || bedrooms <= 0 || bathrooms <= 0 || area <= 0)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Veuillez saisir des valeurs valides pour le prix, les chambres, les salles de bain et la superficie."
                    });
                }

                string imgFolderPath = Server.MapPath("~/UploadedFiles");
                if (!Directory.Exists(imgFolderPath))
                {
                    Directory.CreateDirectory(imgFolderPath);
                }

                List<string> imageUrls = new List<string>();

                if (Request.Files.Count == 0)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Veuillez téléverser au moins une image."
                    });
                }

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];

                    if (file == null || file.ContentLength <= 0 || string.IsNullOrWhiteSpace(file.FileName))
                    {
                        continue;
                    }

                    if (file.ContentLength > 5 * 1024 * 1024)
                    {
                        return Json(new
                        {
                            success = false,
                            message = "Chaque image doit être inférieure à 5 Mo."
                        });
                    }

                    if (!file.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
                    {
                        return Json(new
                        {
                            success = false,
                            message = "Un des fichiers sélectionnés n'est pas une image valide."
                        });
                    }

                    string extension = Path.GetExtension(file.FileName);
                    if (string.IsNullOrWhiteSpace(extension))
                    {
                        extension = ".jpg";
                    }

                    string uniqueName = Guid.NewGuid().ToString("N") + extension;
                    string fullPath = Path.Combine(imgFolderPath, uniqueName);

                    file.SaveAs(fullPath);
                    imageUrls.Add("/UploadedFiles/" + uniqueName);
                }

                if (!imageUrls.Any())
                {
                    return Json(new
                    {
                        success = false,
                        message = "Aucune image valide n'a été téléversée."
                    });
                }

                var apartments = ReadApartmentsFromFile();

                int newId = apartments.Any() ? apartments.Max(a => a.Id) + 1 : 1;

                Apartment newApartment = new Apartment
                {
                    Id = newId,
                    Title = title,
                    Location = location,
                    Description = description,
                    Price = price,
                    Bedrooms = bedrooms,
                    Bathrooms = bathrooms,
                    Area = area,
                    Badge = badge,
                    ImageUrl = imageUrls.FirstOrDefault(),
                    AllImages = imageUrls,
                    CreatedDate = DateTime.Now
                };

                apartments.Add(newApartment);
                SaveApartmentsToFile(apartments);

                return Json(new
                {
                    success = true,
                    id = newApartment.Id,
                    imageUrl = newApartment.ImageUrl,
                    message = "Appartement sauvegardé avec succès."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Erreur : " + ex.Message
                });
            }
        }

        [HttpGet]
        public JsonResult GetApartments()
        {
            try
            {
                var apartments = ReadApartmentsFromFile()
                    .OrderByDescending(a => a.CreatedDate)
                    .ToList();

                return Json(apartments, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new List<Apartment>(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteApartment(int id)
        {
            try
            {
                var apartments = ReadApartmentsFromFile();
                var apartmentToDelete = apartments.FirstOrDefault(a => a.Id == id);

                if (apartmentToDelete == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Appartement non trouvé."
                    });
                }

                if (apartmentToDelete.AllImages != null && apartmentToDelete.AllImages.Any())
                {
                    foreach (var imageUrl in apartmentToDelete.AllImages)
                    {
                        DeletePhysicalImage(imageUrl);
                    }
                }
                else if (!string.IsNullOrWhiteSpace(apartmentToDelete.ImageUrl))
                {
                    DeletePhysicalImage(apartmentToDelete.ImageUrl);
                }

                apartments.Remove(apartmentToDelete);
                SaveApartmentsToFile(apartments);

                return Json(new
                {
                    success = true,
                    message = "Appartement supprimé avec succès."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Erreur : " + ex.Message
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendEmail(string name, string email, string phone, string message)
        {
            try
            {
                name = (name ?? "").Trim();
                email = (email ?? "").Trim();
                phone = (phone ?? "").Trim();
                message = (message ?? "").Trim();

                if (string.IsNullOrWhiteSpace(name) ||
                    string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(phone) ||
                    string.IsNullOrWhiteSpace(message))
                {
                    return Json(new { success = false, message = "Tous les champs sont obligatoires." });
                }

                SmtpSection section = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                string to = "contact@gestionlocations.com";
                string from = section.From;

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(from),
                    Subject = "Nouveau message depuis le formulaire de contact",
                    Body =
                        "Nom : " + name + Environment.NewLine +
                        "Courriel : " + email + Environment.NewLine +
                        "Téléphone : " + phone + Environment.NewLine + Environment.NewLine +
                        "Message :" + Environment.NewLine + message,
                    BodyEncoding = Encoding.UTF8,
                    SubjectEncoding = Encoding.UTF8,
                    IsBodyHtml = false
                };

                mailMessage.To.Add(to);
                mailMessage.ReplyToList.Add(new MailAddress(email));

                using (var smtpClient = new SmtpClient(section.Network.Host, section.Network.Port))
                {
                    smtpClient.Credentials = new NetworkCredential(section.Network.UserName, section.Network.Password);
                    smtpClient.EnableSsl = section.Network.EnableSsl;
                    smtpClient.Send(mailMessage);
                }

                return Json(new { success = true, message = "Message envoyé avec succès." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Erreur : " + ex.Message });
            }
        }

        private List<Apartment> ReadApartmentsFromFile()
        {
            string jsonFilePath = Server.MapPath("~/App_Data/apartments.json");
            string dataFolder = Path.GetDirectoryName(jsonFilePath);

            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }

            if (!System.IO.File.Exists(jsonFilePath))
            {
                return new List<Apartment>();
            }

            string json = System.IO.File.ReadAllText(jsonFilePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Apartment>();
            }

            return JsonConvert.DeserializeObject<List<Apartment>>(json) ?? new List<Apartment>();
        }

        private void SaveApartmentsToFile(List<Apartment> apartments)
        {
            string jsonFilePath = Server.MapPath("~/App_Data/apartments.json");
            string json = JsonConvert.SerializeObject(apartments, Formatting.Indented);
            System.IO.File.WriteAllText(jsonFilePath, json);
        }

        private void DeletePhysicalImage(string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(imageUrl) || !imageUrl.StartsWith("/UploadedFiles/"))
            {
                return;
            }

            string imagePath = Server.MapPath("~" + imageUrl);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }
    }

    public class Apartment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Area { get; set; }
        public string Badge { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<string> AllImages { get; set; }
        public DateTime CreatedDate { get; set; }

        public Apartment()
        {
            AllImages = new List<string>();
        }
    }
}