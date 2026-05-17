namespace ProjetoES.API.Models
{
	public enum TipoSessao
	{
		Estreia,
		HorarioFixo,
		JanelaAcesso
	}

	public class Sessao
	{
		public int Id { get; set; }

		public int FestivalId { get; set; }
		public Festival? Festival { get; set; }

		public int FilmeId { get; set; }
		public Filme? Filme { get; set; }

		public DateTime DataInicio { get; set; }
		public DateTime DataFim { get; set; }

		public string Sala { get; set; } = string.Empty;

		public TipoSessao Tipo { get; set; } = TipoSessao.HorarioFixo;
	}
}
