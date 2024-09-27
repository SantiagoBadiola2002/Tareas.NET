using Consigna2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Consigna2.Controllers
{
    
    [Route("Casa")]
    public class CasaController : Controller // En Views la carpeta tiene que llamarse igual al controlador -> "Casa"
    {
        private readonly ILogger<CasaController> _logger;
        private readonly ICasaRepository _casaRepository;
        private readonly IWebHostEnvironment _webRootPath;

        public CasaController(ILogger<CasaController> logger, ICasaRepository repo, IWebHostEnvironment path)
        {
            _logger = logger;
            _casaRepository = repo;
            _webRootPath = path;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var casas = _casaRepository.GetCasas();
            return PartialView("_CasaTable", casas);
        }

        [HttpGet]
        [Route("Ver/{id}")]
        public IActionResult Ver(int id)
        {
            var casa = _casaRepository.Get(id);
            if (casa == null)
            {
                return NotFound();
            }
            return PartialView("_CasaDetail", casa);
        }

        // https://localhost:7144/Casa/GetAllCasas
        [Route("GetAllCasas")]
        public IActionResult GetAllCasas()
        {
            var casas = this._casaRepository.GetCasas();
            return View("GrillaCasas", casas);
        }

    

        [HttpGet]
        [Route("Borrar/{id}")]
        public IActionResult Borrar(int id)
        {
            Casa casa;
            _logger.LogInformation($"Estoy en action Borrar, id: {id}");
            casa = this._casaRepository.Get(id);

            if (casa == null)
            {
                return NotFound($"Casa no encontrada con el id: {id}");
            }

            return View(casa);
        }

        [HttpPost, ActionName("ConfirmarBorrado")]
        public IActionResult ConfirmarBorrado(int id)
        {
            _logger.LogInformation($"Estoy en action ConfirmarBorrado, id: {id}");
            this._casaRepository.Delete(id);
            return RedirectToAction("GetAllCasas", "Casa");
        }

        [HttpGet]
        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            _logger.LogInformation($"Estoy en action Editar, id: {id}");
            var casa = _casaRepository.Get(id);

            if (casa == null)
            {
                return NotFound($"Casa no encontrada con el id: {id}");
            }

            return View(casa);
        }

        [HttpPost]
        [Route("EditarConfirmado")]
        public IActionResult EditarConfirmado(Casa casaActualizada)
        {
            if (casaActualizada == null)
            {
                return BadRequest("La casa proporcionada no es válida.");
            }

            var casaExistente = _casaRepository.Get(casaActualizada.Id);

            if (casaExistente == null)
            {
                return NotFound($"Casa no encontrada con el id: {casaActualizada.Id}");
            }

            // Actualiza los campos de la casa existente con los nuevos valores
            casaExistente.NumeroHabitaciones = casaActualizada.NumeroHabitaciones;
            casaExistente.NumeroBanos = casaActualizada.NumeroBanos;
            casaExistente.Precio = casaActualizada.Precio;

            _logger.LogInformation($"Casa actualizada: {casaActualizada.Id}");

            // Redirige a la lista de casas después de guardar los cambios
            return RedirectToAction("GetAllCasas");
        }

        [HttpGet]
        [Route("Crear")]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [Route("CrearCasa")]
        public IActionResult CrearCasa([Bind("Id,Precio,NumeroHabitaciones,NumeroBanos")] Casa casa)
        {
            this._casaRepository.Add(casa);
            return RedirectToAction("GetAllCasas", "Casa");
        }

        [Route("EjemploDondeDevuelvoHtml")]
        public IActionResult EjemploDondeDevuelvoHtml()
        {
            // Observar que los archivos estáticos van en la carpeta wwwroot
            string htmlFilePath = Path.Combine(_webRootPath.WebRootPath, "ejemplo.html");
            _logger.LogInformation("Camino: " + htmlFilePath);
            var fileBytes = System.IO.File.ReadAllBytes(htmlFilePath);

            FileContentResult file = File(fileBytes, "text/html");
            return file;
        }

        [Route("EjemploAcoplado")]
        public IActionResult EjemploAcoplado()
        {
            // Si bien este código se podría sacar a una clase Helper
            // la manera en que generamos la vista no es mantenible
            // ni práctica de usar
            _logger.LogInformation("Ejecutando ejemplo codigo acoplado");
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset=\"utf-8\" />");
            sb.AppendLine("<title>Lista de Casas</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("table { width: 100%; border-collapse: collapse; }");
            sb.AppendLine("table, th, td { border: 1px solid black; }");
            sb.AppendLine("th, td { padding: 8px; text-align: left; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<h1>Lista de Casas</h1>");
            sb.AppendLine("<table>");
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>ID</th>");
            sb.AppendLine("<th>Precio</th>");
            sb.AppendLine("<th>Habitaciones</th>");
            sb.AppendLine("<th>Baños</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody>");

            foreach (var casa in this._casaRepository.GetCasas())
            {
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td>{casa.Id}</td>");
                sb.AppendLine($"<td>{casa.Precio}</td>");
                sb.AppendLine($"<td>{casa.NumeroHabitaciones}</td>");
                sb.AppendLine($"<td>{casa.NumeroBanos}</td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return Content(sb.ToString(), "text/html");
        }
    }
}
