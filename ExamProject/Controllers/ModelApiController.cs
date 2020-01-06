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
    public class ModelApiController : ControllerBase
    {
        private readonly CarsRentalContext db;
        public ModelApiController(CarsRentalContext _db)
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
                var models = db.Models.Include(m => m.Marque).Where(p => p.NomModel.Contains(term) || p.Marque.NomMarque.Contains(term))
                    .Select(p => p.NomModel).ToList();
                return Ok(models);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}