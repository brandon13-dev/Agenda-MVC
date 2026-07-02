using Entity.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataLogin
    {
        AgendaEntities _db = new AgendaEntities();

        public UsuarioAutenticadoDTO ObtenerLogin(string usuario, string contrasena)
        {
            Usuarios usuarioExistente = _db.Usuarios
                                .FirstOrDefault(u => u.NombreUsuario == usuario &&
                                                     u.Contrasena == contrasena);

            if (usuarioExistente == null) return null;

            return new UsuarioAutenticadoDTO()
            {
                UsuarioId = usuarioExistente.UsuarioId,
                Nombre = usuarioExistente.Nombre,
                ApellidoMaterno = usuarioExistente.ApellidoMaterno,
                ApellidoPaterno = usuarioExistente.ApellidoMaterno,
                CorreoElectronico = usuarioExistente.CorreoElectronico,
                NombreUsuario = usuarioExistente.NombreUsuario,
                ExtensionImagen = usuarioExistente.ExtensionImagen
            };
        }
    }
}
