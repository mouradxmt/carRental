using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamProject.Helpers;
using ExamProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspaceLocataireApiController : ControllerBase
    {
        private readonly CarsRentalContext db;
        public EspaceLocataireApiController(CarsRentalContext _db)
        {
            db = _db;
        }
        [Authorize(Roles = Roles.Admin + "," + Roles.Executive)]
        [Produces("application/json")]
        [HttpGet("search")]
        public IActionResult Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var annees = db.Voiture.Include(m => m.Marque).Include(m => m.Model).Where(p => p.Annee.Contains(term))
                    .Select(p => p.Annee).ToList();
                return Ok(annees);
            }
            catch
            {
                return BadRequest();
            }
        }
        [Authorize(Roles = Roles.Admin + "," + Roles.Executive)]
        [Produces("application/json")]
        [HttpGet("searchc")]
        public IActionResult Searchc()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var color = db.Voiture.Include(m => m.Marque).Include(m => m.Model).Where(p => p.Couleur.Contains(term))
                    .Select(p => p.Couleur).ToList();
                return Ok(color);
            }
            catch
            {
                return BadRequest();
            }
        }
        //[Produces("application/json")]
        //[HttpGet("searchmk")]
        //public async Task<IActionResult> Searchmk()
        //{
        //    try
        //    {
        //        string term = HttpContext.Request.Query["term"].ToString();
        //        var km = db.Voiture.Include(m => m.Marque).Include(m => m.Model).Where(p => p.Kilometrage.ToString().Contains(term))
        //            .Select(p => p.Kilometrage.ToString()).ToList();
        //        return Ok(km);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}
        //[Produces("application/json")]
        //[HttpGet("searchml")]
        //public IActionResult Searchml()
        //{
        //    try
        //    {
        //        string term = HttpContext.Request.Query["term"].ToString();
        //        var model = db.Models.Include(m => m.Marque).Where(p => p.NomModel.Contains(term) || p.Marque.NomMarque.Contains(term))
        //            .Select(p => p.NomModel).ToList();
        //        return Ok(model);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}
        [Authorize(Roles = Roles.Admin + "," + Roles.Executive)]
        [Produces("application/json")]
        [HttpGet("searchp")]
        public IActionResult Searchp()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var prix = db.Voiture.Include(m => m.Marque).Include(m => m.Model).Where(p => p.PrixParJour.Contains(term))
                    .Select(p => p.PrixParJour).ToList();
                return Ok(prix);
            }
            catch
            {
                return BadRequest();
            }
        }
        [Authorize(Roles = Roles.Admin + "," + Roles.Executive)]
        [Produces("application/json")]
        [HttpGet("searchmk")]
        public IActionResult Searchmk()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var Marque = db.Marques.Where(p => p.NomMarque.Contains(term))
                    .Select(p => p.NomMarque).ToList();
                return Ok(Marque);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}