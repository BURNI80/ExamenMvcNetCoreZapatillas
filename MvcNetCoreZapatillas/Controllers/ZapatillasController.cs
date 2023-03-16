using Microsoft.AspNetCore.Mvc;
using MvcNetCoreZapatillas.Models;
using MvcNetCoreZapatillas.Repositories;

namespace MvcNetCoreZapatillas.Controllers
{
    public class ZapatillasController : Controller
    {
        private RepositoryZapatillas repo;

        public ZapatillasController(RepositoryZapatillas repo)
        {
            this.repo = repo;
        }

        public IActionResult Zapatillas()
        {
            
            return View(this.repo.GetZapatillas());
        }

        public IActionResult Zapatilla(int id)
        {
            return View(this.repo.FindZapatilla(id));
        }

        public IActionResult _ImagenesPartial(int id , int posicion)
        {
            if (posicion == null || posicion <= 0)
            {
                posicion = 1;
            }
            ViewData["TOTALIMG"] = this.repo.GetTotalImagenes(id);
            ViewData["POSICION"] = posicion;
            ImagenZapatilla imagenes = this.repo.GetImagen(id, posicion);
            return PartialView("_ImagenesPartial", imagenes);
        }
    }
}
