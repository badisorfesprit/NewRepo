using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Domain.Entities
{


    public enum ComplaintP
    {
        Violence, Securite, Maltraitance
    }
    public enum Genre
    {
        Parent, Visiteur
    }
    public class ReclamationP
    {


        [Key]

        public int ReclamationPId { get; set; }
        public string Nom { get; set; }
        public string Ville { get; set; }
        public Genre Genre { get; set; }
        public int CodePostale { get; set; }


        [Display(Name = "Comment")]
        [Required(ErrorMessage = "this field is required")]
        public string Comment { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateReclamation { get; set; }
        //public int? ClientId { get; set; }//nullable
        public ComplaintP ComplaintType { get; set; }

    }
}
