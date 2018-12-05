using System;
using System.Collections.Generic;
using System.IO;
using Senai.Checkpoint.Mvc.Interfaces;
using Senai.Checkpoint.Mvc.Models;

namespace Senai.Checkpoint.Mvc.Repositorios {
    public class ComentariosRepositorio : IComentario {

        public ComentariosModel Comentar (ComentariosModel comentarios) {
            if (File.Exists ("comentarios.csv")) {
                comentarios.ID = System.IO.File.ReadAllLines ("comentarios.csv").Length + 1;
            } else {
                comentarios.ID = 1;
            }

            using (StreamWriter sw = new StreamWriter ("comentarios.csv", true)) {
                sw.WriteLine ($"{comentarios.ID};{comentarios.Nome};{comentarios.Email};{comentarios.Comentario};{DateTime.Now}");
            }

            return comentarios;
        }

        public ComentariosModel Administrar (ComentariosModel comentarios) {
            if (File.Exists ("adm.csv")) {
                comentarios.ID = System.IO.File.ReadAllLines ("adm.csv").Length + 1;
            } else {
                comentarios.ID = 1;
            }

            using (StreamWriter sw = new StreamWriter ("adm.csv", true)) {
                sw.WriteLine ($"{comentarios.ID};{comentarios.Nome};{comentarios.Email};{comentarios.Comentario};{DateTime.Now}");
            }

            return comentarios;
        }

        public List<ComentariosModel> Listar () => ListaCSVcm ();

        public List<ComentariosModel> Mostrar () => ListaCSVcm ();

        private List<ComentariosModel> ListaCSVcm () {

            List<ComentariosModel> lsComentarios = new List<ComentariosModel> ();

            string[] linhas = File.ReadAllLines ("comentarios.csv");

            foreach (string linha in linhas) {
                if (string.IsNullOrEmpty (linha)) {
                    continue;
                }

                string[] Dados = linha.Split (";");

                ComentariosModel comentarios = new ComentariosModel (

                    id: int.Parse (Dados[0]),
                    nome: Dados[1],
                    email: Dados[2],
                    comentario: Dados[3],
                    data: DateTime.Parse (Dados[4])

                );

                lsComentarios.Add (comentarios);
            }

            return lsComentarios;
        }

        //Reverter coméntarios:

        public string Reverse (string text) {

            char[] contrario = text.ToCharArray ();

            string reverse = String.Empty;
            for (int i = contrario.Length - 1; i > -1; i--) {
                reverse += contrario[i];
            }
            return reverse;

        }

        public ComentariosModel Aceitar()
        {
            throw new NotImplementedException();
        }

        public ComentariosModel Rejeitar()
        {
            throw new NotImplementedException();
        }

        //Fim reverter comentários

        // public ComentariosModel BuscarId (int Id) {
        //     string[] linhas = System.IO.File.ReadAllLines ("comentarios.csv");

        //     for (int i = 0; i < linhas.Length; i++) {
        //         if (string.IsNullOrEmpty (linhas[i])) {
        //             continue;
        //         }

        //         string[] dados = linhas[i].Split (";");

        //         if (dados[0] == Id.ToString ()) {
        //             ComentariosModel comentarios = new ComentariosModel (
        //                 id: int.Parse (dados[0]),
        //                 nome: dados[1],
        //                 email: dados[2],
        //                 comentario: dados[3]
        //             );

        //             return comentarios;
        //         }
        //     }

        //     return null;
        // }

        // public ComentariosModel BuscarEmail (string email, string senha) {
        //     List<ComentariosModel> Cadastrados = ListaCSVcm ();

        //     foreach (ComentariosModel comentarios in Cadastrados) {
        //         if (comentarios.Email == email) {
        //             return comentarios;
        //         }
        //     }

        //     return null;
        // }

    }
}