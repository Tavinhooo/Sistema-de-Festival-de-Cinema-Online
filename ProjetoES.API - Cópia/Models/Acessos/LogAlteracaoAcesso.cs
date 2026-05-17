namespace ProjetoES.API.Models
{
    public class LogAlteracaoAcesso
    {
        public int Id { get; set; }
        public int UtilizadorId { get; set; }
        public DateTime DataAlteracao { get; set; }
        public Acesso? Acesso { get; set; }
        public Administrador? Administrador { get; set; }
        public EstadoAcesso EstadoAnterior { get; set; }
        public EstadoAcesso EstadoNovo { get; set; }
        public string? Motivo { get; set; }

        // Relação com Utilizador
        public virtual Utilizador Utilizador { get; set; } = null!;
    }
}