namespace ProjetoES.API.Pricing;

public interface IPrecoCalculator
{
    decimal CalcularPreco(IEnumerable<decimal> precosBilhetes);
    string Descricao { get; }
}