using Data;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BusinessMediosContacto
    {
        DataMediosContacto _data = new DataMediosContacto();

        // Agregar Medio de contacto
        public void AgregarMedioContacto(ContactoMediosDTO cmDTO)
        {
            ValidarContactoMediosDTO(cmDTO);

            _data.AgregarMedioContacto(cmDTO);
        }

        // Eliminar Medio Contacto
        public void EliminarMedioContacto(int medioContactoId)
        {
            if (medioContactoId <= 0) throw new ArgumentException("Id del medio de contacto invalido.");

            _data.EliminarMedioContacto(medioContactoId);
        }

        // Obtener los Medios de Contacto
        public List<ContactoMediosDTO> ObtenerMediosPorContacto(int contactoId)
        {
            if (contactoId <= 0) throw new ArgumentException("Id del contacto invalido.");

            return _data.ObtenerMediosPorContacto(contactoId);
        }

        // Metodo auxiliar para validar el form de medios de contacto
        private void ValidarContactoMediosDTO(ContactoMediosDTO cmDTO)
        {
            string errores = "";
            if (cmDTO.ContactoId <= 0) errores += "Id del contacto invalido <br />";
            if (cmDTO.TipoContactoId <= 0) errores += "Id del tipo de contacto invalido. <br />";
            if (string.IsNullOrWhiteSpace(cmDTO.Valor)) errores += "El valor del contacto es obligatorio. <br />";
            if (string.IsNullOrWhiteSpace(cmDTO.Descripcion)) errores += "La descripcion del medio del contacto es obligatoria. <br />";

            if (string.IsNullOrEmpty(errores)) throw new ArgumentException(errores);
        }
    }
}
