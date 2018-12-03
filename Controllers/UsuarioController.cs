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

            //Busca o usuario pelo seu e-mail e senha
            UsuarioModel usuarioRetornado = UsuarioRepositorio.BuscarEmailSenha (usuario.Email, usuario.Senha);

            //Verifica se o usuário existe
            if (usuarioRetornado != null) {
                //Caso usuário exista salva os dados(nome e email) na session
                HttpContext.Session.SetString ("nomeUsuario", usuarioRetornado.Nome.ToString ());
                HttpContext.Session.SetString ("emailUsuario", usuarioRetornado.Email.ToString ());

                //Informa ao usuário que o login foi efetuado
                TempData["Login"] = "Login realizado com sucesso!";

                //Redireciona para a página de comentários
                return RedirectToAction ("Comentar", "Comentarios");

            } else {
                //Caso não exista informa ao usuário
                ViewBag.Mensagem = "Acesso negado!";
            }

            //Retorna a view de usuário
            return View ();
        }

        public IActionResult Listar () {

            ViewData["Usuarios"] = UsuarioRepositorio.Listar ();

            return View ();
        }

    }
}