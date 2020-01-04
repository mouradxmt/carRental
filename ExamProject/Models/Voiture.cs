using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Models
{
    public class Voiture
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Provide Price")]
        public double PrixParJour { get; set; }

        [Required(ErrorMessage = "Provide Year")]
        public int Annee { get; set; }

        [Required(ErrorMessage = "Provide Mileage")]
        [Range(1, int.MaxValue, ErrorMessage = "This Mileage is Invalid")]
        public int Kilometrage { get; set; }

        public Marque Marque { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select a Make")]
        public int MarqueID { get; set; }

        public Model Model { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select a Model")]
        public int ModelID { get; set; }

        public Proprietaire Proprietaire { get; set; }
        public string Couleur { get; set; }

        public string Immatriculation { get; set; }

        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
}

