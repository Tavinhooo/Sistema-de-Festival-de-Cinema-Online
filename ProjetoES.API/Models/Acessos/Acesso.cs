namespace ProjetoES.API.Models
{
    public enum EstadoAcesso { Ativo, Suspenso, Revogado }
    
    public class Acesso
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public virtual Utilizador? Cliente { get; set; }

        public int FilmeId { get; set; }
        public int FestivalId { get; set; }
        public virtual Filme Filme { get; set; } = null!;

        public DateTime DataAquisicao { get; set; } = DateTime.UtcNow;
        public DateTime? DataValidade { get; set; }

        public EstadoAcesso Estado { get; set; } = EstadoAcesso.Ativo;
        // "BilheteSessao", "PasseDiario", "PasseCompleto", "AluguerDigital"
        public string TipoAcesso { get; set; } = string.Empty;
    }
}