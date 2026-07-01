using Business;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agenda_Web.Controllers
{
    public class ContactosController : Controller
    {
        BusinessContactos _businessContactos = new BusinessContactos();
        BusinessMediosContacto _businessMediosContacto = new BusinessMediosContacto();

        // Vista de contactos
        [HttpGet]
        public ActionResult Contactos()
        {
            // Si la sesion es nulla entonces regresamos a la vista de login
            if (Session["Usuario"] == null) return RedirectToAction("Login", "Login");

            // Obtenemos el usuario existente
            UsuarioAutenticadoDTO usuario = (UsuarioAutenticadoDTO)Session["Usuario"];

            // Obtenemos sus contactos
            List<ContactoDTO> contactos = _businessContactos.ObtenerContactosPorUsuario(usuario.UsuarioId).ToList();

            // Mapeamos sus medios de contacto
            foreach (ContactoDTO contacto in contactos)
            {
                contacto.ContactoMedios = _businessMediosContacto.ObtenerMediosPorContacto(contacto.ContactoId);
            }

            return View("Contactos", contactos);
        }

        // Ir a crear contacto
        [HttpGet]
        public ActionResult Create()
        {
            // Si la sesion es nulla entonces regresamos a la vista de login
            if (Session["Usuario"] == null) return RedirectToAction("Login", "Login");

            return View("Create", new ContactoDTO());
        }

        // Crear contacto POST
        [HttpPost]
        public ActionResult CreatePost(ContactoDTO contactoDTO, HttpPostedFile ArchivoImagen)
        {
            try
            {
                // Si la sesion es nulla entonces regresamos a la vista de login
                if (Session["Usuario"] == null) return RedirectToAction("Login", "Login");

                // Obtenemos el usuario existente
                UsuarioAutenticadoDTO usuario = (UsuarioAutenticadoDTO)Session["Usuario"];
                contactoDTO.UsuarioId = usuario.UsuarioId;

                if (ArchivoImagen == null || ArchivoImagen.ContentLength == 0) throw new ArgumentException("Debes de seleccionar una imagen");

                string extension = ArchivoImagen.FileName.Split('.').Last();
                contactoDTO.ExtensionImagen = extension;

                _businessContactos.AgregarContacto(contactoDTO);
                int nuevoId = contactoDTO.UsuarioId;

                string rutaCarpeta = Server.MapPath("~/Img/contactos");
                if (!Directory.Exists(rutaCarpeta)) Directory.CreateDirectory(rutaCarpeta);

                string nombreArchivo = $"{nuevoId}.{extension}";
                string rutaArchivo = Path.Combine(rutaCarpeta, nombreArchivo);
                ArchivoImagen.SaveAs(rutaArchivo);

                return RedirectToAction("MediosContacto", "MediosContacto", new { contactoId = nuevoId });
            }
            catch (ArgumentException ex)
            {
                TempData["error"] = ex.Message;
                return View("Create", contactoDTO);

            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error y no se pudo agregar el contacto. Intente nuevamente.";
                return RedirectToAction("Contactos", "Contactos");
            }
        }

        // Editar contacto
        [HttpGet]
        public ActionResult Edit(int contactoId)
        {
            try
            {
                if (Session["Usuario"] == null) return RedirectToAction("Login", "Login");

                ContactoDTO contacto = _businessContactos.ObtenerContactoPorId(contactoId);
                return View("Edit", contacto);

            }
            catch (ArgumentException ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Contactos", "Contactos");
            }
            catch (Exception)
            {
                Session["Usuario"] = null;
                TempData["error"] = "Ocurrio un error en el servidor.";
                return View("Error");
            }
        }

        // Editar Contacto POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(ContactoDTO contactoDTO, HttpPostedFile ArchivoImagen)
        {
            try
            {
                if (Session["Usuario"] == null) return RedirectToAction("Login", "Login");

                if (ArchivoImagen != null && ArchivoImagen.ContentLength > 0)
                {
                    string extensionVieja = contactoDTO.ExtensionImagen;
                    string nuevaExtension = ArchivoImagen.FileName.Split('.').Last();

                    if (!string.IsNullOrEmpty(extensionVieja) && extensionVieja != nuevaExtension)
                    {
                        string rutaVieja = Server.MapPath($"~/Img/contactos/{contactoDTO.ContactoId}.{extensionVieja}");
                        if (System.IO.File.Exists(rutaVieja)) System.IO.File.Delete(rutaVieja);
                    }

                    contactoDTO.ExtensionImagen = nuevaExtension;

                    string rutaCarpeta = Server.MapPath("~/Img/contactos");
                    string nombreArchivo = $"{contactoDTO.ContactoId}.{nuevaExtension}";
                    string rutaArchivo = Path.Combine(rutaCarpeta, nombreArchivo);
                    ArchivoImagen.SaveAs(rutaArchivo);
                }

                _businessContactos.EditarContacto(contactoDTO);
                return RedirectToAction("Medio", "Medios", new { contactoId = contactoDTO.ContactoId });
            }
            catch (ArgumentException ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Edit", "Contactos", contactoDTO);
            }
            catch (Exception)
            {
                Session["Usuario"] = null;
                TempData["error"] = "Ocurrio un error en el servidor.";
                return View("Error");
            }
        }

        public ActionResult Delete(int contactoId)
        {
            try
            {
                if (Session["Usuario"] == null) return RedirectToAction("Login", "Login");
                _businessContactos.EliminarContacto(contactoId);
                return RedirectToAction("Contactos", "Contactos");
            }
            catch (ArgumentException ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Contactos", "Contactos");
            }
            catch (Exception)
            {
                Session["Usuario"] = null;
                TempData["error"] = "Ocurrio un error en el servidor.";
                return View("Error");
            }
        }

        public ActionResult Buscar(string busqueda)
        {
            try
            {
                if (Session["Usuario"] == null) return RedirectToAction("Login", "Login");

                UsuarioAutenticadoDTO usuario = (UsuarioAutenticadoDTO)Session["Usuario"];
                List<ContactoDTO> contactos = new List<ContactoDTO>();

                contactos = _businessContactos.BuscarContactos(usuario.UsuarioId, busqueda);

                // LLenamos los medios de contacto
                foreach (var c in contactos)
                {
                    c.ContactoMedios = _businessMediosContacto.ObtenerMediosPorContacto(c.ContactoId);
                }

                return View("Contactos", "Contactos", contactos);
            }
            catch (ArgumentException ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Contactos", "Contactos");
            }
            catch (Exception)
            {
                Session["Usuario"] = null;
                TempData["error"] = "Ocurrio un error en el servidor.";
                return View("Error");
            }
        }
    }
}