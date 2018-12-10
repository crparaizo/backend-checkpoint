using System.Collections.Generic;
using Senai.Checkpoint.Mvc.Models;

namespace Senai.Checkpoint.Mvc.Interfaces {
    public interface IComentario {
        ComentariosModel Comentar (ComentariosModel comentarios);

        List<ComentariosModel> Listar ();

        void Aprovar (int id);

        void Rejeitar (int id);

        List<ComentariosModel> Aprovados ();

    }
}