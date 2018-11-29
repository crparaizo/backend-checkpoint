using System;

namespace Senai.Checkpoint.Mvc.Models {
    public class ComentariosModel {

        public int ID { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Comentario { get; set; }

        public DateTime DataCriacao { get; set; }

    }
}