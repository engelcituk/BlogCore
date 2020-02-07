using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.AccesoDatos.Data
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _db;

        public SliderRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
       
        public void Update(Slider slider)
        {
            var objDesdeDb = _db.Slider.FirstOrDefault(s => s.id == slider.id);
            objDesdeDb.nombre = slider.nombre;
            objDesdeDb.estado = slider.estado;
            objDesdeDb.url_imagen = slider.url_imagen;
           

            _db.SaveChanges();

        }
    }
}
