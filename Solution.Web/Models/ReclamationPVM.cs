using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Solution.Web.Models
{
    public enum ComplaintPVM
    {
        Violence, Securite, Maltraitance
    }
    public enum GenrePVM
    {
        Parent, Visiteur
    }
    public class ReclamationPVM
    {
        [Key]

        public int ReclamationPId { get; set; }
        public string Nom { get; set; }
        public string Ville { get; set; }
        public GenrePVM Genre { get; set; }
        public int CodePostale { get; set; }


        [Display(Name = "Comment")]

        [Required(ErrorMessage = "this field is required")]
        public string Comment { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateReclamation { get; set; }
        //public int? ClientId { get; set; }//nullable
        public ComplaintPVM ComplaintType { get; set; }
    }
}