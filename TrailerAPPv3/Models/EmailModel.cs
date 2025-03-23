using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrailerAPPv3.Models
{
    public class EmailModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo")]
        public string ToEmail { get; set; }

        [Required]
        [Display(Name = "Asunto")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Mensaje")]
        public string Message { get; set; }
    }
}