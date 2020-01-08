using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Models
{
    public class Demande
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public int voitureid { get; set; }
        public Voiture voiture { get; set; }
        public Location location { get; set; }
        public string etat { get; set; }

    }
}
