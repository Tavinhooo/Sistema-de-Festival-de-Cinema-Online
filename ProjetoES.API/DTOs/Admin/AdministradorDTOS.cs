using ProjetoES.API.Models;

namespace ProjetoES.API.DTOs
{
    public class FilmeDTO
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Sinopse { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;

        public int Ano { get; set; }

        public int DuracaoMinutos { get; set; }

        public decimal PrecoBilhete { get; set; }
        public double MediaAvaliacao { get; set; }

        // Para já vamos usar um Link da internet para a imagem para ser mais fácil
        public string PosterUrl { get; set; } = string.Empty;
        public int FestivalId { get; set; }
    }

    public class FestivalDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateOnly DataInicio { get; set; }
        public DateOnly DataFim { get; set; }
        public List<int> FilmesIds { get; set; } = new();
    }

    public class SessaoDTO
    {
        public int Id { get; set; }
        public int FilmeId { get; set; }
        public DateTime DataHora { get; set; }
        public int Sala { get; set; }
        public int CapacidadeTotal { get; set; }
        public decimal Preco { get; set; }
        public int? FestivalId { get; set; }
    }

    public class UtilizadorDTO
    {
        public int Id { get; set; }
        public string PrimeiroNome { get; set; } = string.Empty;
        public string UltimoNome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public TipoUtilizador Tipo { get; set; }
        public bool IsLogged { get; set; }
        public DateTime? DataPrimeiraCompra { get; set; }

    }

public class AvaliacaoDTO
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public int FilmeId { get; set; }
    public int Classificacao { get; set; }
    public string Comentario { get; set; } = string.Empty;
    public DateTime DataAvaliacao { get; set; }
    public bool IsReportado { get; set; }
    public string? MotivoReporte { get; set; } // ADICIONA ISTO
}
    public class PedidoDTO
    {
        public int Id { get; set; }
        public int UtilizadorId { get; set; }
        public string UtilizadorEmail { get; set; } = string.Empty;
        public DateTime DataPedido { get; set; }
        public int? SessaoId { get; set; }
        public int Quantidade { get; set; }
        public string Estado { get; set; } = string.Empty;
        public double PrecoTotal { get; set; }
    }
    public class AlterarTipoDTO
    {
        public TipoUtilizador NovoTipo { get; set; }
    }
}