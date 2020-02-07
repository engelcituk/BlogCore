using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.AccesoDatos.Data
{
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext _db;

        public ArticuloRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
       
        public void Update(Articulo articulo)
        {
            var objDesdeDb = _db.Articulo.FirstOrDefault(s => s.id == articulo.id);
            objDesdeDb.nombre = articulo.nombre;
            objDesdeDb.descripcion = articulo.descripcion;
            objDesdeDb.url_imagen = articulo.url_imagen;
            objDesdeDb.CategriaId = articulo.CategriaId;

            //_db.SaveChanges();

        }
    }
}
