
using Location.Models; using Location.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Security.Claims;
using System.Security.Principal;

namespace Location.Controllers
{
    [Authorize]
    public class DownloadFileController : Controller
    {

        private ApplicationConnection db = new ApplicationConnection();
       
        
        public ActionResult DownloadFile(string nomDoc)
        {
            string nomDocument = nomDoc;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadedFiles/");
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + nomDocument);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, nomDocument);


        }


    }
}