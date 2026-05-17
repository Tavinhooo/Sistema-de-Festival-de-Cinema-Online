using System.Globalization;
using System.Text;
using ProjetoES.API.Factories;
using ProjetoES.API.Models;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Services;

public class AcessoService
{
    private readonly AcessoRepository _acessoRepository;

    public AcessoService(AcessoRepository acessoRepository)
    {
        _acessoRepository = acessoRepository;
    }

    public void CriarAcessos(int clienteId, int filmeId, int quantidade, string tipoAcesso)
    {
        if (quantidade <= 0)
        {
            throw new ArgumentException("A quantidade tem de ser superior a zero.");
        }

        var factory = ObterFactory(tipoAcesso);
        var dataAquisicao = DateTime.UtcNow;

        var acessos = Enumerable.Range(0, quantidade)
            .Select(_ => factory.CriarAcesso(clienteId, filmeId, dataAquisicao))
            .ToList();

        _acessoRepository.CriarAcessos(acessos);
    }

    private static AcessoFactory ObterFactory(string tipoAcesso)
    {
        var normalizado = RemoverAcentos(tipoAcesso.Trim().ToLowerInvariant());

        return normalizado switch
        {
            "bilhete de sessao" => new BilheteAcessoFactory(),
            "passe diario" => new PasseDiarioAcessoFactory(),
            "passe completo" => new PasseCompletoAcessoFactory(),
            "aluguer digital" => new AluguerDigitalFactory(),
            _ => throw new ArgumentOutOfRangeException(nameof(tipoAcesso), tipoAcesso, "Tipo de acesso inválido.")
        };
    }

    private static string RemoverAcentos(string texto)
    {
        var textoNormalizado = texto.Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder(texto.Length);

        foreach (var caractere in textoNormalizado)
        {
            var categoria = CharUnicodeInfo.GetUnicodeCategory(caractere);
            if (categoria != UnicodeCategory.NonSpacingMark)
            {
                builder.Append(caractere);
            }
        }

        return builder.ToString().Normalize(NormalizationForm.FormC);
    }
}