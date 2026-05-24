namespace ProjetoES.API.Pricing;

/// <summary>
/// Decorator base para o cálculo de preço, permitindo a adição de funcionalidades de cálculo de preço de forma flexível e modular,
///  sem modificar as classes existentes. O PrecoDecorator é uma classe abstrata que implementa a interface IPrecoCalculator.
/// </summary>
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