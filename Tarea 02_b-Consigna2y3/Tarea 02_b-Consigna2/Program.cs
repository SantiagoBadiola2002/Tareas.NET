using System.Drawing;
using Tarea_02_b_Consigna2.Orchestrator;
using Tarea_02_b_Consigna2;
using Tarea_02_b_Consigna2.Pasos;

class Program
{
    static void Main(string[] args)
    {
        var contexto = new ContextoImagen
        {
            ColorOriginal = Color.White,
            ColorNuevo = Color.Red
        };

        var pipeline = new PipelineOrquestador();
        pipeline.AgregarPaso(new Paso1LeerImagen("D:\\Proyectos C#\\Tarea 02_b-Consigna2y3\\Tarea 02_b-Consigna2\\imagenes\\imagen1.png"));
        pipeline.AgregarPaso(new Paso2AplicarFiltroColor());
        pipeline.AgregarPaso(new Paso3GuardarImagen("D:\\Proyectos C#\\Tarea 02_b-Consigna2y3\\Tarea 02_b-Consigna2\\imagenes\\imagen1Procesada.png"));

        pipeline.Ejecutar(contexto);

        Console.WriteLine("Procesamiento de imagen completado.");
    }
}
