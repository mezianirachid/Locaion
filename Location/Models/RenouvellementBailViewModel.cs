using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Location.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RenouvellementBailViewModel
    {
        // Identifiant du bail original (pour référence)
        public int BailOriginalId { get; set; }

        // Informations du locataire (lecture seule dans la vue)
        public int LocataireId { get; set; }
        [Display(Name = "Locataire")]
        public string LocataireNomComplet { get; set; }

        // Informations de l'appartement (lecture seule)
        public int AppartementId { get; set; }
        [Display(Name = "Adresse de l'appartement")]
        public string AppartementAdresse { get; set; }

        // Champs modifiables pour le nouveau bail
        [Required(ErrorMessage = "La date de début est requise")]
        [Display(Name = "Nouvelle date de début")]
        [DataType(DataType.Date)]
        public DateTime NouvelleDateDebut { get; set; }

        [Display(Name = "Nouvelle date de fin (optionnelle)")]
        [DataType(DataType.Date)]
        public DateTime? NouvelleDateFin { get; set; }

        [Required(ErrorMessage = "Le loyer est requis")]
        [Display(Name = "Nouveau loyer mensuel")]
        [DataType(DataType.Currency)]
        public decimal NouveauLoyer { get; set; }

        [Display(Name = "Charges mensuelles")]
        [DataType(DataType.Currency)]
        public decimal NouvellesCharges { get; set; }

        // Champ pour les commentaires / conditions particulières (pour la lettre)
        [Display(Name = "Commentaires (pour la lettre)")]
        [DataType(DataType.MultilineText)]
        public string Commentaires { get; set; }
    }
}