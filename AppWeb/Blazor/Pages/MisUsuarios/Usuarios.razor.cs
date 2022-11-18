using Blazor.Interfaces;
using Entidades;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.MisUsuarios
{
    public partial class Usuarios
    {
        // Inyección del servicio de la clase Program.cs para acceder a los métodos de UsuarioServicio
        [Inject] private IUsuarioServicio usuarioServicio { get; set; }

        private IEnumerable<Usuario> lista { get; set; }

        // Cargar lista de usuarios al llamar el componente
        protected override async Task OnInitializedAsync()
        {
            lista = await usuarioServicio.GetLista();
        }
    }
}
