namespace ProjetoES.API.Pricing;

// Passe Diário: soma todos os filmes do festival com 50% de desconto
public class PasseDiarioDecorator : PrecoDecorator
{
    public override string Descricao => "Passe Diário (50% desconto sobre soma dos filmes)";

    public PasseDiarioDecorator(IPrecoCalculator inner) : base(inner) { }

    public override decimal CalcularPreco(IEnumerable<decimal> precosBilhetes)
    {
        var lista = precosBilhetes.ToList();
        return lista.Sum() * 0.50m;
    }
}