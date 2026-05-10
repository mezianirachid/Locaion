using System.Web;
using Location.Class;
using System.ComponentModel.DataAnnotations;


namespace Location.Models
{
    public class ImportExcel
    {
        [Required(ErrorMessage = "Veuillez selectionner votre fichier Excel")]
        [FileExt(Allow = ".xls,.xlsx", ErrorMessage = "Seules sont acceptés les fichiers excels ayant l'extention xls ou xlsx.")]
        public HttpPostedFileBase file { get; set; }
    }
}