using System.Collections.Generic;
using Senai.Checkpoint.Mvc.Models;

namespace Senai.Checkpoint.Mvc.Interfaces {
    public interface IComentario {
        ComentariosModel Comentar (ComentariosModel comentarios);

        List<ComentariosModel> Listar ();

        // ComentariosModel BuscarId (int Id);

        // ComentariosModel BuscarEmail(string email, string senha);
    }
}