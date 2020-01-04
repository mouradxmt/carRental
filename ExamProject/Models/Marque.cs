using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamProject.Models
{
    public class Marque
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Provide Make")]
        [MaxLength(30)]
        public string NomMarque { get; set; }
    }
}