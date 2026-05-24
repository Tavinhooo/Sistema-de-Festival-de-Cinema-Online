namespace ProjetoES.API.Models
{
    public enum EstadoAcesso { Ativo, Suspenso, Revogado }
    /// <summary>
    /// Modelo de acesso, representando a aquisição de um filme por um cliente, incluindo informações sobre o cliente, filme,
    ///  data de aquisição, validade e estado do acesso.
    /// </summary>
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