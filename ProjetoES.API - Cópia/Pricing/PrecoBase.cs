namespace ProjetoES.API.Pricing;

public class PrecoBase : IPrecoCalculator
{
    private readonly decimal _precoBilhete;
    public string Descricao => "Preço base";

    public PrecoBase(decimal precoBilhete)
    {
        _precoBilhete = precoBilhete;
    }

    public decimal CalcularPreco(IEnumerable<decimal> precosBilhetes)
        => _precoBilhete;
}