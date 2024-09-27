using Consigna2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Consigna2.Controllers
{
    [Route("Apartamento")]
    public class ApartamentoController : Controller //En Views la carpeta tiene que llamarse igual al controlador -> "Apartamento"
    {
        private readonly ILogger<ApartamentoController> _logger;
        private readonly IApartamentoRepository _apartamentoRepository;
        private readonly IWebHostEnvironment _webRootPath;

        public ApartamentoController(ILogger<ApartamentoController> logger, IApartamentoRepository repo, IWebHostEnvironment path)
        {
            _logger = logger;
            _apartamentoRepository = repo;
            _webRootPath = path;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var apartamentos = _apartamentoRepository.GetApartamentos();
            return PartialView("_ApartamentoTable", apartamentos);
        }

        [HttpGet]
        [Route("Ver/{id}")]
        public IActionResult Ver(int id)
        {
            var apartamento = _apartamentoRepository.Get(id);
            if (apartamento == null)
            {
                return NotFound();
            }
            return PartialView("_ApartamentoDetail", apartamento);
        }


        // https://localhost:7144/Apartamento/GetAllApartamentos
        [Route("GetAllApartamentos")]
        public IActionResult GetAllApartamentos()
        {
            var apartamentos = this._apartamentoRepository.GetApartamentos();
            return View("GrillaApartamentos", apartamentos);
        }

        [HttpGet]
        [Route("Borrar/{id}")]
        public IActionResult Borrar(int id)
        {
            Apartamento apartamento;
            _logger.LogInformation($"Estoy en action Borrar, id: {id}");
            apartamento = this._apartamentoRepository.Get(id);

            if (apartamento == null)
            {
                return NotFound($"ID no encontrado: {id}");
            }

            return View(apartamento);
        }

        [HttpPost, ActionName("ConfirmarBorrado")]
        public IActionResult ConfirmarBorrado(int id)
        {
            _logger.LogInformation($"Estoy en action ConfirmarBorrado, id: {id}");
            this._apartamentoRepository.Delete(id);
            return RedirectToAction("GetAllApartamentos", "Apartamento");
        }

        [HttpGet]
        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            _logger.LogInformation($"Estoy en action Editar, id: {id}");
            var apartamento = _apartamentoRepository.Get(id);

            if (apartamento == null)
            {
                return NotFound($"Apartamento no encontrado con el ID: {id}");
            }

            return View(apartamento);
        }

        [HttpPost]
        [Route("EditarConfirmado")]
        public IActionResult EditarConfirmado(Apartamento apartamentoActualizado)
        {
            if (apartamentoActualizado == null)
            {
                return BadRequest("El apartamento proporcionado no es válido.");
            }

            var apartamentoExistente = _apartamentoRepository.Get(apartamentoActualizado.Id);

            if (apartamentoExistente == null)
            {
                return NotFound($"Apartamento no encontrado con el ID: {apartamentoActualizado.Id}");
            }

            // Actualiza los campos del apartamento existente con los nuevos valores
            apartamentoExistente.Piso = apartamentoActualizado.Piso;
            apartamentoExistente.TieneElevador = apartamentoActualizado.TieneElevador;

            _logger.LogInformation($"Apartamento actualizado: {apartamentoActualizado.Id}");

            // Redirige a la lista de apartamentos después de guardar los cambios
            return RedirectToAction("GetAllApartamentos");
        }

        [HttpGet]
        [Route("Crear")]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [Route("CrearApartamento")]
        public IActionResult CrearApartamento([Bind("Id,Precio,Piso,TieneElevador")] Apartamento apartamento)
        {
            this._apartamentoRepository.Add(apartamento);
            return RedirectToAction("GetAllApartamentos", "Apartamento");
        }

        [Route("EjemploDondeDevuelvoHtml")]
        public IActionResult EjemploDondeDevuelvoHtml()
        {
            // observar que los archivos estáticos van en la carpeta wwwroot
            string htmlFilePath = Path.Combine(_webRootPath.WebRootPath, "ejemplo.html");
            _logger.LogInformation("Camino: " + htmlFilePath);
            var fileBytes = System.IO.File.ReadAllBytes(htmlFilePath);

            FileContentResult file = File(fileBytes, "text/html");
            return file;
        }

        [Route("EjemploAcoplado")]
        public IActionResult EjemploAcoplado()
        {
            // si bien este código se podría sacar a una clase Helper
            // la manera en que generamos la vista no es mantenible
            // ni práctica de usar
            _logger.LogInformation("Ejecutando ejemplo codigo acoplado");
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset=\"utf-8\" />");
            sb.AppendLine("<title>Lista de Apartamentos</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("table { width: 100%; border-collapse: collapse; }");
            sb.AppendLine("table, th, td { border: 1px solid black; }");
            sb.AppendLine("th, td { padding: 8px; text-align: left; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<h1>Lista de Apartamentos</h1>");
            sb.AppendLine("<table>");
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>Piso</th>");
            sb.AppendLine("<th>Tiene Elevador</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody>");

            foreach (var apartamento in this._apartamentoRepository.GetApartamentos())
            {
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td>{apartamento.Piso}</td>");
                sb.AppendLine($"<td>{apartamento.TieneElevador}</td>");
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

