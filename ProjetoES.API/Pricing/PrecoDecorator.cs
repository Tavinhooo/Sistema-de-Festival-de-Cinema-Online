namespace ProjetoES.API.Pricing;

public abstract class PrecoDecorator : IPrecoCalculator
{
    protected readonly IPrecoCalculator _inner;
    public abstract string Descricao { get; }

    protected PrecoDecorator(IPrecoCalculator inner)
    {
        _inner = inner;
    }

    public abstract decimal CalcularPreco(IEnumerable<decimal> precosBilhetes);
}