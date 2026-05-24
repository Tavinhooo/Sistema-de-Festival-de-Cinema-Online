

namespace ProjetoES.API.Models
{
    /// <summary>
    /// Modelo de morada, representando as informações de endereço associadas a um utilizador, incluindo o nome do destinatário,
    ///  morada de faturação, código postal, localidade e país. A morada é utilizada para fins de faturação e envio de correspondências
    ///  relacionadas aos pedidos e acessos dos clientes no sistema.
    /// </summary>
    public class Morada
    {
        public string NomeDestinatario { get; set; } = string.Empty;
        public string MoradaFaturacao { get; set; } = string.Empty;
        public string CodigoPostal { get; set; } = string.Empty;
        public string Localidade { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
    }
}