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
        public string PrixParJour { get; set; }

        [Required(ErrorMessage = "Provide Year")]
        public string Annee { get; set; }

        [Required(ErrorMessage = "Provide Mileage")]
        [Range(1, int.MaxValue, ErrorMessage = "This Mileage is Invalid")]
        public string Kilometrage { get; set; }

        public Marque Marque { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select a Make")]
        public int MarqueID { get; set; }

        public Model Model { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Select a Model")]
        public int ModelID { get; set; }

        public int ProprietaireId { get; set; }
        public string Couleur { get; set; }

        public string Immatriculation { get; set; }

        public string Description { get; set; }
        public string ImagePath { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public bool EstDisponible { get; set; }
    }
}

