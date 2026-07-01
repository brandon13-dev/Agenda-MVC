using Entity.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataMediosContacto
    {
        AgendaEntities _db = new AgendaEntities();

        // Agregar Medio de Contacto
        public void AgregarMedioContacto(ContactoMediosDTO contactoMediosDTO)
        {
            ContactoMedios nuevo = new ContactoMedios()
            {
                ContactoId = contactoMediosDTO.ContactoId,
                TipoContactoId = contactoMediosDTO.TipoContactoId,
                Valor = contactoMediosDTO.Valor,
            };

            _db.ContactoMedios.Add(nuevo);
            _db.SaveChanges();
        }

        // Eliminar medio de contacto
        public void EliminarMedioContacto(int contactoMedioId)
        {
            ContactoMedios cmExistente = _db.ContactoMedios
                .FirstOrDefault(cm => cm.ContactoMedioId == contactoMedioId) ?? throw new ArgumentException("No existe ese medio de contacto");

            _db.ContactoMedios.Remove(cmExistente);
            _db.SaveChanges();
        }

        // Obtener los medios de contacto por ID del contacto
        public List<ContactoMediosDTO> ObtenerMediosPorContacto(int contactoId)
        {
            List<ContactoMedios> ls = _db.ContactoMedios
                .Include("TiposContacto")
                .Where(cm => cm.ContactoId == contactoId)
                .ToList();

            List<ContactoMediosDTO> lsDTO = new List<ContactoMediosDTO>();
            foreach (ContactoMedios cm in ls)
            {
                ContactoMediosDTO cmDTO = new ContactoMediosDTO()
                {
                    ContactoMedioId = cm.ContactoMedioId,
                    ContactoId = cm.ContactoId,
                    TipoContactoId = cm.TipoContactoId,
                    Valor = cm.Valor,
                    Descripcion = cm.TiposContacto.Descripcion
                };
                lsDTO.Add(cmDTO);
            }

            return lsDTO;
        }
    }
}
