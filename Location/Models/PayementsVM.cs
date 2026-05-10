using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.ComponentModel.DataAnnotations;


namespace Location.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    public class PayementsVM
    {
        public int NumeroFacture { get; set; }        
        public string Adresse { get; set; }
        public string NomCompletLocataire { get; set; }
        public string NomCompletColocataire { get; set; }
        public Nullable<int> Annee { get; set; }
        public Nullable<int> Mois { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Le format de la date est incorrect")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [Display(Name = "Date payement")]
        public DateTime DatePayement { get; set; }
        public string LieuPayement { get; set; }
        public Nullable<decimal> Prix { get; set; }
        public Nullable<decimal> Montant { get; set; }
        public string NomMoyenPayement { get; set; }
        public string Description { get; set; }

        public string DatePayementFormated { get; set; }

        public PayementsVM()
        {
            DatePayementFormated = DatePayement.ToShortDateString(); 
        }


        

    }

}