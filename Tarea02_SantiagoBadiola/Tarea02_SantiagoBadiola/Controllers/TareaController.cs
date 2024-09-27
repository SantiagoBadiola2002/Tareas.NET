using Microsoft.AspNetCore.Mvc;
using Tarea02_SantiagoBadiola.Models;

namespace Tarea02_SantiagoBadiola.Controllers
{
    [ApiController]
    [Route("api/tareas")]
    public class TareaController : Controller // -> Click Derecho -> Agregar -> Controlador -> en Blanco
    {
        private readonly ILogger<TareaController> _logger;
        private IList<Tarea> _tareaList;

        public TareaController(ILogger<TareaController> logger)
        {
            this._logger = logger; // una instancia del logger se injecta usando el constructor

            this._tareaList = new List<Tarea>();
            this._tareaList.Add(new Tarea(1, "TareaEjemplo1", "Crear clases", 2, "Santiago", new DateTime(2024, 8, 8)));
            this._tareaList.Add(new Tarea(2, "TareaEjemplo2", "Crear BD", 3, "Santiago", new DateTime(2024, 8, 9)));
            this._tareaList.Add(new Tarea(3, "TareaEjemplo3", "Levantar servidor", 1, "Javier", new DateTime(2024, 8, 10)));

        }

        // CONSULTA TODOS https://localhost:7034/api/tareas
        [HttpGet]
        public ActionResult<IList<Tarea>> GetAll()
        {
            _logger.LogInformation("Retorno lista de tareas");
            return Ok(_tareaList.ToList());
        }

        // CONSULTA POR ID https://localhost:7034/api/tareas/1
        [HttpGet("{id}")]
        public ActionResult<Tarea> GetById(int id)
        {
            var tarea = _tareaList.FirstOrDefault(t => t.Id == id);
            if (tarea == null)
            {
                _logger.LogWarning($"Tarea con id: {id} no encontrada.");
                return NotFound();
            }

            _logger.LogInformation($"Retorno Tarea con id: {id}");
            return Ok(tarea);
        }

        // INSERTAR https://localhost:7034/api/tareas
        [HttpPost]
        public ActionResult Insertar([FromBody] Tarea nuevaTarea)
        {
            // Generar un nuevo ID para la tarea
            int nuevoId = (_tareaList.Max(t => (int?)t.Id) ?? 0) + 1;
            nuevaTarea.Id = nuevoId;

            _tareaList.Add(nuevaTarea);
            _logger.LogInformation($"Tarea con id: {nuevaTarea.Id} creada.");

            return CreatedAtAction(nameof(GetById), new { id = nuevaTarea.Id }, nuevaTarea);
        }

        // ELIMINAR https://localhost:7034/api/tareas/1
        [HttpDelete("{id}")]
        public ActionResult Eliminar(int id)
        {
            var tarea = _tareaList.FirstOrDefault(t => t.Id == id);
            if (tarea == null)
            {
                _logger.LogWarning($"Tarea con id: {id} no encontrada.");
                return NotFound();
            }

            _tareaList.Remove(tarea);
            _logger.LogInformation($"Tarea con id: {id} eliminada.");

            return NoContent();
        }
    }
}
