using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Location.Models
{
    public class UserEdit
    {
        public string UserId { get; set; }

        [Display(Name = "Nom utilisateur")]
        public string Username { get; set; }

        [Display(Name = "Nom")]
        public string FirstName { get; set; }


        [Display(Name = "Prénom")]
        public string LastName { get; set; }

        [Display(Name = "Courriel")]
        public string Email { get; set; }

        [Display(Name = "Téléphone")]
        public string Telephone { get; set; }

        public string Role { get; set; }


        [Required]
        [Display(Name = "User Roles")]
        public string UserRoles { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool LockoutEnabled { get; set; }

       
    }
}