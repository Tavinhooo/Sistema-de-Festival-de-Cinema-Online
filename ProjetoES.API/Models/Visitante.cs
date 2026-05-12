namespace ProjetoES.API.Models
{
    // Visitante anónimo - herda de UtilizadorBase para usar TPH
    // Distinguido de Utilizador apenas pelo campo Discriminator='Visitante'
    public class Visitante : UtilizadorBase
    {
    }
}