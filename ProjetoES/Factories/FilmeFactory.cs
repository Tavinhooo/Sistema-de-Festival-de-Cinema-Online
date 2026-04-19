using ProjetoES.Models;

namespace ProjetoES.Factories
{
    public static class FilmeFactory
    {
        // O Factory Method centraliza a criação e define valores por defeito lógicos
        public static Filme CriarFilme(string titulo, string sinopse, string genero, int ano, int duracao, decimal preco, string posterUrl)
        {
            return new Filme
            {
                Titulo = titulo,
                Sinopse = sinopse,
                Genero = genero,
                Ano = ano,
                DuracaoMinutos = duracao,
                PrecoBilhete = preco,
                // Se não enviarem foto, metemos uma padrão automaticamente!
                PosterUrl = string.IsNullOrWhiteSpace(posterUrl) ? "/images/default-poster.jpg" : posterUrl
            };
        }
    }
}