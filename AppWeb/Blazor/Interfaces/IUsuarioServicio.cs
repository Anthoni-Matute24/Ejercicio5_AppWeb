using Entidades;

namespace Blazor.Interfaces
{
    public interface IUsuarioServicio
    {
        // Métodos
        Task<Usuario> GetPorCodigo(string codigo); // Devuelve un usuario
        Task<IEnumerable<Usuario>> GetLista(); // Devuelve la lista de usuarios.
    }
}
