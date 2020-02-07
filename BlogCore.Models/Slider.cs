using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCore.Models
{
    public class Slider
    {
        [Key]
        public int id { get; set; }

        
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre del slider")]
        public string nombre { get; set; }       

        [Display(Name = "Estado")]
        public bool estado { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string url_imagen { get; set; }
    }
}
