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

            if (form["senha"] != form["confirma"]) {
                TempData["Erro"] = "Senhas não coincidem!!";
                return View ();
            }

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

                if (usuarioRetornado.Email != "admin@carfel.com") {

                    //Caso usuário exista salva os dados(id,nome e email) na session
                    HttpContext.Session.SetString ("nomeUsuario", usuarioRetornado.Nome);
                    HttpContext.Session.SetString ("emailUsuario", usuarioRetornado.Email);

                    //Informa ao usuário que o login foi efetuado
                    TempData["Login"] = "Login realizado com sucesso!";

                    //Redireciona para a página de comentários
                    return RedirectToAction ("Comentar", "Comentarios");
                } else {
                    HttpContext.Session.SetString ("nomeUsuario", usuarioRetornado.Nome);
                    HttpContext.Session.SetString ("emailUsuario", usuarioRetornado.Email);

                    //Action, Controller
                    return RedirectToAction ("Listar", "Comentarios");
                }

            } else {
                //Caso não exista informa ao usuário
                 TempData["Erro"] = "Usuário e/ou senha incorretos";
            }

            //Retorna a view de usuário
            return View ();
        }

        public IActionResult Listar () {

            ViewData["Usuarios"] = UsuarioRepositorio.Listar ();

            return View ();
        }

        
        [HttpGet]

        public IActionResult Deslogar () {

            HttpContext.Session.Remove("nomeUsuario");
            HttpContext.Session.Remove("emailUsuario");

            return RedirectToAction ("Login");
        }

    }
}