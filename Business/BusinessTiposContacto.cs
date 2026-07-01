using Data;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BusinessTiposContacto
    {
        DataTiposContacto _data = new DataTiposContacto();

        public List<TiposContactoDTO> ObtenerTiposDeContacto()
        {
            return _data.ObtenerTiposContacto();
        }
    }
}
