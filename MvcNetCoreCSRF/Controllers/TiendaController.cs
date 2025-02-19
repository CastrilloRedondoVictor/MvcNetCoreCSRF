using Microsoft.AspNetCore.Mvc;

namespace MvcNetCoreCSRF.Controllers
{
    public class TiendaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Productos()
        {
            if(HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("Denied", "Managed");
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Productos(string direccion, string[] productos)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("Denied", "Managed");
            }
            TempData["DIRECCION"] = direccion;
            TempData["PRODUCTOS"] = productos;
            return RedirectToAction("PedidoFinal");
        }

        public IActionResult PedidoFinal()
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                return RedirectToAction("Denied", "Managed");
            }

            string direccion = TempData["DIRECCION"] as string;
            string[] productos = TempData["PRODUCTOS"] as string[];

            ViewData["DIRECCION"] = direccion;
            return View(productos);
        }
    }
}
