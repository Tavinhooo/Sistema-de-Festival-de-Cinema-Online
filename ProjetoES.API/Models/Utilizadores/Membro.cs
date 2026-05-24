namespace ProjetoES.API.Models
{
    /// <summary>
    /// Modelo de membro, representando um utilizador do tipo "Membro" no sistema, que é um cliente regular do festival,
    ///  com acesso aos filmes e funcionalidades do sistema. O modelo de membro é uma subclasse de Utilizador,
    ///  e a distinção entre os tipos de utilizadores é feita através do campo TipoUtilizador na classe base.
    /// </summary>
    [System.Obsolete("Use Utilizador com TipoUtilizador.Membro", false)]
    public class Membro : Utilizador
    {
    }
}