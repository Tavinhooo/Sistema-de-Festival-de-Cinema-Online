namespace ProjetoES.API.DTOs;

/// <summary>
/// DTO para atualizar o método de pagamento de uma compra, incluindo o novo método de pagamento escolhido pelo utilizador.
/// </summary>
public class AtualizarMetodoPagamentoDTO
{
    public string MetodoPagamento { get; set; } = string.Empty;
}
