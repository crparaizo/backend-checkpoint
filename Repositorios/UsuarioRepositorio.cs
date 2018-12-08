using System.Collections.Generic;
using System.IO;
using Senai.Checkpoint.Mvc.Interfaces;
using Senai.Checkpoint.Mvc.Models;

namespace Senai.Checkpoint.Mvc.Repositorios {
    public class UsuarioRepositorio : IUsuario {

        public UsuarioModel Cadastrar (UsuarioModel usuario) {

            if (File.Exists ("usuarios.csv")) {
                usuario.ID = System.IO.File.ReadAllLines ("usuarios.csv").Length + 1;
            } else {
                usuario.ID = 2;

                using (StreamWriter adm = new StreamWriter ("usuarios.csv", true)) {
                    adm.WriteLine ($"{"1"};{"Administrador"};{"admin@carfel.com"};{"admin"}");
                }

            }

            using (StreamWriter sw = new StreamWriter ("usuarios.csv", true)) {
                sw.WriteLine ($"{usuario.ID};{usuario.Nome};{usuario.Email};{usuario.Senha}");
            }

            return usuario;

        }

        public List<UsuarioModel> Listar () => ListaCSV ();

        private List<UsuarioModel> ListaCSV () {

            List<UsuarioModel> lsUsuarios = new List<UsuarioModel> ();

            string[] linhas = File.ReadAllLines ("usuarios.csv");

            foreach (string linha in linhas) {
                if (string.IsNullOrEmpty (linha)) {
                    continue;
                }

                string[] Dados = linha.Split (";");

                UsuarioModel usuario = new UsuarioModel (

                    id: int.Parse (Dados[0]),
                    nome: Dados[1],
                    email: Dados[2],
                    senha: Dados[3]

                );

                lsUsuarios.Add (usuario);
            }

            return lsUsuarios;

        }

        public UsuarioModel BuscarId (int Id) {
            string[] linhas = System.IO.File.ReadAllLines ("usuarios.csv");

            for (int i = 0; i < linhas.Length; i++) {
                if (string.IsNullOrEmpty (linhas[i])) {
                    continue;
                }

                string[] dados = linhas[i].Split (";");

                if (dados[0] == Id.ToString ()) {
                    UsuarioModel usuario = new UsuarioModel (
                        id: int.Parse (dados[0]),
                        nome: dados[1],
                        email: dados[2],
                        senha: dados[3]
                    );

                    return usuario;
                }
            }

            return null;
        }

        public UsuarioModel BuscarEmailSenha (string email, string senha) {
            List<UsuarioModel> Cadastrados = ListaCSV ();

            foreach (UsuarioModel usuario in Cadastrados) {
                if (usuario.Email == email && usuario.Senha == senha) {
                    return usuario;
                }
            }

            return null;
        }

    }
}