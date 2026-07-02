using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agenda_Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Business
        BusinessLogin _business = new BusinessLogin();
        // Vista principal para login
        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult ObtenerLogin(string usuario, string password)
        {
            try
            {
                Session["Usuario"] = _business.ObtenerLogin(usuario, password);
                TempData["success"] = "Usuario logeado correctamente";
                return RedirectToAction("Contactos", "Contactos");
            }
            catch (ArgumentException ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Ocurrio un error en el servidor:" + ex;
                return View("Error");
            }
        }

        public ActionResult CerrarSesion()
        {
            Session["Usuario"] = null;
            return RedirectToAction("Login");
        }
    }
}