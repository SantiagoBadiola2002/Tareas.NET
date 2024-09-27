namespace Tarea_03_a.Models
{
    public class Usuario
    {
        public String Correo { get; set; }
        public String Contrasenia { get; set; }

        public Usuario(String correo, String contrasenia)
        {
            this.Correo = correo;
            this.Contrasenia = contrasenia;
        }

        public Boolean EsUsuarioValido()
        {
            return true;
        }

        public Boolean NecesitarVerificacion()
        {
            return true;
        }
    }
}
