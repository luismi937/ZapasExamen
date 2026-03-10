using Microsoft.AspNetCore.Mvc;
using ZapasExamen.Models;
using ZapasExamen.Repositories;

namespace ZapasExamen.Controllers
{
    public class ZapatillasController : Controller
    {
        private RepositoryZapatillas repo;
        public ZapatillasController(RepositoryZapatillas repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            List<Zapatilla> zapatillas = await this.repo.GetZapatillasAsync();
            return View(zapatillas);
        }
        public async Task<IActionResult> Detalles(int idproducto)
        {
            Zapatilla zapa = await this.repo.FindZapatillaAsync(idproducto);
            ViewData["ZAPATILLA"] = zapa;
            return View(zapa);
        }
        public async Task<IActionResult> PaginacionImagenes(int? posicion, int idproducto)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            ModelPaginacionImagenes model = await this.repo.GetPaginacionImagenesAsync(posicion.Value, idproducto);
            ViewData["NUMREGISTROS"] = model.NumRegistros;
            ViewData["POSICION"] = posicion;
            int siguiente = posicion.Value + 1;
            if (siguiente > model.NumRegistros)
            {
                siguiente = model.NumRegistros;
            }
            int anterior = posicion.Value - 1;
            if (anterior < 1)
            {
                anterior = 1;
            }
            ViewData["ULTIMO"] = model.NumRegistros;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            ViewData["IDPRODUCTO"] = model.Zapa.IdProducto;
            return PartialView("_PaginacionImagenes", model.ImagenZapatilla);

        }
    }
}
