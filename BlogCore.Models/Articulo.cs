using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogCore.Models
{
    public class Articulo
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre del artículo")]
        public string nombre { get; set; }

        [Display(Name = "Fecha de creación")]
        public string fecha_creacion { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string url_imagen { get; set; }

        [Required]
        public int CategriaId { get; set; }
        [ForeignKey("CategriaId")]
        public Categoria Categoria { get; set; }
    }
}
