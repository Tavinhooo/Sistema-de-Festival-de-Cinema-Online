namespace ProjetoES.API.Pricing;

/// <summary>
/// Decorator para o cálculo de preço do passe completo, aplicando um desconto de 30% sobre a soma dos preços dos bilhetes dos filmes.
///  O PasseCompletoDecorator é uma implementação do padrão Decorator, 
/// que permite adicionar funcionalidades de cálculo de preço de forma flexível e modular, sem modificar as classes existentes.
///  Ao utilizar o PasseCompletoDecorator, o sistema pode calcular o preço final do passe completo aplicando o desconto apropriado,
///  proporcionando uma experiência de compra mais atrativa para os clientes que desejam adquirir um passe completo para o festival.
/// </summary>
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