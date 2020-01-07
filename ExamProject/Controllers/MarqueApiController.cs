using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamProject.Helpers;
using ExamProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarqueApiController : ControllerBase
    {
        private readonly CarsRentalContext db;
        public MarqueApiController(CarsRentalContext _db)
        {
            db = _db;
        }
        [Authorize(Roles = Roles.Admin + "," + Roles.Executive)]
        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var marques = db.Marques.Where(p => p.NomMarque.Contains(term))
                    .Select(p => p.NomMarque).ToList();
                return Ok(marques);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}