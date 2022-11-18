namespace Blazor
{
    public class Config
    {
        public string CadenaConexion { get; set; }

        // Constructor para pasarle valor a la CadenaConexion
        public Config(string cadenaConexion)
        {
            CadenaConexion = cadenaConexion;
        }
    }
}
