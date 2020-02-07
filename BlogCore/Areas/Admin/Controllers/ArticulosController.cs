
using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticulosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnviroment;

        public ArticulosController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnviroment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnviroment = hostingEnviroment;

        }

        [HttpGet]
        public IActionResult Index()
        {
           return View();
        }

        //para cargar la vista de crear y la lista de categorias disponibles
        [HttpGet]        
        public IActionResult Create()
        {
            ArticuloVM articuloViewModel = new ArticuloVM()
            {
                Articulo = new Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()
            };

            return View(articuloViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM articuloVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if (articuloVM.Articulo.id==0)
                {
                    //Nuevo articulo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas,nombreArchivo+extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    articuloVM.Articulo.url_imagen = @"\imagenes\articulos" + nombreArchivo + extension;
                    articuloVM.Articulo.fecha_creacion = DateTime.Now.ToString();

                    _contenedorTrabajo.Articulo.Add(articuloVM.Articulo);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
              
            }
            return View();
            
        }
        #region llamadas a la api
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Articulo.GetAll(includeProperties:"Categoria") });
        }        
        #endregion
    }
}