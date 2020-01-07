using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamProject.Models;
using ExamProject.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;

namespace ExamProject.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class MarquesController : Controller
    {
        private readonly CarsRentalContext _context;

        public MarquesController(CarsRentalContext context)
        {
            _context = context;
        }
        //private readonly IStringLocalizer<MarquesController> _localizer;

        //public MarquesController(IStringLocalizer<MarquesController> localizer)
        //{
        //    _localizer = localizer;
        //}

        // GET: Marques
        public async Task<IActionResult> Index(string marques)
        {
            if (marques != null)
            {
                var marquedata = _context.Marques.Where(p => p.NomMarque.Contains(marques)).ToList();
                return View(marquedata);
            }
            else
            {
                return View(await _context.Marques.ToListAsync());
            }
        }


        // GET: Marques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marque = await _context.Marques
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marque == null)
            {
                return NotFound();
            }

            return View(marque);
        }

        // GET: Marques/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Marque marque)
        {
            if (ModelState.IsValid)
            {
                _context.Add(marque);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(marque);
        }

        // GET: Marques/Edit/5
        public IActionResult Edit(int id)
        {
            var marque =_context.Marques.Find(id);
            if (marque == null)
            {
                return NotFound();
            }
            return View(marque);
        }

        // POST: Marques/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Edit(Marque marque)
        {
            if (ModelState.IsValid)
            {
               _context.Update(marque);
               _context.SaveChanges();
                return RedirectToAction(nameof(Index));  
            }
            return View(marque);
        }

        // GET: Marques/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marque = await _context.Marques
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marque == null)
            {
                return NotFound();
            }

            return View(marque);
        }

        // POST: Marques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Marque marque = _context.Marques.Find(id);
            _context.Marques.Remove(marque);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }        
    }
}
