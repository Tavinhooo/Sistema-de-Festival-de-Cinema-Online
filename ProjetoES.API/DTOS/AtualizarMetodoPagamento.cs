namespace ProjetoES.API.DTOS;

public class AtualizarMetodoPagamentoDTO
{
    // Ex: "MBWay", "Cartao", "Multibanco"
    public string MetodoPagamento { get; set; } = string.Empty;
}