using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tarea_02_c.Models;
using System.Text;

namespace Tarea_02_c.Controllers
{
    [Route("Libro")]
    public class LibroController : Controller //En Views la carpeta tiene que llamarse igual al controlador -> "Libro"
    {
        private readonly ILogger<LibroController> _logger;
        private readonly ILibroRepository _libroRepository;
        private readonly IWebHostEnvironment _webRootPath;

        public LibroController(ILogger<LibroController> logger, ILibroRepository repo, IWebHostEnvironment path)
        {
            _logger = logger;
            _libroRepository = repo;
            _webRootPath = path;
        }

        // https://localhost:7144/Libro/GetAllLibros
        [Route("GetAllLibros")]
        public IActionResult GetAllLibros()
        {
            var libros = this._libroRepository.GetLibros();
            return View("GrillaLibros", libros);
        }

        [HttpGet]
        [Route("Ver/{titulo}")] //-> La view tiene que llamarse igual a la ruta "Ver"
        public IActionResult Ver(string titulo)
        {
            _logger.LogInformation($"Estoy en action Ver, titulo: {titulo}");


            if (titulo == null)
            {
                return NotFound("Debe especificar el titulo");
            }

            Libro libro;

            libro = this._libroRepository.Get(titulo);


            if (libro == null)
            {
                return NotFound($"Titulo no encontrado: {titulo}");
            }
            return View(libro);
        }

        [HttpGet]
        [Route("Borrar/{titulo}")]
        public IActionResult Borrar(string titulo)
        {
            Libro libro;
            _logger.LogInformation($"Estoy en action Borrar, titulo: {titulo}");
            libro = this._libroRepository.Get(titulo);


            if (libro == null)
            {
                return NotFound($"Titulo no encontrado: {titulo}");
            }

            return View(libro);
        }

        [HttpPost, ActionName("ConfirmarBorrado")]
        public IActionResult ConfirmarBorrado(string titulo)
        {
            _logger.LogInformation($"Estoy en action ConfirmarBorrardo, titulo: {titulo}");
            this._libroRepository.Delete(titulo);
            return RedirectToAction("GetAllLibros", "Libro");
        }

        [HttpGet]
        [Route("Editar/{titulo}")]
        public IActionResult Editar(string titulo)
        {
            _logger.LogInformation($"Estoy en action Editar, titulo: {titulo}");
            var libro = _libroRepository.Get(titulo);

            if (libro == null)
            {
                return NotFound($"Libro no encontrado con el título: {titulo}");
            }

            return View(libro);
        }

        [HttpPost]
        [Route("EditarConfirmado")]
        public IActionResult EditarConfirmado(Libro libroActualizado)
        {
            if (libroActualizado == null)
            {
                return BadRequest("El libro proporcionado no es válido.");
            }

            var libroExistente = _libroRepository.Get(libroActualizado.Titulo);

            if (libroExistente == null)
            {
                return NotFound($"Libro no encontrado con el título: {libroActualizado.Titulo}");
            }

            // Actualiza los campos del libro existente con los nuevos valores
            libroExistente.Autor = libroActualizado.Autor;
            libroExistente.ISBN = libroActualizado.ISBN;

            _logger.LogInformation($"Libro actualizado: {libroActualizado.Titulo}");

            // Redirige a la lista de libros después de guardar los cambios
            return RedirectToAction("GetAllLibros");
        }




        [HttpGet]
        [Route("Crear")]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [Route("CrearLibro")]
        public IActionResult CrearLibro([Bind("Titulo,Autor,ISBN")] Libro libro)
        {
            this._libroRepository.Add(libro);
            return RedirectToAction("GetAllLibros", "Libro");
        }

        [Route("EjemploDondeDevuelvoHtml")]
        public IActionResult EjemploDondeDevuelvoHtml()
        {
            //observar que los archivos estáticos van en la carpeta wwwroot
            string htmlFilePath = Path.Combine(_webRootPath.WebRootPath, "ejemplo.html");
            _logger.LogInformation("Camino: " + htmlFilePath);
            var fileBytes = System.IO.File.ReadAllBytes(htmlFilePath);

            FileContentResult file = File(fileBytes, "text/html");
            return file;
        }

        [Route("EjemploAcoplado")]
        public IActionResult EjemploAcoplado()
        {
            //si bien este código se podría sacar a una clase Helper
            //la manera en que generamos la vista no es mantenible
            //ni práctica de usar
            _logger.LogInformation("Ejecutando ejemplo codigo acoplado");
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset=\"utf-8\" />");
            sb.AppendLine("<title>Lista de Libros</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("table { width: 100%; border-collapse: collapse; }");
            sb.AppendLine("table, th, td { border: 1px solid black; }");
            sb.AppendLine("th, td { padding: 8px; text-align: left; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<h1>Lista de Vehículos</h1>");
            sb.AppendLine("<table>");
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>Marca</th>");
            sb.AppendLine("<th>Modelo</th>");
            sb.AppendLine("<th>Año</th>");
            sb.AppendLine("<th>Color</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody>");

            foreach (var libro in this._libroRepository.GetLibros())
            {
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td>{libro.Titulo}</td>");
                sb.AppendLine($"<td>{libro.Autor}</td>");
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
