using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{
    public enum Complaint
    {
        Violence, Securite, Maltraitance
    }
    public class Reclamation
    {

        [Key]
        public int ReclamationId { get; set; }
        [Display(Name = "Comment")]
        [Required(ErrorMessage = "this field is required")]
        public string Comment { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateReclamation { get; set; }
        //public int? ClientId { get; set; }//nullable
        public Complaint ComplaintType { get; set; }
    }
}
