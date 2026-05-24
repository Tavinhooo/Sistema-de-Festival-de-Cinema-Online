namespace ProjetoES.API.Models
{
    public enum TipoUtilizador
    {
        Membro = 1,           // Novo utilizador, registado mas sem compras
        Cliente = 2,          // Já realizou compras
        Administrador = 3     // Administrador do sistema
    }
}