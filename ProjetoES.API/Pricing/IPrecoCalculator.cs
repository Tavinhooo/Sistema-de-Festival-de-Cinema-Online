namespace ProjetoES.API.Pricing;

/// <summary>
/// Interface para o cálculo de preços, representando um contrato para classes que implementam diferentes 
/// estratégias de cálculo de preços para os bilhetes dos filmes.
///  A interface define um método CalcularPreco que recebe uma coleção de preços dos bilhetes e retorna o preço final calculado,
///  além de uma propriedade Descricao que fornece uma descrição da estratégia de cálculo utilizada.
///  As classes que implementam esta interface podem aplicar diferentes regras de desconto, `
/// promoções ou outras lógicas de precificação para determinar o preço final a ser cobrado aos clientes.
/// </summary>
public interface IPrecoCalculator
{
    decimal CalcularPreco(IEnumerable<decimal> precosBilhetes);
    string Descricao { get; }
}