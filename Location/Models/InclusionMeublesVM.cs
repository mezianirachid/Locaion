using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Location.Models
{
    public class InclusionMeublesVM
    {
        public int Id { get; set; }         
        public int BauxId { get; set; }
        public int MeubleId { get; set; }
        public int MeubleNom { get; set; }
        public int NbMeublesInclus { get; set; }
        public string Observation { get; set; }
    }
}