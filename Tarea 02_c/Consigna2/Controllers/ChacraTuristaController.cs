namespace Consigna2.Controllers
{
    using Consigna2.Infraestructure.Data;
    using Consigna2.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Text;

    [Route("ChacraTuristica")]
    public class ChacraTuristaController : Controller
    {
        private readonly ILogger<ChacraTuristaController> _logger;
        private readonly IChacraTuristaRepository _chacraTuristicaRepository;
        private readonly IWebHostEnvironment _webRootPath;

        public ChacraTuristaController(ILogger<ChacraTuristaController> logger, IChacraTuristaRepository repo, IWebHostEnvironment path)
        {
            _logger = logger;
            _chacraTuristicaRepository = repo;
            _webRootPath = path;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var chacras = _chacraTuristicaRepository.GetChacrasTuristicas();
            return PartialView("_ChacraTuristicaTable", chacras);
        }

        [HttpGet]
        [Route("Ver/{id}")]
        public IActionResult Ver(int id)
        {
            var chacra = _chacraTuristicaRepository.Get(id);
            if (chacra == null)
            {
                return NotFound();
            }
            return PartialView("_ChacraTuristicaDetail", chacra);
        }


        // https://localhost:7144/ChacraTuristica/GetAllChacras
        [Route("GetAllChacras")]
        public IActionResult GetAllChacras()
        {
            var chacras = this._chacraTuristicaRepository.GetChacrasTuristicas();
            return View("GrillaChacras", chacras);
        }

       
        [HttpGet]
        [Route("Borrar/{id}")]
        public IActionResult Borrar(int id)
        {
            _logger.LogInformation($"Estoy en action Borrar, ID: {id}");
            var chacra = this._chacraTuristicaRepository.Get(id);

            if (chacra == null)
            {
                return NotFound($"ID no encontrado: {id}");
            }

            return View(chacra);
        }

        [HttpPost, ActionName("ConfirmarBorrado")]
        public IActionResult ConfirmarBorrado(int id)
        {
            _logger.LogInformation($"Estoy en action ConfirmarBorrardo, ID: {id}");
            this._chacraTuristicaRepository.Delete(id);
            return RedirectToAction("GetAllChacras", "ChacraTuristica");
        }

        [HttpGet]
        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            _logger.LogInformation($"Estoy en action Editar, ID: {id}");
            var chacra = _chacraTuristicaRepository.Get(id);

            if (chacra == null)
            {
                return NotFound($"Chacra no encontrada con el ID: {id}");
            }

            return View(chacra);
        }

        [HttpPost]
        [Route("EditarConfirmado")]
        public IActionResult EditarConfirmado(ChacraTuristica chacraActualizada)
        {
            if (chacraActualizada == null)
            {
                return BadRequest("La chacra proporcionada no es válida.");
            }

            var chacraExistente = _chacraTuristicaRepository.Get(chacraActualizada.Id);

            if (chacraExistente == null)
            {
                return NotFound($"Chacra no encontrada con el ID: {chacraActualizada.Id}");
            }

            // Actualiza los campos de la chacra existente con los nuevos valores
            chacraExistente.Area = chacraActualizada.Area;
            chacraExistente.TienePiscina = chacraActualizada.TienePiscina;

            _logger.LogInformation($"Chacra actualizada: {chacraActualizada.Id}");

            // Redirige a la lista de chacras después de guardar los cambios
            return RedirectToAction("GetAllChacras");
        }

        [HttpGet]
        [Route("Crear")]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [Route("CrearChacra")]
        public IActionResult CrearChacra([Bind("Area,TienePiscina")] ChacraTuristica chacra)
        {
            this._chacraTuristicaRepository.Add(chacra);
            return RedirectToAction("GetAllChacras", "ChacraTuristica");
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
            _logger.LogInformation("Ejecutando ejemplo código acoplado");
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset=\"utf-8\" />");
            sb.AppendLine("<title>Lista de Chacras</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("table { width: 100%; border-collapse: collapse; }");
            sb.AppendLine("table, th, td { border: 1px solid black; }");
            sb.AppendLine("th, td { padding: 8px; text-align: left; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<h1>Lista de Chacras</h1>");
            sb.AppendLine("<table>");
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>Área</th>");
            sb.AppendLine("<th>Tiene Piscina</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody>");

            foreach (var chacra in this._chacraTuristicaRepository.GetChacrasTuristicas())
            {
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td>{chacra.Area}</td>");
                sb.AppendLine($"<td>{(chacra.TienePiscina ? "Sí" : "No")}</td>");
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
