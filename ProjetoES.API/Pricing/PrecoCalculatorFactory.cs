namespace ProjetoES.API.Pricing;

public static class PrecoCalculatorFactory
{
    // Para BilheteSessao e AluguerDigital o preco é o do filme escolhido (PrecoBase)
    // Para PasseDiario e PasseCompleto encadeamos o decorator sobre PrecoBase(0)
    public static IPrecoCalculator Criar(string tipoAcesso, decimal precoBilheteFilme = 0)
    {
        IPrecoCalculator base_ = new PrecoBase(precoBilheteFilme);

        return tipoAcesso.Trim().ToLowerInvariant() switch
        {
            "bilhete de sessao" or "bilhete de sessão" => base_,
            "aluguer digital"                          => base_,
            "passe diario"    or "passe diário"        => new PasseDiarioDecorator(base_),
            "passe completo"                           => new PasseCompletoDecorator(base_),
            _ => throw new ArgumentException($"Tipo de acesso inválido: {tipoAcesso}")
        };
    }
}