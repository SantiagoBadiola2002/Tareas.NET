using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

public class LoginHub : Hub
{
    private readonly ILogger<LoginHub> _logger;

    public LoginHub(ILogger<LoginHub> logger)
    {
        _logger = logger;
    }

    public async Task Login(string email, string password)
    {
        _logger.LogInformation("SignalR identificación del usuario: " + Context.ConnectionId);
        _logger.LogInformation("Correo: " + email + " Contraseña: " + password);

        if (email == "admin" && password == "123")
        {
            _logger.LogInformation("Correo y contraseña correctos");
            await Clients.Caller.SendAsync("LoginResult", true); // Notifica que el login fue exitoso
        }
        else
        {
            _logger.LogInformation("Correo y contraseña incorrectos");
            await Clients.Caller.SendAsync("LoginResult", false); // Notifica que el login falló
        }
    }
}
