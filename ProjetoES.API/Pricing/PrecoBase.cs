namespace ProjetoES.API.Pricing;

/// <summary>
/// Classe de preço base, representando a estratégia de cálculo de preço sem descontos ou promoções,
///  onde o preço final é simplesmente a soma dos preços dos bilhetes dos filmes.
///  O PrecoBase é a implementação mais simples da interface IPrecoCalculator, 
/// e serve como ponto de partida para outras estratégias de cálculo de preço que podem aplicar descontos ou promoções adicionais.
///  Ao utilizar o PrecoBase, o sistema calcula o preço final dos bilhetes dos filmes sem aplicar nenhum desconto, 
/// proporcionando uma base para comparação com outras estratégias de precificação mais complexas.
/// </summary>
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