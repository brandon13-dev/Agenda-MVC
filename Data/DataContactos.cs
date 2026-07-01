using Entity.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataContactos
    {
        AgendaEntities _db = new AgendaEntities();

        // Metodo para obtener contactos por usuario
        public List<ContactoDTO> ObtenerContactosPorUsuario(int usuarioId)
        {
            List<Contactos> ls = _db.Contactos
                                .Where(c => c.UsuarioId == usuarioId)
                                .ToList();

            List<ContactoDTO> lsDTO = new List<ContactoDTO>();

            // Mapeamos los contactos
            foreach (Contactos contacto in ls)
            {
                ContactoDTO contactoDTO = new ContactoDTO()
                {
                    ContactoId = contacto.ContactoId,
                    UsuarioId = contacto.UsuarioId,
                    Nombre = contacto.Nombre,
                    ApellidoPaterno = contacto.ApellidoPaterno,
                    ApellidoMaterno = contacto.ApellidoMaterno,
                    FechaNacimiento = contacto.FechaNacimiento,
                    CorreoElectronico = contacto.CorreoElectronico,
                    ExtensionImagen = contacto.ExtensionImagen,
                };
                lsDTO.Add(contactoDTO);
            }
            return lsDTO;
        }

        // Metodo para Obtener Contacto por ID
        public ContactoDTO ObtenerContactoPorId(int id)
        {
            Contactos contacto = _db.Contactos
                                .Include("ContactoMedios")
                                .FirstOrDefault(c => c.ContactoId == id);

            // Mapeamos a ContactoDTO
            ContactoDTO contactoDTO = new ContactoDTO()
            {
                ContactoId = contacto.ContactoId,
                UsuarioId = contacto.UsuarioId,
                Nombre = contacto.Nombre,
                ApellidoPaterno = contacto.ApellidoPaterno,
                ApellidoMaterno = contacto.ApellidoMaterno,
                FechaNacimiento = contacto.FechaNacimiento,
                CorreoElectronico = contacto.CorreoElectronico,
                ExtensionImagen = contacto.ExtensionImagen,
            };

            // Mapeamos sus medios de contacto
            contactoDTO.ContactoMedios = new List<ContactoMediosDTO>();
            foreach (ContactoMedios medio in contacto.ContactoMedios)
            {
                ContactoMediosDTO cm = new ContactoMediosDTO()
                {
                    ContactoMedioId = medio.ContactoMedioId,
                    ContactoId = medio.ContactoId,
                    TipoContactoId = medio.TipoContactoId,
                    Valor = medio.Valor
                };
                contactoDTO.ContactoMedios.Add(cm);
            }
            return contactoDTO;
        }

        // Metodo para Agregar Contacto
        public void AgregarContacto(ContactoDTO contactoDTO)
        {
            Contactos contactoNuevo = new Contactos()
            {
                Nombre = contactoDTO.Nombre,
                ApellidoPaterno = contactoDTO.ApellidoPaterno,
                ApellidoMaterno = contactoDTO.ApellidoMaterno,
                FechaNacimiento = contactoDTO.FechaNacimiento,
                ExtensionImagen = contactoDTO.ExtensionImagen,
                UsuarioId = contactoDTO.UsuarioId,
                CorreoElectronico = contactoDTO.CorreoElectronico
            };

            _db.Contactos.Add(contactoNuevo);
            _db.SaveChanges();
        }

        // Metodo para Editar Contacto
        public void EditarContacto(ContactoDTO contactoDTO)
        {
            Contactos contactoExistente = _db.Contactos.
                FirstOrDefault(c => c.ContactoId == contactoDTO.ContactoId) ?? throw new ArgumentException("El contacto no existe");

            contactoExistente.Nombre = contactoDTO.Nombre;
            contactoExistente.ApellidoPaterno = contactoDTO.ApellidoPaterno;
            contactoExistente.ApellidoMaterno = contactoDTO.ApellidoMaterno;
            contactoExistente.FechaNacimiento = contactoDTO.FechaNacimiento;
            contactoExistente.ExtensionImagen = contactoDTO.ExtensionImagen;
            contactoExistente.UsuarioId = contactoDTO.UsuarioId;
            contactoExistente.CorreoElectronico = contactoDTO.CorreoElectronico;

            _db.SaveChanges();
        }

        // Metdo para Eliminar un Contacto por su ID
        public void EliminarContacto(int id)
        {
            // Eliminamos sus medios de contacto para evitar excepcion
            List<ContactoMedios> medios = _db.ContactoMedios
                .Where(m => m.ContactoId == id).ToList();
            _db.ContactoMedios.RemoveRange(medios);

            Contactos contactoExistente = _db.Contactos.
                FirstOrDefault(ce => ce.ContactoId == id) ?? throw new ArgumentException("El contacto no existe");
            _db.Contactos.Remove(contactoExistente);
            _db.SaveChanges();
        }

        // Metodo para Buscar Contactos
        public List<ContactoDTO> BuscarContactos(int usuarioId, string busqueda)
        {
            // Buscamos en los contactos
            List<Contactos> ls = _db.Contactos
                .Where(c => c.UsuarioId == usuarioId &&
                           (c.Nombre.Contains(busqueda) ||
                            c.ApellidoPaterno.Contains(busqueda) ||
                            c.CorreoElectronico.Contains(busqueda)))
                .ToList();

            if (ls == null) return null;

            // Mapeamos los contactos encontrados
            List<ContactoDTO> lsDTO = new List<ContactoDTO>();
            foreach (Contactos contacto in ls)
            {
                ContactoDTO contactoDTO = new ContactoDTO()
                {
                    ContactoId = contacto.ContactoId,
                    Nombre = contacto.Nombre,
                    ApellidoPaterno = contacto.ApellidoPaterno,
                    ApellidoMaterno = contacto.ApellidoMaterno,
                    FechaNacimiento = contacto.FechaNacimiento,
                    ExtensionImagen = contacto.ExtensionImagen,
                    UsuarioId = contacto.UsuarioId,
                    CorreoElectronico = contacto.CorreoElectronico
                };
                lsDTO.Add(contactoDTO);
            }

            return lsDTO;
        }
    }
}
