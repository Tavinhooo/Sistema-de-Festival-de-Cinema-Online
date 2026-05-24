using System.Globalization;
using System.Text;
using ProjetoES.API.Factories;
using ProjetoES.API.Models;
using ProjetoES.API.Repositories;

namespace ProjetoES.API.Services;

public class AcessoService
{
    private readonly AcessoRepository _acessoRepository;
    private readonly FilmeRepository _filmeRepository;

    public AcessoService(AcessoRepository acessoRepository, FilmeRepository filmeRepository)
    {
        _acessoRepository = acessoRepository;
        _filmeRepository = filmeRepository;
    }

    public void CriarAcessos(int clienteId, int? filmeId, int festivalId, int quantidade, string tipoAcesso)
    {
        if (quantidade <= 0)
        {
            throw new ArgumentException("A quantidade tem de ser superior a zero.");
        }

        if (EPasseFestival(tipoAcesso))
        {
            var filmesFestival = _filmeRepository.ObterFilmesPorFestival(festivalId);
            if (filmesFestival.Count == 0)
            {
                throw new ArgumentException("Festival sem filmes.");
            }

            var factory = new PasseCompletoAcessoFactory();
            var dataAquisicao = DateTime.UtcNow;
            var acessos = new List<Acesso>();

            for (var i = 0; i < quantidade; i++)
            {
                foreach (var filme in filmesFestival)
                {
                    var acesso = factory.CriarAcesso(clienteId, filme.Id, dataAquisicao);
                    acesso.FestivalId = festivalId;
                    acessos.Add(acesso);
                }
            }

            _acessoRepository.CriarAcessos(acessos);
            return;
        }

        if (!filmeId.HasValue)
        {
            throw new ArgumentException("O filme é obrigatório.");
        }

        var normalFactory = ObterFactory(tipoAcesso);
        var normalDataAquisicao = DateTime.UtcNow;

        var normalAcessos = Enumerable.Range(0, quantidade)
            .Select(_ => normalFactory.CriarAcesso(clienteId, filmeId.Value, normalDataAquisicao))
            .ToList();

        foreach (var acesso in normalAcessos) acesso.FestivalId = festivalId;

        _acessoRepository.CriarAcessos(normalAcessos);
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

    private static bool EPasseFestival(string tipoAcesso)
        => !string.IsNullOrWhiteSpace(tipoAcesso)
           && (
               RemoverAcentos(tipoAcesso.Trim().ToLowerInvariant()) == "passe completo" ||
               RemoverAcentos(tipoAcesso.Trim().ToLowerInvariant()) == "passe diario"
           );
}