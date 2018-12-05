using System.Collections.Generic;
using Senai.Checkpoint.Mvc.Models;

namespace Senai.Checkpoint.Mvc.Interfaces {
    public interface IComentario {
        ComentariosModel Comentar (ComentariosModel comentarios);

        List<ComentariosModel> Listar ();

        ComentariosModel Administrar (ComentariosModel comentarios);

        List<ComentariosModel> Mostrar ();

        ComentariosModel Aceitar ();

        ComentariosModel Rejeitar ();

        // ComentariosModel BuscarId (int Id);

        // ComentariosModel BuscarEmail(string email, string senha);
    }
}