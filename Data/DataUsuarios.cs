using Entity.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataUsuarios
    {
        readonly AgendaEntities _db = new AgendaEntities();

        // Metodo para Listar Usuarios (solo para administradores)
        public List<UsuarioDTO> Obtener()
        {
            List<Usuarios> listaUsuarios = _db.Usuarios.ToList();
            List<UsuarioDTO> listaUsuariosDTO = new List<UsuarioDTO>();

            // Mapeamos la lista
            foreach (Usuarios usuario in listaUsuarios)
            {
                listaUsuariosDTO.Add(MapUsuarioToDTO(usuario));
            }

            // Retornamos la lista de Usuarios
            return listaUsuariosDTO;
        }

        // Metodo para Obtener un Usuario por ID
        public UsuarioDTO Obtener(int id)
        {
            // Obtenemos el usuario
            Usuarios usuario = _db.Usuarios
                .FirstOrDefault(u => u.UsuarioId == id);

            // Mapeamos y retornamos al Usuario encontrado
            return usuario != null ? MapUsuarioToDTO(usuario) : null;
        }

        // Metodo para Agregar Usuario
        public int Agregar(UsuarioDTO usuarioDTO)
        {
            // Mapeamos el Usuario para poderlo agregar
            Usuarios usuario = MapDTOToUsuario(usuarioDTO);

            _db.Usuarios.Add(usuario);
            _db.SaveChanges();

            return usuario.UsuarioId;
        }

        // Metodo para Editar un Cliente
        public void Editar(UsuarioDTO usuarioDTO)
        {
            Usuarios usuario = _db.Usuarios
                .FirstOrDefault(u => u.UsuarioId == usuarioDTO.UsuarioId);

            if (usuario == null) return;

            usuario.Nombre = usuarioDTO.Nombre;
            usuario.ApellidoPaterno = usuarioDTO.ApellidoPaterno;
            usuario.ApellidoMaterno = usuarioDTO.ApellidoMaterno;
            usuario.NombreUsuario = usuarioDTO.NombreUsuario;
            usuario.CorreoElectronico = usuarioDTO.CorreoElectronico;
            usuario.Contrasena = usuarioDTO.Contrasena;
            usuario.ExtensionImagen = usuarioDTO.ExtensionImagen;

            _db.SaveChanges();
        }

        // Metodo para Eliminar un Cliente
        public void Eliminar(int id)
        {
            // Obtenemos el usuario
            var usuario = _db.Usuarios
                .FirstOrDefault(u => u.UsuarioId == id);

            // Validamos que el usuario no sea nulo
            if (usuario != null)
            {
                _db.Usuarios.Remove(usuario);
                _db.SaveChanges();
            }
        }

        // Metodo privado para Validar Existencia del Usuario
        public bool ExisteUsuario(int id)
        {
            var query = _db.Usuarios
                .Where(u => u.UsuarioId == id);

            return query.Any();
        }

        // Metodo privado para Validar que el NombreUsuario no esta en la base de datos
        public bool ValidarNombreUsuario(string nombreUsuario, int? idUsuarioActual = null)
        {
            var query = _db.Usuarios
                .Where(u => u.NombreUsuario == nombreUsuario);

            if (idUsuarioActual.HasValue)
            {
                query = query.Where(u => u.UsuarioId != idUsuarioActual.Value);
            }

            return query.Any();
        }

        // Metodo privado para Validar que el Correo Electronico no esta en la base de datos
        public bool ValidarCorreoElectronico(string correoElectronico, int? idUsuarioActual = null)
        {
            var query = _db.Usuarios
                .Where(u => u.CorreoElectronico == correoElectronico);

            if (idUsuarioActual.HasValue)
            {
                query = query.Where(u => u.UsuarioId != idUsuarioActual.Value);
            }

            return query.Any();
        }

        // Metodo privado para Mapear de Usuario a UsuarioDTO
        private UsuarioDTO MapUsuarioToDTO(Usuarios usuario)
        {
            UsuarioDTO usuarioDTO = new UsuarioDTO()
            {
                UsuarioId = usuario.UsuarioId,
                Nombre = usuario.Nombre,
                ApellidoPaterno = usuario.ApellidoPaterno,
                ApellidoMaterno = usuario.ApellidoMaterno,
                CorreoElectronico = usuario.CorreoElectronico,
                NombreUsuario = usuario.NombreUsuario,
                Contrasena = usuario.Contrasena,
                ExtensionImagen = usuario.ExtensionImagen
            };

            return usuarioDTO;
        }

        // Metodo privado para Mapear de UsuarioDTO a Usuario
        private Usuarios MapDTOToUsuario(UsuarioDTO usuarioDTO)
        {
            Usuarios usuario = new Usuarios()
            {
                Nombre = usuarioDTO.Nombre,
                ApellidoPaterno = usuarioDTO.ApellidoPaterno,
                ApellidoMaterno = usuarioDTO.ApellidoMaterno,
                CorreoElectronico = usuarioDTO.CorreoElectronico,
                NombreUsuario = usuarioDTO.NombreUsuario,
                Contrasena = usuarioDTO.Contrasena,
                ExtensionImagen = usuarioDTO.ExtensionImagen
            };

            return usuario;
        }
    }
}
