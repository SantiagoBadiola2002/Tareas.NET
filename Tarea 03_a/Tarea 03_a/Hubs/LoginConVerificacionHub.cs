using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Tarea_03_a.Models;

namespace Tarea_03_a.Hubs
{
    public class LoginConVerificacionHub : Hub
    {
        private readonly ILogger<LoginConVerificacionHub> _logger;

        public LoginConVerificacionHub(ILogger<LoginConVerificacionHub> logger)
        {
            _logger = logger;
        }

        public void Login(String correo, String contrasenia)
        {
            _logger.LogInformation("SignalR identificacion del usuario: " + Context.ConnectionId);
            Usuario usr = new Usuario(correo, contrasenia);
            if (usr.EsUsuarioValido())
            {
                if (usr.NecesitarVerificacion())
                { 
                    string usrId = Context.ConnectionId; //mando mail al cliente
                    /*_logger.LogInformation($"**** Copiar la siguiente url para probar");
                    _logger.LogInformation($"curl https://localhost:7269/verificar/usuario/{usrId}"); */
                }
            };
        }
    }
}
