namespace ProjetoES.API.Pricing;

// Passe Diário: soma todos os filmes do festival com 50% de desconto
/// <summary>
/// Decorator para o cálculo de preço do passe diário, aplicando um desconto de 50% sobre a soma dos preços dos bilhetes dos filmes.
///  O PasseDiarioDecorator é uma implementação do padrão Decorator, que permite adicionar funcionalidades de cálculo de preço de forma
///  flexível e modular, sem modificar as classes existentes. Ao utilizar o PasseDiarioDecorator,
///  o sistema pode calcular o preço final do passe diário aplicando o desconto apropriado,
///  proporcionando uma experiência de compra mais atrativa para os clientes que desejam adquirir um passe diário para o festival.
/// </summary>
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