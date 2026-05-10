using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Location.Models
{
    public class BauxVM
    {
        public int Id { get; set; }
        public string NumeroBail { get; set; }
        public int AppartementId { get; set; }
        public int LocataireId { get; set; }
        public int LocateurId { get; set; }
        public Nullable<decimal> Prix { get; set; }
        public int ? MoyenPayementId { get; set; }
        public string LieuPayement { get; set; }
        public Nullable<bool> ReglementImmeuble { get; set; }
        [Required(ErrorMessage = "La date de debut du bail est obligatoire")]
        [DataType(DataType.Date, ErrorMessage = "Le format de la date est incorrect")]
        //[Remote("IsValidDateOfBirth", "Validation", HttpMethod = "POST", ErrorMessage = "Please provide a valid date of birth.")]
        [Display(Name = "Date début bail")]
        public Nullable<System.DateTime> DateDebut { get; set; }
        public Nullable<System.DateTime> DateFin { get; set; }
        public Nullable<bool> StationnementExt { get; set; }
        public Nullable<int> NbPlacesExt { get; set; }
        public Nullable<bool> StationnementInt { get; set; }
        public Nullable<int> NbPlacesInt { get; set; }
        public string Emplacement { get; set; }
        public string RemiseEspaceRangenment { get; set; }
        public string Autre { get; set; }
        public Nullable<bool> MeublesInclus { get; set; }
        public Nullable<bool> AppareilsInclus { get; set; }
        public Nullable<bool> Deneigement { get; set; }
        public Nullable<bool> TailleGazon { get; set; }
        public Nullable<decimal> MontantDepot { get; set; }
        public Nullable<System.DateTime> DateOccupation { get; set; }
        public Nullable<System.DateTime> DateRevision { get; set; }
        public string Observation { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<System.DateTime> DatePayement { get; set; }

        public List<int> ListMeubles { get; set; }
    }
}