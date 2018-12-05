using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Checkpoint.Mvc.Interfaces;
using Senai.Checkpoint.Mvc.Models;
using Senai.Checkpoint.Mvc.Repositorios;

namespace Senai.Checkpoint.Mvc.Controllers {
    public class ComentariosController : Controller {

        public IComentario ComentariosRepositorio { get; set; }

        public ComentariosController () {

            ComentariosRepositorio = new ComentariosRepositorio ();
        }

        [HttpGet]
        public IActionResult Comentar () {
            string id = HttpContext.Session.GetString ("idUsuario");

            if (id != null) {
                int idInt = int.Parse (id);

                UsuarioRepositorio usuarioRep = new UsuarioRepositorio ();

                UsuarioModel usuario = usuarioRep.BuscarId (idInt);

                ViewBag.Nome = usuario.Nome;
                ViewBag.Email = usuario.Email;
                ViewBag.Mensagem = "Comentário enviado para aprovação.";
            }

            return View ();

        }

        [HttpPost]
        public IActionResult Comentar (IFormCollection form) {

            if (HttpContext.Session.GetString ("nomeUsuario") == null) {
                return RedirectToAction ("Usuario", "Login");
            }

            String Nome = HttpContext.Session.GetString ("nomeUsuario");

            String Email = HttpContext.Session.GetString ("emailUsuario");

            ComentariosModel comentariosModel = new ComentariosModel (nome: Nome, email: Email, comentario: form["comentario"]);

            ComentariosRepositorio.Comentar (comentariosModel);

            return View ();
        }

        public IActionResult Listar () {

            ViewData["Comentarios"] = ComentariosRepositorio.Listar ();

            return View ();
        }

        [HttpGet]

        public IActionResult Administrar () {

            return View ();
        }

        [HttpPost]

        public IActionResult Administrar (IFormCollection form) {

            if (HttpContext.Session.GetString ("nomeUsuario") != "Adminitrador") {
                return RedirectToAction ("Usuario", "Login");
            }

            String Nome = HttpContext.Session.GetString ("nomeUsuario");

            String Email = HttpContext.Session.GetString ("emailUsuario");

            ComentariosModel comentariosModel = new ComentariosModel (nome: Nome, email: Email, comentario: form["comentario"]);

            ComentariosRepositorio.Administrar (comentariosModel);

            return View ();
        }

        public IActionResult Mostrar () {

            ViewData["Avaliacao"] = ComentariosRepositorio.Mostrar ();

            return View ();
        }

    }
}