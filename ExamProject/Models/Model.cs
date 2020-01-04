using System.ComponentModel.DataAnnotations;

namespace ExamProject.Models
{
    public class Model
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Provide Model")]
        [MaxLength(30)]
        public string NomModel { get; set; }
        public Marque Marque { get; set; }
        public int MarqueID { get; set; }
    }
}