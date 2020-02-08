using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnviroment;

        public SlidersController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnviroment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnviroment = hostingEnviroment;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                
                //procesamiento de la imagen y datos del slider
                string nombreArchivo = Guid.NewGuid().ToString();
                var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                var extension = Path.GetExtension(archivos[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                }

                slider.url_imagen = @"\imagenes\sliders\" + nombreArchivo + extension;
               
                _contenedorTrabajo.Slider.Add(slider);
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
               

            }
            return View();
        }

        // busco el dato y lo envio a la vista
        [HttpGet]
        public IActionResult Edit(int? id)
        {            
            if (id != null)
            {
               var slider = _contenedorTrabajo.Slider.Get(id.GetValueOrDefault());
                return View(slider);
            }
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnviroment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var sliderDesdeDb = _contenedorTrabajo.Articulo.Get(slider.id);

                if (archivos.Count() > 0)
                {
                    //Editamos imagen
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, sliderDesdeDb.url_imagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //subimos nuevamente el archivo
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + nuevaExtension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    slider.url_imagen = @"\imagenes\sliders\" + nombreArchivo + extension;
                    

                    _contenedorTrabajo.Slider.Update(slider);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //aqui cuando la imagen ya existe y no se reemplaz, se conserva la existente en la bd
                    slider.url_imagen = sliderDesdeDb.url_imagen;
                }
                _contenedorTrabajo.Slider.Update(slider);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));

            }
            return View();

        }
        #region llamadas a la api
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Slider.GetAll() });
        }
        //borrado de un articulo y su imagen
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var sliderFromDb = _contenedorTrabajo.Slider.Get(id);
            string rutaPrincipal = _hostingEnviroment.WebRootPath;
            var rutaImagen = Path.Combine(rutaPrincipal, sliderFromDb.url_imagen.TrimStart('\\'));

            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            if (sliderFromDb == null)
            {
                return Json(new { success = false, message = "Error al borrar el artículo" });
            }
            _contenedorTrabajo.Slider.Remove(sliderFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "El slider se ha borrado de manera exitosa" });
        }
        #endregion
    }
}