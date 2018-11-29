using System;

namespace Senai.Checkpoint.Mvc.Models {
    public class UsuarioModel {

        public int ID { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public UsuarioModel () {

        }

        public UsuarioModel (string nome, string email, string senha) {

            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;

        }

        public UsuarioModel (string email, string senha) {

            this.Email = email;
            this.Senha = senha;

        }

        public UsuarioModel (int id, string nome, string email, string senha) {

            this.ID = id;
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
        }
    }

}