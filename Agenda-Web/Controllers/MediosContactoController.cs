using Business;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Agenda_Web.Controllers
{
    public class MediosContactoController : Controller
    {
        BusinessContactos _businessContacto = new BusinessContactos();
        BusinessMediosContacto _businessMedios = new BusinessMediosContacto();
        BusinessTiposContacto _businessTipos = new BusinessTiposContacto();

        public ActionResult MediosContacto(int contactoId)
        {
            if (Session["Usuario"] == null) return RedirectToAction("Login", "Login");

            ContactoDTO contactoDTO = _businessContacto.ObtenerContactoPorId(contactoId);
            contactoDTO.ContactoMedios = _businessMedios.ObtenerMediosPorContacto(contactoId);

            ViewBag.TiposContacto = _businessTipos.ObtenerTiposDeContacto().ToList();

            return View("MediosContacto", contactoDTO);
        }

        public ActionResult AgregarMedio(ContactoMediosDTO medio)
        {
            _businessMedios.AgregarMedioContacto(medio);
            return RedirectToAction("MediosContacto", new { contactoId = medio.ContactoId });
        }

        public ActionResult EliminarMedio(int contactoMedioId, int contactoId)
        {
            _businessMedios.EliminarMedioContacto(contactoMedioId);
            return RedirectToAction("MediosContacto", new { contactoId = contactoId });
        }
    }
}