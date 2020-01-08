using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamProject.Models;
using ExamProject.Models.viewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using ExamProject.Helpers;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;

namespace ExamProject.Controllers
{
    
    public class ModelsController : Controller
    {
        private readonly IStringLocalizer<ModelsController> _localizer;

        private readonly CarsRentalContext _context;
        [BindProperty]
        public ModelVM VM { get; set; }
        public ModelsController(CarsRentalContext context, IStringLocalizer<ModelsController> localizer)
        {
            _context = context;
            _localizer = localizer;
            VM = new ModelVM()
            {
                Marques = _context.Marques.ToList(),
                Model = new Models.Model()
            };
        }

        [Authorize(Roles = Roles.Admin)]
        public IActionResult Index(string models)
        {
            if (models != null)
            {
                var marquedata = _context.Models.Where(p => (p.NomModel.Contains(models)) || p.Marque.NomMarque.Contains(models)).ToList();
                return View(marquedata);
            }
            else
            {
                var model = _context.Models.Include(m => m.Marque);
                return View(model.ToList());
            }
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize(Roles = Roles.Admin)]
        public List<Marque> FillSelectList()
        {
            var makes = _context.Marques.ToList();
            makes.Insert(0, new Marque { Id = -1, NomMarque = _localizer["--- Please select a Make--"] });
            return makes;
        }
        [Authorize(Roles = Roles.Admin)]
        ModelVM GetAllMakes()
        {
            var vmodel = new ModelVM
            {
                Marques = FillSelectList()
            };
            return vmodel;
        }
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Create()
        {
            return View(GetAllMakes());
        }

        // POST: Models/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Create(IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View(VM);
            }
            _context.Models.Add(VM.Model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Roles.Admin)]
        public IActionResult Edit(int id)
        {
            VM.Model = _context.Models.Include(m => m.Marque).SingleOrDefault(m => m.Id == id);
            if (VM.Model == null)
            {
                return NotFound();
            }

            return View(VM);
        }

        // POST: Models/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public IActionResult Edit()
        {
            if (!ModelState.IsValid)
            {
                return View(VM);
            }
            _context.Update(VM.Model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Model model = _context.Models.Find(id);
            _context.Models.Remove(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //private bool ModelExists(int id)
        //{
        //    return _context.Models.Any(e => e.Id == id);
        //}
        [Authorize(Roles = Roles.Admin + "," + Roles.Executive)]
        [HttpGet("api/models/{MarqueID}")]
        public IEnumerable<Model> Models(int MarqueID)
        {
            return _context.Models.ToList()
            .Where(m => m.MarqueID == MarqueID);
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
