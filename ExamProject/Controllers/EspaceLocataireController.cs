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
            espaceLocataireVM.voitures = _context.Voiture.Include(v => v.Marque).Include(v => v.Model).Where(v=>v.Marque.NomMarque.Contains(keyword) ).ToList();
            ViewBag.keyword = keyword;
            return View(espaceLocataireVM);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiture = await _context.Voiture
                .Include(v => v.Marque)
                .Include(v => v.Model)
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("Id,PrixParJour,Annee,Kilometrage,MarqueID,ModelID,ProprietaireId,Couleur,Immatriculation,Description,ImagePath")] Voiture voiture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voiture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarqueID"] = new SelectList(_context.Marques, "Id", "NomMarque", voiture.MarqueID);
            ViewData["ModelID"] = new SelectList(_context.Models, "Id", "NomModel", voiture.ModelID);
            return View(voiture);
        }

        // GET: EspaceLocataire/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiture = await _context.Voiture.FindAsync(id);
            if (voiture == null)
            {
                return NotFound();
            }
            ViewData["MarqueID"] = new SelectList(_context.Marques, "Id", "NomMarque", voiture.MarqueID);
            ViewData["ModelID"] = new SelectList(_context.Models, "Id", "NomModel", voiture.ModelID);
            return View(voiture);
        }

        // POST: EspaceLocataire/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PrixParJour,Annee,Kilometrage,MarqueID,ModelID,ProprietaireId,Couleur,Immatriculation,Description,ImagePath")] Voiture voiture)
        {
            if (id != voiture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voiture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoitureExists(voiture.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarqueID"] = new SelectList(_context.Marques, "Id", "NomMarque", voiture.MarqueID);
            ViewData["ModelID"] = new SelectList(_context.Models, "Id", "NomModel", voiture.ModelID);
            return View(voiture);
        }

        // GET: EspaceLocataire/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiture = await _context.Voiture
                .Include(v => v.Marque)
                .Include(v => v.Model)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voiture == null)
            {
                return NotFound();
            }

            return View(voiture);
        }

        // POST: EspaceLocataire/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voiture = await _context.Voiture.FindAsync(id);
            _context.Voiture.Remove(voiture);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoitureExists(int id)
        {
            return _context.Voiture.Any(e => e.Id == id);
        }
    }
}
