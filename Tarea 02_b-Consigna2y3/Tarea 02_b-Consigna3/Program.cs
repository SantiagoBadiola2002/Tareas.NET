using Tarea_02_b_Consigna3.pipeline;

class Program
{
    static void Main()
    {
        var pipeLine = new PipeLineEstiloFuncional();

        pipeLine.AgregarEndPoint("/casoA", (string s) => {
            Console.WriteLine("Haciendo algo interesante al recibir request casoA");
            Console.WriteLine($"Parámetro recibido: {s}");
        });

        pipeLine.AgregarEndPoint("/casoB", (string s) => {
            Console.WriteLine("Haciendo algo interesante al recibir request casoB");
        });

        pipeLine.AgregarEndPoint("/casoC", (string s) => {
            Console.WriteLine("Haciendo algo interesante al recibir request casoC");
        });

        // Procesar request
        pipeLine.ProcesarRequestEntrante("/casoB");
    }
}
