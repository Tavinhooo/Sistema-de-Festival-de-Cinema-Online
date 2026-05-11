namespace ProjetoES.API.Models
{
    public class Visitante //deixou de ser abstrato para poder existir como visitante anónimo no sistema.
    {
        public int Id { get; set; }
        // Marca se a visita atual está autenticada.
        public bool IsLogged { get; set; }
    }
}