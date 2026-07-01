using Data;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BusinessUsuarios
    {
        readonly DataUsuarios _data = new DataUsuarios();

        // Metodo para Obtener Usuarios
        public List<UsuarioDTO> Obtener()
        {
            return _data.Obtener();
        }

        // Metodo para Obtener un Usuario por ID
        public UsuarioDTO Obtener(int id)
        {
            // Validmos el id 
            ValidarId(id);

            // Obtenemos el usuario
            var usuario = _data.Obtener(id) ?? throw new ArgumentException("No existe el usuario con el ID");

            // Retornamos el usuario obtenido
            return usuario;
        }

        // Metodo para Agregar Usuario
        public int Agregar(UsuarioDTO usuarioDTO)
        {
            // Validamos los campos del DTO
            ValidarDTO(usuarioDTO);

            // Validamos que no este registrado el correo electronico ni nombre de usuario
            ValidarUnicos(usuarioDTO.CorreoElectronico, usuarioDTO.NombreUsuario);

            // Si todo esta bien entonce agregamos
            return _data.Agregar(usuarioDTO);
        }

        // Metodo para Editar Usuario
        public void Editar(UsuarioDTO usuarioDTO)
        {
            // Validamos los campos del DTO
            ValidarDTO(usuarioDTO);

            //Validsmoas el ID del DTO
            ValidarId(usuarioDTO.UsuarioId);

            // Validamos existencia + unicos
            ValidarUsuarioExistenteYUnicos(usuarioDTO);

            // Si todo esta bien entonces editamos
            _data.Editar(usuarioDTO);
        }

        // Metodo para Eliminar Usuario
        public void Eliminar(int id)
        {
            // Validamos el Id
            ValidarId(id);

            // Validamos la existencia del usuario
            ExisteUsuario(id);

            // Si esta bien entonces eliminamos
            _data.Eliminar(id);
        }

        // Metodo auxiliar para validar UsuarioDTO
        private void ValidarDTO(UsuarioDTO usuarioDTO)
        {
            string errores = "";
            if (string.IsNullOrWhiteSpace(usuarioDTO.Nombre))
            {
                errores += "El nombre es obligatorio.<br />";
            }
            if (string.IsNullOrWhiteSpace(usuarioDTO.ApellidoPaterno))
            {
                errores += "El apellido paterno es obligatorio.<br />";
            }
            if (string.IsNullOrWhiteSpace(usuarioDTO.CorreoElectronico))
            {
                errores += "El correo electronico es obligatorio.<br />";
            }
            if (string.IsNullOrWhiteSpace(usuarioDTO.NombreUsuario))
            {
                errores += "El nombre de usuario es obligatorio.<br />";
            }
            if (string.IsNullOrWhiteSpace(usuarioDTO.Contrasena))
            {
                errores += "La contraseña es obligatoria.<br />";
            }
            if (usuarioDTO.Contrasena != usuarioDTO.RepetirContrasena)
            {
                errores += "Las contrasenas no coinciden.<br />";
            }
            if (string.IsNullOrWhiteSpace(usuarioDTO.ExtensionImagen))
            {
                errores += "La imagen es obligatoria.<br />";
            }

            if (!string.IsNullOrEmpty(errores))
                throw new ArgumentException(errores);
        }

        // Metodo para Validar Unicos
        private void ValidarUnicos(string correo, string nombreUsuario, int? idActual = null)
        {
            if (_data.ValidarCorreoElectronico(correo, idActual))
            {
                throw new ArgumentException("El correo electronico ya existe.");
            }
            if (_data.ValidarNombreUsuario(nombreUsuario, idActual))
            {
                throw new ArgumentException("El nombre de usuario ya existe.");
            }

        }

        // Metodo para Validar existencia + unicos
        private void ValidarUsuarioExistenteYUnicos(UsuarioDTO usuarioDTO)
        {
            // Verificamos que existe el usuario
            ExisteUsuario(usuarioDTO.UsuarioId);

            // Luuego validamos unicos
            ValidarUnicos(
                usuarioDTO.CorreoElectronico,
                usuarioDTO.NombreUsuario,
                usuarioDTO.UsuarioId
            );
        }

        // Metodo para validar el id
        private void ValidarId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID invalido");
            }
        }

        // Metodo para validar que exista el Usuario
        private void ExisteUsuario(int id)
        {
            if (!_data.ExisteUsuario(id))
            {
                throw new ArgumentException("No existe el usuario con el ID");
            }
        }
    }
}
