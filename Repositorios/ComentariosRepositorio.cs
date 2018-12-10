using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                sw.WriteLine ($"{comentarios.ID};{comentarios.Nome};{comentarios.Email};{comentarios.Comentario};{DateTime.Now};{false}");
            }

            return comentarios;
        }

        public List<ComentariosModel> Listar () => ListaCSVcm ();

        private List<ComentariosModel> ListaCSVcm () {

            List<ComentariosModel> lsComentarios = new List<ComentariosModel> ();

            if (!File.Exists ("comentarios.csv")) {
                return lsComentarios;
            }

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
                    data: DateTime.Parse (Dados[4]),
                    status: bool.Parse (Dados[5])

                );

                lsComentarios.Add (comentarios);

            }

            return lsComentarios;
        }

        public List<ComentariosModel> Aprovados () {

            List<ComentariosModel> ComentariosAprovados = new List<ComentariosModel> ();

            if (!File.Exists ("comentarios.csv")) {
                return ComentariosAprovados;
            }

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
                    data: DateTime.Parse (Dados[4]),
                    status: bool.Parse (Dados[5])
                );

                if (comentarios.Status == true) {

                    ComentariosAprovados.Add (comentarios);

                }

            }

            return ComentariosAprovados.OrderBy (x => x.DataCriacao).Reverse ().ToList ();

        }

        // Para sair do login: dar remove no id, nome e email da sessão

        public void Aprovar (int id) {
            //Abre o stream de leitura do arquivo
            string[] linhas = File.ReadAllLines ("comentarios.csv");

            //Lê cada registro no CSV
            for (int i = 0; i < linhas.Length; i++) {
                //Separa os dados da linha
                string[] dadosDaLinha = linhas[i].Split (';');

                if (id.ToString () == dadosDaLinha[0]) {
                    linhas[i] = ($"{dadosDaLinha[0]};{dadosDaLinha[1]};{dadosDaLinha[2]};{dadosDaLinha[3]};{dadosDaLinha[4]};{true}");
                    break;
                }

            }

            File.WriteAllLines ("comentarios.csv", linhas);
        }

        public void Rejeitar (int id) {
            //Abre o stream de leitura do arquivo
            string[] linhas = File.ReadAllLines ("comentarios.csv");

            //Lê cada registro no CSV
            for (int i = 0; i < linhas.Length; i++) {
                //Separa os dados da linha  
                string[] dadosDaLinha = linhas[i].Split (';');

                if (id.ToString () == dadosDaLinha[0]) {
                    linhas[i] = ($"{dadosDaLinha[0]};{dadosDaLinha[1]};{dadosDaLinha[2]};{dadosDaLinha[3]};{dadosDaLinha[4]};{false}");
                    break;
                }

            }

            File.WriteAllLines ("comentarios.csv", linhas);
        }

    }
}