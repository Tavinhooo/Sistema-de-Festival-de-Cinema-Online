namespace ProjetoES.API.Models
{
    public abstract class Visitante
    {
        public int Id { get; set; }
        // Marca se a visita atual está autenticada.
        public bool IsLogged { get; set; }
    }
}