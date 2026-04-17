namespace ProjetoES.Models
{
    public class Administrador : Utilizador
    {
        // Podes adicionar coisas específicas do Admin aqui no futuro, 
        // como "NivelPermissao" ou "Departamento".
        public string Departamento { get; set; } = "Geral";
    }
}