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

        public IActionResult Index()
        {
            List<Zapatilla> zapatillas = this.repo.GetZapatillas();

            return View(zapatillas);
        }

        public IActionResult Details(int id)
        {
           
       


            Zapatilla zapatilla = this.repo.FindZapatillas(id);
           

            return View(zapatilla);
    }






    public IActionResult _ImagenesPartial(int id, int? posicion)
    {

        if (posicion == null)
        {
            posicion = 1;
        }
        int numregistros = this.repo.GetNumeroRegistrosImagenesZapatillas(id);
        //ESTAMOS EN LA POSICION 1, QUE TENEMOS QUE DEVOLVER A LA VISTA?
        int siguiente = posicion.Value + 1;
        if (siguiente > numregistros)
        {

            siguiente = numregistros;
        }
        int anterior = posicion.Value - 1;
        if (anterior < 1)
        {
            anterior = 1;
        }

        ViewData["ULTIMO"] = numregistros;
        ViewData["SIGUIENTE"] = siguiente;
        ViewData["ANTERIOR"] = anterior;



        List<ImagenZapatilla> imagenes = this.repo.GetImagenesZapatillas(id);

        return PartialView("_ImagenesPartial", imagenes);
    }

}
}
