using Data;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BusinessLogin
    {
        DataLogin _data = new DataLogin();

        public UsuarioAutenticadoDTO ObtenerLogin(string usuario, string contrasena)
        {
            // Validmos el Login
            ValidarFormLogin(usuario, contrasena);

            // Si no son vacios -> Obtenemos el login
            UsuarioAutenticadoDTO login = _data.ObtenerLogin(usuario, contrasena);

            // Si el login retorna usuario vacio
            if (login == null)
            {
                throw new ArgumentException("Usuario o contrasena incorrectos.");
            }

            return login;
        }

        // Metodo para Validar el Form de Login
        private void ValidarFormLogin(string usuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(usuario))
            {
                throw new ArgumentException("El usuario es requerido. <br />");
            }
            if (string.IsNullOrWhiteSpace(contrasena))
            {
                throw new ArgumentException("La contrasena es requerida. <br />");
            }
        }
    }
}
