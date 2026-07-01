using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class ContactoMediosDTO
    {
        public int ContactoMedioId { get; set; }
        public int ContactoId { get; set; }
        public int TipoContactoId { get; set; }
        public string Valor { get; set; }
        public string Descripcion { get; set; }
    }
}
