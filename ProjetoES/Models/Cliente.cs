namespace ProjetoES.Models
{
    // O Cliente herda de Utilizador
    public class Cliente : Utilizador
    {
        public string MoradaFaturacao { get; set; } = string.Empty;
        public string CodigoPostal { get; set; } = string.Empty;
        public string Localidade { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;

        // Relação: 1 Cliente tem Múltiplas Compras/Faturas
        // (A classe Compra ainda vai ser criada no próximo passo)
        // public virtual ICollection<Compra> HistoricoCompras { get; set; } = new List<Compra>();
    }
}