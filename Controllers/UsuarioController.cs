using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Checkpoint.Mvc.Interfaces;
using Senai.Checkpoint.Mvc.Models;
using Senai.Checkpoint.Mvc.Repositorios;

namespace Senai.Checkpoint.Mvc.Controllers {
    public class UsuarioController : Controller {

        public IUsuario UsuarioRepositorio { get; set; }

        public UsuarioController () {

            UsuarioRepositorio = new UsuarioRepositorio ();
        }

        [HttpGet]

        public IActionResult Cadastro () {
            return View ();
        }

        [HttpPost]

        public IActionResult Cadastro (IFormCollection form) {
            UsuarioModel usuarioModel = new UsuarioModel (nome: form["nome"], email: form["email"], senha: form["senha"]);

            UsuarioRepositorio usuarioRepositorio = new UsuarioRepositorio ();

            UsuarioRepositorio.Cadastrar (usuarioModel);

            TempData["Mensagem"] = "Usuário Cadastrado!!";

            return View ();
        }

        [HttpGet]

        public IActionResult Login () => View ();

        [HttpPost]

        public IActionResult Login (IFormCollection form) {

            UsuarioModel usuario = new UsuarioModel (
                email: form["email"],
                senha: form["senha"]
            );

            UsuarioModel usuarioRetornado = UsuarioRepositorio.BuscarEmailSenha (usuario.Email, usuario.Senha);

            if (usuarioRetornado != null) {
                HttpContext.Session.SetString ("IdUsuario", usuarioRetornado.Email.ToString ());

                TempData["Mensagem"] = "Login realizado com sucesso!";

                return RedirectToAction ("Comentar", "Comentarios");

            } else {

                ViewBag.Mensagem = "Acesso negado!";
            }

            return View ();
        }

        public IActionResult Listar () {

            ViewData["Usuarios"] = UsuarioRepositorio.Listar ();

            return View ();
        }

    }
}