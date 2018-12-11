using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            }

            return View ();

        }

        [HttpPost]
        public IActionResult Comentar (IFormCollection form) {

            if (HttpContext.Session.GetString ("nomeUsuario") == null) {
                return RedirectToAction ("Login", "Usuario");
            }

            String Nome = HttpContext.Session.GetString ("nomeUsuario");

            String Email = HttpContext.Session.GetString ("emailUsuario");

            ComentariosModel comentariosModel = new ComentariosModel (nome: Nome, email: Email, comentario: form["comentario"]);

            ComentariosRepositorio.Comentar (comentariosModel);

            TempData["Coment"] = "Comentário enviado para aprovação!";

            return View ();
        }

        public IActionResult Listar () {

            String Email = HttpContext.Session.GetString ("emailUsuario");

            if (Email == "admin@carfel.com") {

                ViewData["Comentarios"] = ComentariosRepositorio.Listar ();

                // .OrderByDescending (x => x.DataCriacao)
                
            }

            return View ();
        }

        public IActionResult Aprovados () {

            ViewData["Aprovados"] = ComentariosRepositorio.Aprovados ();

            return View ();
        }

        [HttpGet]

        public IActionResult Aprovar (int id) {
            ComentariosRepositorio Comentario = new ComentariosRepositorio ();
            Comentario.Aprovar (id);

            TempData["Aprovar"] = "Comentário aprovado!";

            return RedirectToAction ("Listar", "Comentarios");
        }

        [HttpGet]

        public IActionResult Rejeitar (int id) {
            ComentariosRepositorio Comentario = new ComentariosRepositorio ();
            Comentario.Rejeitar (id);

            TempData["Rejeitar"] = "Comentário rejeitado!";

            return RedirectToAction ("Listar", "Comentarios");
        }

    }
}