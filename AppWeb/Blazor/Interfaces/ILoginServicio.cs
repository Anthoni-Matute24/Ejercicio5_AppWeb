using Entidades;

namespace Blazor.Interfaces
{
    public interface ILoginServicio
    {
        Task<bool> ValidarUsuario(Login logIn);
    }
}
