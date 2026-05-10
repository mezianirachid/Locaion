using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.ComponentModel.DataAnnotations;


namespace Location.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    public class LocatairesVM
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string Courriel { get; set; }
        public string Adresse { get; set; }       

    }

}