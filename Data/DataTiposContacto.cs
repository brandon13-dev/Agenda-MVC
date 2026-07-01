using Entity.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataTiposContacto
    {
        AgendaEntities _db = new AgendaEntities();

        // Obtenemos los tipos de contacto
        public List<TiposContactoDTO> ObtenerTiposContacto()
        {
            List<TiposContacto> ls = _db.TiposContacto.ToList();

            List<TiposContactoDTO> lsDTO = new List<TiposContactoDTO>();
            foreach (TiposContacto tipo in ls)
            {
                TiposContactoDTO tipoDTO = new TiposContactoDTO()
                {
                    TipoContactoId = tipo.TipoContactoId,
                    Descripcion = tipo.Descripcion
                };
                lsDTO.Add(tipoDTO);
            }
            return lsDTO;
        }
    }
}
