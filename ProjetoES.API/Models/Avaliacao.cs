using Microsoft.AspNetCore.Mvc;

namespace ProjetoES.API.Models
{
    public class Avaliacao
    {
        public int Id {get; set;}
        public int ClienteID {get; set;}
        public int FilmeID {get; set;}
        public int Nota {get; set;}
        public string Comentario {get; set;} = string.Empty;
        public DateTime DataAvaliacao {get; set;}
        public bool IsReportado {get; set;}
        //public Cliente? Cliente {get; set;}
        public Filme? Filme {get; set;}
    }
}