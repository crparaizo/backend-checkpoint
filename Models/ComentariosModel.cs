using System;

namespace Senai.Checkpoint.Mvc.Models {
    public class ComentariosModel {

        public int ID { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Comentario { get; set; }

        public DateTime DataCriacao { get; set; }

        public bool Status { get; set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        public ComentariosModel () {

        }

        public ComentariosModel (int id, string nome, string email, string comentario, DateTime data, bool status) {

            this.ID = id;
            this.Nome = nome;
            this.Email = email;
            this.Comentario = comentario;
            this.DataCriacao = data;
            this.Status = status;

        }

        public ComentariosModel (string nome, string email, string comentario) {

            this.Nome = nome;
            this.Email = email;
            this.Comentario = comentario;

        }

    }
}