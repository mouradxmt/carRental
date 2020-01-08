using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamProject.Controllers
{
    public class DashBoardController : Controller
    {
        CarsRentalContext _context;
        public DashBoardController(CarsRentalContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult marqueByNumber()
        {
            List<chartMarqueNumber> listMarque = new List<chartMarqueNumber>();
            var marque = _context.Marques.ToList();
            chartMarqueNumber item;
            foreach(var ele in marque)
            {
                var count = _context.Voiture.Include(m => m.Marque).Include(m => m.Model).Where(m => m.Marque.NomMarque == ele.NomMarque).Count();
                item = new chartMarqueNumber() { nomMarque = ele.NomMarque, count= count };
                listMarque.Add(item);
            }

           
            return Json(listMarque);
        }

        public JsonResult carsByDemand()
        {
            List<CarsDemand> listMarque = new List<CarsDemand>();
            var voitures = _context.Voiture.Include(m => m.Marque).Include(m => m.Model).ToList();
            CarsDemand item;
            foreach (var ele in voitures)
            {

                var count = _context.locations.Where(m => m.VoitureId==ele.Id).Count();
                item = new CarsDemand() {nomVoiture=ele.Marque.NomMarque+" "+ele.Model.NomModel,countLocation=count  };

                listMarque.Add(item);
            }


            return Json(listMarque);
        }
    }
}