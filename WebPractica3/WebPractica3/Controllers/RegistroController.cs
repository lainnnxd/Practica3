using Microsoft.AspNetCore.Mvc;
using WebPractica3.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace WebPractica3.Controllers
{
    public class RegistroController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public RegistroController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("PracticaApi");
            var response = await client.GetAsync("Compras/Pendientes"); 

            if (response.IsSuccessStatusCode)
            {
                var compras = await response.Content.ReadFromJsonAsync<List<Compra>>();
                return View("~/Views/Home/Registro.cshtml", compras); 
            }

          
            return View("~/Views/Home/Registro.cshtml", new List<Compra>());
        }


     
        [HttpPost]
        public async Task<IActionResult> RegistrarAbono(Abono abono)
        {
          
            if (abono.Monto <= 0)
            {
                ModelState.AddModelError("", "El monto del abono debe ser mayor a 0.");
                return RedirectToAction("Index");
            }

            var client = _clientFactory.CreateClient("PracticaApi");
            var response = await client.PostAsJsonAsync("Abonos/RegistrarAbono", abono); 

            if (response.IsSuccessStatusCode)
            {
               
                return RedirectToAction("Index", "Consulta");
            }

            
            ModelState.AddModelError("", "Error al registrar el abono.");
            return RedirectToAction("Index");
        }

    }
}
