namespace ProjetoES.API.Pricing;

// Passe Completo: soma todos os filmes com 30% de desconto
public class PasseCompletoDecorator : PrecoDecorator
{
    public override string Descricao => "Passe Completo (30% desconto sobre soma dos filmes)";

    public PasseCompletoDecorator(IPrecoCalculator inner) : base(inner) { }

    public override decimal CalcularPreco(IEnumerable<decimal> precosBilhetes)
    {
        var lista = precosBilhetes.ToList();
        return lista.Sum() * 0.70m;
    }
}