using Blazor.Interfaces;
using Datos.Interfaces;
using Datos.Repositorios;
using Entidades;

namespace Blazor.Servicios
{
    public class LoginServicio : ILoginServicio
    {
        // Solo permite leer el contenido de la propiedad "_configuracion"
        private readonly Config _configuracion;

        // Llamar la Interfaz del Repositorio
        private ILoginRepositorio loginRepositorio; // Nombre Objeto: logInRepositorio

        public LoginServicio(Config config) // Constructor
        {
            _configuracion = config;
            // Configuración del servicio
            // Del objeto "Config" se toma la CadenaConexion
            loginRepositorio = new LoginRepositorio(config.CadenaConexion);
        }

        public async Task<bool> ValidarUsuario(Login logIn)
        {
            return await loginRepositorio.ValidarUsuario(logIn);
        }
    }
}
