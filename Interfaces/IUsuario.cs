using System.Collections.Generic;
using Senai.Checkpoint.Mvc.Models;

namespace Senai.Checkpoint.Mvc.Interfaces {
    public interface IUsuario {

        UsuarioModel Cadastrar (UsuarioModel usuario);

        List<UsuarioModel> Listar ();

        UsuarioModel BuscarId (int Id);

        UsuarioModel BuscarEmailSenha (string email, string senha);

        UsuarioModel CompararSenha (string senha, string confirma);

        UsuarioModel Aceitar ();

        UsuarioModel Rejeitar ();

    }
}