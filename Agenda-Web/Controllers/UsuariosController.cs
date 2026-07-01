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
    public class UsuariosController : Controller
    {
        BusinessUsuarios _business = new BusinessUsuarios();

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create", new UsuarioDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(UsuarioDTO usuarioDTO, HttpPostedFileBase ArchivoImagen)
        {
            try
            {
                // Validamos que haya imagen
                if (ArchivoImagen == null || ArchivoImagen.ContentLength == 0)
                    throw new ArgumentException("Debe seleecionar una foto");

                // Validamos la extension
                string extension = ArchivoImagen.FileName.Split('.').Last();
                string[] extnesionesPermitidas = { "jpg", "jpeg", "png", "gif", "bmp" };

                if (!extnesionesPermitidas.Contains(extension))
                    throw new ArgumentException("Solo se permiten imagenes JPG, PNG, GIF o BMP");

                // Asignamos la extension al DTO
                usuarioDTO.ExtensionImagen = extension;

                // Agregamos y obtenemos el ID
                int nuevoId = _business.Agregar(usuarioDTO);

                // Guradamos la imagen
                string rutaCarpeta = Server.MapPath("~/Img/usuarios");
                if (!Directory.Exists(rutaCarpeta))
                    Directory.CreateDirectory(rutaCarpeta);

                string nombreArchivo = $"{nuevoId}.{extension}";
                string rutaArchivo = Path.Combine(rutaCarpeta, nombreArchivo);
                ArchivoImagen.SaveAs(rutaArchivo);

                TempData["success"] = "Usuario creado correctamente";
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                TempData["error"] = ex.Message;
                return View("Create", usuarioDTO);
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error al comunicarse con el servidor";
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                UsuarioDTO usuarioDTO = _business.Obtener(id);
                return View("Edit", usuarioDTO);
            }
            catch (ArgumentException ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error al comunicarse con el servidor";
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(UsuarioDTO usuarioDTO)
        {
            try
            {
                _business.Editar(usuarioDTO);
                TempData["success"] = "Usuario modificado correctamente";
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                TempData["error"] = ex.Message;
                return View("Edit", usuarioDTO);
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error al comunicarse con el servidor";
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                UsuarioDTO usuarioDTO = _business.Obtener(id);
                return View("Delete", usuarioDTO);
            }
            catch (ArgumentException ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error al comunicarse con el servidor";
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            try
            {
                _business.Eliminar(id);
                TempData["success"] = "Pedido Eliminado Correctamente";
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                TempData["error"] = ex.Message;
                UsuarioDTO usuarioDTO = _business.Obtener(id);
                return View("Delete", usuarioDTO);
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrio un error al comunicarse con el servidor";
                return View("Error");
            }
        }
    }
}