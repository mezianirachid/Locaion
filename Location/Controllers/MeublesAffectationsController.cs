using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Location.DAL;
namespace Location.Controllers
{
    [Authorize]
    public class MeublesAffectationsController : Controller
    {
        private ApplicationConnection db = new ApplicationConnection();

        public JsonResult GetAllMeubles()
        {

            var models = db.Meubles;
            var result = models.Select(x => new
            {
                Id = x.Id,
                Nom = x.Nom,
                Description = x.Description,
            }).ToList();

            return new JsonResult { Data = new { Statut = true, Message = "Test message result", ListMeubles = result }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            //if (1 == 1)
            //    return new JsonResult { Data = new { Statut = true, Message = "Les meubles sont ajoutés avec succès" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //else
            //    return new JsonResult { Data = new { Statut = false, Message = "Les meubles n'ont pas été ajoutés" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public JsonResult GetMeublesInclus(int bauxId)
        {
            var models1 = db.InclusionMeubles.Where(x => x.BauxId == bauxId).ToList();
            var models2 = db.Meubles.ToList();
            var results = models1.Select(x => new
                {
                    Id = x.Meubles.Id,
                    Nom = x.Meubles.Nom,
                    NbMeublesInclus = x.NbMeublesInclus == null ? "0" : x.NbMeublesInclus.ToString(),
                    Description = x.Meubles.Description
                }).Union(models2.Select(y => new
                {
                    Id = y.Id,
                    Nom = y.Nom,
                    NbMeublesInclus = "0",
                    Description = y.Description
                }));
            return new JsonResult { Data = new { Statut = true, Message = "Test message result", ListMeubles = results }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}