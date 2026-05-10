using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Location.Models
{
    public class Contact
    {
        //custom fields
        [StringLength(100)]
        public string Nom { get; set; }
        [StringLength(100)]
        public string Prenom { get; set; }
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        [StringLength(100)]
        public string Courriel { get; set; }
        [Required(ErrorMessage = "Le champ {0} est obligatoire")]
        [StringLength(100)]
        public string Telephone { get; set; }
        [StringLength(50)]
        public string Sujet { get; set; }

        [StringLength(500)]
        public string Body { get; set; }


        ////To change label title value
        //[DisplayName("Upload File")]
        //public string FilePath { get; set; }

        public HttpPostedFileBase FileContact { get; set; }
        //Remarque importante pour les variables de type HttpPostedFileBase: Tout d'abord, il est possible de télécharger avec Ajax, l'important est que vous devez définir<form enctype= "multipart/form-data" ></ form > sur votre formulaire pour lui dire que votre formulaire a une entrée de téléchargement de fichier.
    }
}