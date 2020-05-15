using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Solution.Domain.Entities;

namespace Solution.Web.Models
{
    public enum ComplaintVM
    {
       Violence , Securite , Maltraitance
    }
    public class ReclamationVM
    {
        public int ReclamationId { get; set; }
        public string Comment { get; set; }
        public DateTime DateReclamation { get; set; }
        public ComplaintState state { get; set; }
        public Complaint ComplaintType { get; set; }
        public int? senderID { get; set; }
        public User sender { get; set; }
        public User receiver { get; set; }
    }
}