using Entidades;

namespace Datos.Interfaces
{
    public interface IUsuarioRepositorio
    {
        // Métodos
        Task<Usuario> GetPorCodigo(string codigo); // Devuelve un usuario
        Task<IEnumerable<Usuario>> GetLista(); // Devuelve la lista de usuarios.
    }
}
