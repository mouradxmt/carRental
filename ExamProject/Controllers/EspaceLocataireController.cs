using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamProject.Models;
using ExamProject.Models.viewModel;

namespace ExamProject.Controllers
{
    public class EspaceLocataireController : Controller
    {
        private readonly CarsRentalContext _context;
        EspaceLocataireVM espaceLocataireVM;

        public EspaceLocataireController(CarsRentalContext context)
        {
            _context = context;
            espaceLocataireVM = new EspaceLocataireVM()
            {
                voitures = _context.Voiture.ToList(),
                searchedCar = ""

            };
            foreach(var ele in espaceLocataireVM.voitures)
            {
                var appuser = _context.applicationUsers.Where(ap => ap.ProprietaireId == ele.ProprietaireId).First();
                ele.ApplicationUser = appuser;

                if (!ele.EstDisponible)
                {
                    var location = _context.locations.Where(l => l.VoitureId == ele.Id).FirstOrDefault();

                    int result  =DateTime.Compare(location.DateFin, DateTime.Now);
                    if (result < 0)
                    {
                        ele.EstDisponible = true;
                        _context.SaveChanges();
                    }
                }
            }
            

        }

        
        public IActionResult Index()
        {
            espaceLocataireVM.voitures = _context.Voiture.Include(v => v.Marque).Include(v => v.Model).ToList();
            return View(espaceLocataireVM);
        }

        [HttpPost]
        public IActionResult Index(string keyword="")
        {
            if (String.IsNullOrEmpty(keyword)) return Index();
            espaceLocataireVM.voitures = _context.Voiture.Include(v => v.Marque).Include(v => v.Model).Where(v=>v.Marque.NomMarque.Contains(keyword) || v.Model.NomModel.Contains(keyword)).ToList();
            ViewBag.keyword = keyword;
            return View(espaceLocataireVM);

        }


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiture = _context.Voiture.Include(v => v.Marque).Include(v => v.Model)
                .First(m => m.Id == id);
            if (voiture == null)
            {
                return NotFound();
            }

            return View(voiture);
        }
        

        public IActionResult Allouer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiture = espaceLocataireVM.voitures
                .First(m => m.Id == id);
            if (voiture == null)
            {
                return NotFound();
            }

            return View(voiture);
        }
        // GET: EspaceLocataire/Create
        public IActionResult Create()
        {
            ViewData["MarqueID"] = new SelectList(_context.Marques, "Id", "NomMarque");
            ViewData["ModelID"] = new SelectList(_context.Models, "Id", "NomModel");
            return View();
        }

        // POST: EspaceLocataire/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> confirmAllouer( string startingDate,string endingDate,int carid)
        {
            string[] sd = startingDate.Split("-");
            string[] ed = endingDate.Split("-");
            DateTime startd = new DateTime(Convert.ToInt32(sd[0]), Convert.ToInt32(sd[1]), Convert.ToInt32(sd[2]));
            DateTime endd = new DateTime(Convert.ToInt32(ed[0]), Convert.ToInt32(ed[1]), Convert.ToInt32(ed[2]));

            var car = _context.Voiture.Where(v => v.Id == carid).FirstOrDefault();
            if (car.EstDisponible == true) { 
            var user = _context.applicationUsers.Where(ap => ap.UserName == User.Identity.Name).First();
            Location location = new Location() {
                DateDebut =startd,
                DateFin =endd,
                LocataireId =user.LocataireId,
                PrixTotal=(endd-startd).TotalDays * Convert.ToDouble(car.PrixParJour),
                VoitureId =carid
                
            };
            _context.locations.Add(location);
                car.EstDisponible = false;
            _context.SaveChanges();
            }

            return this.Redirect("/EspaceLocataire");
        }



        public IActionResult ajouterFavori(int id)
        {
            var currentUser = _context.applicationUsers.Where(a => a.UserName == User.Identity.Name).First();

            var test = _context.favoris.FirstOrDefault(f => f.idlocataire == currentUser.LocataireId && f.idvoiture == id);
            if (test == null)
            {
                var favo = new Favori() { idlocataire = currentUser.LocataireId, idvoiture = id };
                _context.favoris.Add(favo);
                _context.SaveChanges();
            }
            
            return this.Redirect("/EspaceLocataire");
        }

        public IActionResult supprimerFavori(int id)
        {
            var currentUser = _context.applicationUsers.Where(a => a.UserName == User.Identity.Name).First();

            var test = _context.favoris.FirstOrDefault(f => f.idlocataire == currentUser.LocataireId && f.idvoiture == id);
            if (test != null)
            {
                
                _context.favoris.Remove(test);
                _context.SaveChanges();
            }

            return this.Redirect("/EspaceLocataire");
        }
        public IActionResult favorites()
        {

            var voitures = new List<Voiture>();
            var currentUser = _context.applicationUsers.Where(a => a.UserName == User.Identity.Name).First();
            var fav = _context.favoris.Where(f => f.idlocataire == currentUser.LocataireId).ToList();
            foreach(var ele in fav)
            {
                var voiture =_context.Voiture.Include(v => v.Marque).Include(v => v.Model).FirstOrDefault(v=>v.Id==ele.idvoiture);
                voitures.Add(voiture);
            }

            EspaceLocataireVM vm = new EspaceLocataireVM() { voitures=voitures };
            return View(vm);
        }


        public async Task<IActionResult> myRents()
        {
            var user = _context.applicationUsers.Where(ap => ap.UserName == User.Identity.Name).First();
           var location= _context.locations.Where(lo => lo.LocataireId == user.LocataireId).ToList();

            return View(location);
        }

        

        
    }
}
