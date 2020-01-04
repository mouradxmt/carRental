using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Models
{
    public class ApplicationUser :IdentityUser
    {
        [ForeignKey("Proprietaire")]
        public int ProprietaireId { get; set; }
        [ForeignKey("Locataire")]
        public int LocataireId { get; set; }
        public string Tele { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        [NotMapped]
        public bool IsAdmin { get; set; }


    }
}
