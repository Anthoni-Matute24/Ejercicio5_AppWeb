using Datos.Interfaces;
using Datos.Repositorios;
using Entidades;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blazor.Controllers
{
    public class LoginController : Controller
    {
        // Solo permite leer el contenido de la propiedad "_configuracion"
        private readonly Config _configuracion;

        // Llamar la Interfaz del Repositorio
        private ILoginRepositorio _logInRepositorio; // Nombre Objeto: _logInRepositorio
        private IUsuarioRepositorio _usuarioRepositorio; // Nombre Objeto: _usuarioRepositorio

        public LoginController(Config config)
        {
            _configuracion = config;
            _logInRepositorio = new LoginRepositorio(config.CadenaConexion);
            _usuarioRepositorio = new UsuarioRepositorio(config.CadenaConexion);
        }

        // Verbos HTTP
        [HttpPost("/account/login")]

        // IActionResult: permite regresar el estatus de dicha petición, es decir, si fue exitosa o no fue exitosa 
        // Método para iniciar sesión
        public async Task<IActionResult> LogIn(Login logIn)
        {
            string Rol = string.Empty;

            try
            {
                // Devuelve True si el usuario está registrado
                bool usuarioValido = await _logInRepositorio.ValidarUsuario(logIn);

                if (usuarioValido) // Si está registrado, entonces devolverá los datos de ese registro
                {
                    Usuario user = await _usuarioRepositorio.GetPorCodigo(logIn.Codigo);

                    if (user.EstaActivo) // Validar si está activo
                    {
                        Rol = user.Rol;

                        var claims = new[] // Arreglo de Claims para pasar el nombre y rol del usuario.
                        {
                            new Claim(ClaimTypes.Name, user.Codigo),
                            new Claim(ClaimTypes.Role, Rol)
                        };

                        // Tipo de autenticación: Cookies
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        /* Método de iniciar sesión
                         * IsPersistent = true: la sesión siempre estará activa
                         * ExpiresUtc = DateTime.UtcNow.AddMinutes(#): Tiempo que durará la sesión mientras el usuario está inactivo
                        */
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddMinutes(5) });
                    }
                    else
                    {   // Si el usuario no está activo, lo direccionará nuevamente al LogIn y mostrará un mensaje de alerta
                        return LocalRedirect("/login/El usuario no esta activo");
                    }
                }
                else
                {   // Si el usuario ingresa datos incorrectos, dará un mensaje de alerta
                    return LocalRedirect("/login/Datos de usuario invalidos");
                }
            }
            catch (Exception ex)
            {
            }
            return LocalRedirect("/");
        }

        [HttpGet("/account/logout")]

        // Método para cerrar sesión
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/login");
        }
    }
}
