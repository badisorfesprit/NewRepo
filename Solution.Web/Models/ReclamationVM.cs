using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Solution.Web.Models
{
    public enum ComplaintVM
    {
       Violence , Securite , Maltraitance
    }
    public class ReclamationVM
    {
      

        public int ReclamationId { get; set; }
        public string Nom { get; set; }

        public string Comment { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateReclamation { get; set; }
        public ComplaintVM ComplaintType { get; set; }
        // public int? ClientId { get; set; }



        // public string status { get; set; }
        // public string ResourceName { get; set; }
    }
}