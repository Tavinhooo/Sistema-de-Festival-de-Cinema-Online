

namespace ProjetoES.API.Models
{
    public class Morada
    {
        public int Id { get; set; }
        public string Rua { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string CodigoPostal { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
    }
}