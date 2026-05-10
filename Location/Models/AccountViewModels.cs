using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Location.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le champ {0} est obligatoire")]
        public string Provider { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le champ {0} est obligatoire")]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Se souvenir de moi?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Courriel")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le champ {0} est obligatoire")]
        [Display(Name = "Nom utilisateur")]

		public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le champ {0} est obligatoire")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Retenir mon compte?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le champ {0} est obligatoire")]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le champ {0} est obligatoire")]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Téléphone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Veuillez entrer un numéro de téléphone valide.")]
        public string Telephone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le champ {0} est obligatoire")]
        [EmailAddress]
		[Display(Name = "Courriel")]
		public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le champ {0} est obligatoire")]
        [Display(Name = "Nom utilisateur")]
		public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le champ {0} est obligatoire")]
        [StringLength(100, ErrorMessage = "Le champ {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer mot de passe")]
        [Compare("Password", ErrorMessage = "Les mots de passe doivent être identiques.")]
        public string ConfirmPassword { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Le champ {0} est obligatoire")]
        [Display(Name = "UserRoles")]
        public string UserRoles { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le champ {0} est obligatoire")]
        [EmailAddress]
        [Display(Name = "Courriel")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le champ {0} est obligatoire")]
        [StringLength(100, ErrorMessage = "Le champ {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer mot de passe")]
        [Compare("Password", ErrorMessage = "Les mots de passe doivent être identiques")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le champ {0} est obligatoire")]
        [EmailAddress]
        [Display(Name = "Courriel")]
        public string Email { get; set; }
    }
}
