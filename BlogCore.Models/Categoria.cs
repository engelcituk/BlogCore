﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCore.Models
{
    public class Categoria
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Ingresa un nombre para la categoría")]
        [Display(Name ="Nombre categoría")]
        public string nombre{ get; set; }

        [Required]
        [Display(Name = "Orden de visualización")]
        public string orden { get; set; }
    }
}
