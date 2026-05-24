namespace ProjetoES.API.DTOs;

public class AtualizarMetodoPagamentoDTO
{
    // Ex: "MBWay", "Cartao", "Multibanco"
    public string MetodoPagamento { get; set; } = string.Empty;
}
