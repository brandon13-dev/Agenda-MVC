using Data;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BusinessContactos
    {
        DataContactos _data = new DataContactos();

        // Obtener toda la lista de contactos
        public List<ContactoDTO> ObtenerContactosPorUsuario(int usuarioId)
        {
            ValidarId(usuarioId);

            return _data.ObtenerContactosPorUsuario(usuarioId);
        }

        // Obtener un contacto por su id
        public ContactoDTO ObtenerContactoPorId(int contactoId)
        {
            ValidarId(contactoId);

            return _data.ObtenerContactoPorId(contactoId);
        }

        // Agregar contacto
        public void AgregarContacto(ContactoDTO contactoDTO)
        {
            ValidarContacto(contactoDTO);

            _data.AgregarContacto(contactoDTO);
        }

        // Editar contacto
        public void EditarContacto(ContactoDTO contactoDTO)
        {
            if (contactoDTO.ContactoId <= 0) throw new ArgumentException("Id del Contacto invalido.");
            ValidarContacto(contactoDTO);

            _data.EditarContacto(contactoDTO);
        }

        public void EliminarContacto(int contactoId)
        {
            ValidarId(contactoId);

            _data.EliminarContacto(contactoId);
        }

        // Buscar Contactos
        public List<ContactoDTO> BuscarContactos(int usuarioId, string busqueda)
        {
            if (usuarioId <= 0) throw new ArgumentException("Id de Usuario invalido");
            if (string.IsNullOrWhiteSpace(busqueda)) return ObtenerContactosPorUsuario(usuarioId);

            return _data.BuscarContactos(usuarioId, busqueda);
        }

        // Metodo auxiliar para validar contacto
        private void ValidarContacto(ContactoDTO contactoDTO)
        {
            string errores = "";
            if (contactoDTO.UsuarioId <= 0) errores += "El Id del Usuario es invalido. <br />";
            if (string.IsNullOrWhiteSpace(contactoDTO.Nombre)) errores += "El nombre del contacto es obligatorio. <br />";
            if (string.IsNullOrWhiteSpace(contactoDTO.ApellidoPaterno)) errores += "El apellido paterno del contacto es obligatorio. <br />";
            if (string.IsNullOrWhiteSpace(contactoDTO.ApellidoMaterno)) errores += "El apellido materno del contacto es obligatorio. <br />";
            if (contactoDTO.FechaNacimiento == null) errores += "La fecha de nacimiento del contacto es obligatoria. <br />";
            if (string.IsNullOrWhiteSpace(contactoDTO.ExtensionImagen)) errores += "La imagen del contacto es obligatoria. <br />";
            if (string.IsNullOrWhiteSpace(contactoDTO.CorreoElectronico)) errores += "El correo electronico del contacto es obliogatorio. <br />";

            if (string.IsNullOrEmpty(errores)) throw new ArgumentException(errores);
        }

        // Metodo auxiliar para validar el id
        private void ValidarId(int id)
        {
            if (id <= 0) throw new ArgumentException("Id invalido");
        }
    }
}
