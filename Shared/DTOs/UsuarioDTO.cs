using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string CorreoElectronico { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string RepetirContrasena { get; set; }
        public string ExtensionImagen { get; set; }
    }
}
