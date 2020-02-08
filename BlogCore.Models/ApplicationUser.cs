using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage ="El nombre es obligatorio")]        
        public string nombre { get; set; }
        
        [Required(ErrorMessage = "El país es obligatorio")]
        public string pais { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatorio")]
        public string ciudad { get; set; }
        
        public string dirección { get; set; }

    }
}
