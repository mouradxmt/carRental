using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Models.viewModel
{
    public class EspaceLocataireVM
    {
        public IEnumerable<Voiture> voitures { get; set; }

        public string searchedCar { get; set; }
    }
}
