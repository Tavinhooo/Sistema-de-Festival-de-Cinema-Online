using System.ComponentModel.DataAnnotations;

namespace ProjetoES.API.Models
{
    public enum EstadoAcesso { Ativo, Suspenso, Revogado }

    public class Acesso
    {
        public int Id { get; set; }
        
        public int ClienteId { get; set; }
        public int FilmeId { get; set; }
        public virtual Filme? Filme { get; set; }
        
        public DateTime DataAquisicao { get; set; }
        public DateTime? DataValidade { get; set; } // Útil para os Alugueres de 48h
        
        public EstadoAcesso Estado { get; set; } = EstadoAcesso.Ativo;
        public string TipoAcesso { get; set; } = string.Empty; // Bilhete, Passe, Aluguer
    }
}