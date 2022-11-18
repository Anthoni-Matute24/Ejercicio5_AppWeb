using Dapper;
using Datos.Interfaces;
using Entidades;
using MySql.Data.MySqlClient;

namespace Datos.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        // Variable para capturar la información al llamar "ILogInRepositorio" en el servicio de Blazor
        private string CadenaConexion;

        // Constructor para pasar valores a "CadenaConexion"
        public UsuarioRepositorio(string _cadenaConexion)
        {
            CadenaConexion = _cadenaConexion;
        }

        // Método para establecer una conexión de MySQL
        private MySqlConnection EstablecerConexion()
        {
            return new MySqlConnection(CadenaConexion); // Retorna un nuevo objeto de conexión
            // Contiene la IP del servidor, el puerto, la BD, la contraseña, el usuario, etc.
        }

        public async Task<IEnumerable<Usuario>> GetLista()
        {
            IEnumerable<Usuario> lista = new List<Usuario>();
            try
            {
                // Abrir una nueva conexión 
                using MySqlConnection conexion = EstablecerConexion();
                await conexion.OpenAsync();
                string sql = "SELECT * FROM usuario;";
                // Trae un solo registro
                lista = await conexion.QueryAsync<Usuario>(sql);
            }
            catch (Exception ex)
            {
            }
            return lista;
        }

        public async Task<Usuario> GetPorCodigo(string codigo)
        {
            Usuario user = new Usuario();
            try
            {
                // Abrir una nueva conexión 
                using MySqlConnection conexion = EstablecerConexion();
                await conexion.OpenAsync();
                string sql = "SELECT * FROM usuario WHERE Codigo = @Codigo;";
                // Trae un solo registro
                user = await conexion.QueryFirstAsync<Usuario>(sql, new { codigo });
            }
            catch (Exception ex)
            {
            }
            return user;
        }
    }
}
