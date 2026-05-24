namespace ProjetoES.API.Models
{
    /// <summary>
    /// Modelo de log de alteração de acesso, representando as mudanças no estado de um acesso, 
    /// incluindo informações sobre o utilizador que fez a alteração, a data da alteração, o acesso afetado, o administrador responsável
    ///  pela alteração (se aplicável), os estados anterior e novo do acesso, e o motivo da alteração (se fornecido).
    /// </summary>
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