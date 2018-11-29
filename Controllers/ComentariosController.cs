using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Checkpoint.Mvc.Models;

namespace Senai.Checkpoint.Mvc.Controllers {
    public class ComentariosController : Controller {

        [HttpGet]

        public IActionResult Comentar () {

            return View ();

        }

        [HttpPost]

        public IActionResult Comentar (IFormCollection form) {

            ComentariosModel usuario = new ComentariosModel ();
            usuario.ID = HttpContext.Session.GetInt32 ("idUsuario").Value;
            usuario.Nome = HttpContext.Session.GetString ("nomeUsuario");
            usuario.Email = HttpContext.Session.GetString ("emailUsuario");

            ComentariosModel comentarios = new ComentariosModel ();

            if (System.IO.File.Exists ("comentarios.csv")) {
                String[] linhas = System.IO.File.ReadAllLines ("comentarios.csv");
                comentarios.ID = linhas.Length + 1;
            } else {
                comentarios.ID = 1;
            }

            comentarios.Comentario = form["comentario"];
            comentarios.DataCriacao = DateTime.Now;

            using (StreamWriter sw = new StreamWriter ("comentarios.csv", true)) {
                sw.WriteLine ($"{comentarios.ID};{comentarios.Nome};{comentarios.Email};{comentarios.Comentario};{comentarios.DataCriacao}");
            }

            TempData["Mensagem"] = "Comentário enviado para aprovação.";

            return View ();

        }

    }
}