namespace ProjetoES.API.Models
{
    /// <summary>
    /// Enumeração para os tipos de utilizadores, representando as diferentes categorias de utilizadores no sistema, como "Membro", "Cliente" e "Administrador".
    /// </summary>
    public enum TipoUtilizador
    {
        Membro = 1,           // Novo utilizador, registado mas sem compras
        Cliente = 2,          // Já realizou compras
        Administrador = 3     // Administrador do sistema
    }
}