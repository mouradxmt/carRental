using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamProject.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting.Internal;
using ExamProject.Models.viewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using ExamProject.Helpers;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;

namespace ExamProject.Controllers
{
    [Authorize(Roles = Roles.Admin + "," + Roles.Executive)]
    public class VoituresController : Controller
    {
        private readonly CarsRentalContext _context;
        private readonly HostingEnvironment _hostingEnvironment;
        [BindProperty]
        public CarViewModel VM { get; set; }
        private readonly IStringLocalizer<VoituresController> _localizer;
        public VoituresController(CarsRentalContext context ,HostingEnvironment hostingEnvironment, IStringLocalizer<VoituresController> localizer)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            VM = new CarViewModel()
            {
                Marques = _context.Marques.ToList(),
                Models = _context.Models.ToList(),
                Voiture = new Models.Voiture(),
            };
            _localizer = localizer;
        }

        // GET: Voitures
        public IActionResult Index(string annees, string color, string Marque, string prix)
        {
            var currentUser = _context.applicationUsers.Where(a => a.UserName == User.Identity.Name).First();
            if (annees != null || color != null || Marque != null || prix != null)  /*Contains("")*/
            {
                if (color == null)
                {
                    color = "";
                }
                if (Marque == null)
                {
                    Marque = "";
                }
                if (annees == null)


                {
                    annees = "";
                }
                if (prix == null)
                {
                    prix = "";
                }
                var marquedata = _context.Voiture.Where(p => p.Annee.Contains(annees) && p.Couleur.Contains(color) && p.Marque.NomMarque.Contains(Marque) && p.PrixParJour.Contains(prix)).ToList();
                //var marquedata = _context.Voiture.Where(A) ;
                return View(marquedata);
            }
            else
            {
                var voitures = _context.Voiture.Include(m => m.Marque).Include(m => m.Model).Where(v => v.ProprietaireId == currentUser.ProprietaireId);
                return View(voitures);
            }

        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiture = await _context.Voiture
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voiture == null)
            {
                return NotFound();
            }

            return View(voiture);
        }
        public List<Marque> FillSelectList()
        {
            var makes = _context.Marques.ToList();
            makes.Insert(0, new Marque { Id = -1, NomMarque = "--- Veuillez selectionner une marque--" });
            return makes;
        }
        public List<Model> FillSelectListModels()
        {
            var models = _context.Models.ToList();
            models.Insert(0, new Model { Id = -1, NomModel = "--- Veuillez selectionner un model--" });
            return models;
        }
        CarViewModel GetAllMakes()
        {
            var vmodel = new CarViewModel
            {
                Marques = FillSelectList(),
                Models = FillSelectListModels()
            };
            return vmodel;
        }
        // GET: Voitures/Create
        public IActionResult Create()
        {
            return View(GetAllMakes());
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View(VM);
            }
            var currentUser = _context.applicationUsers.Where(a => a.UserName == User.Identity.Name).First();
            VM.Voiture.ProprietaireId =  currentUser.ProprietaireId ;
            VM.Voiture.EstDisponible = true;
            _context.Voiture.Add(VM.Voiture);
            UploadImage();
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Voitures/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            VM.Voiture = _context.Voiture.SingleOrDefault(b => b.Id == id);
            VM.Models = _context.Models.Where(m => m.MarqueID == VM.Voiture.MarqueID);
            if (VM.Voiture == null)
            {
                return NotFound();
            }
            return View(VM);
        }

        [HttpPost]
        public IActionResult Edit()
        {
            if (!ModelState.IsValid)
            {
                VM.Marques = _context.Marques.ToList();
                VM.Models = _context.Models.ToList();
                return View(VM);
            }
            if (VM.Voiture.ModelID == 0)
            {
                ViewBag.Message = "Please select a Model from the list";
                return View(VM);
            }
            _context.Voiture.Update(VM.Voiture);
            UploadImage();
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Voitures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiture = await _context.Voiture
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voiture == null)
            {
                return NotFound();
            }

            return View(voiture);
        }

        // POST: Voitures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
             Voiture voiture = _context.Voiture.Find(id);
            _context.Voiture.Remove(voiture);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool VoitureExists(int id)
        {
            return _context.Voiture.Any(e => e.Id == id);
        }


        private void UploadImage()
        {

            var CarID = VM.Voiture.Id;

            //Get wwwroot path to save the file on server 
            string wwwrootPath = _hostingEnvironment.WebRootPath;
            //Get the aploaded files
            var files = HttpContext.Request.Form.Files;

            var SavedCar = _context.Voiture.Find(CarID);

            if (files.Count != 0)
            {
                var ImagePath = @"images\cars\";
                var Extension = Path.GetExtension(files[0].FileName);
                var RelativeImagePath = ImagePath + CarID + Extension;
                var AbsImagePath = Path.Combine(wwwrootPath, RelativeImagePath);
                //Upload the file on server
              using (var fileStream = new FileStream(AbsImagePath, FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                //Set the image path on database
               SavedCar.ImagePath = RelativeImagePath;

            }
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
