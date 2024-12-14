using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using WebPractica3.Models;

namespace WebPractica3.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public ConsultaController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("PracticaApi");
            try
            {
                var response = await client.GetAsync("Consulta");

                if (response.IsSuccessStatusCode)
                {
   
                    var datos = await response.Content.ReadFromJsonAsync<List<Principal>>();

                    if (datos == null || !datos.Any())
                    {
                        ModelState.AddModelError("", "No se encontraron datos.");
                    }

                    return View(datos);
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"Error de API: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al conectar con la API: {ex.Message}");
            }

            return View(new List<Principal>());
        }
    }
}
