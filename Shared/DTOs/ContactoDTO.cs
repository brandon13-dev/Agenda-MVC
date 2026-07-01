using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class ContactoDTO
    {
        public int ContactoId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string ExtensionImagen { get; set; }
        public int UsuarioId { get; set; }
        public string CorreoElectronico { get; set; }
        public List<ContactoMediosDTO> ContactoMedios { get; set; }
    }
}
