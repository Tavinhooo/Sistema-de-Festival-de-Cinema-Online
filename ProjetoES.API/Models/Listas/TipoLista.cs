namespace ProjetoES.API.Models
{
    /// <summary>
    /// Enumeração para os tipos de listas pessoais, representando as diferentes categorias de listas que um utilizador
    ///  pode criar para organizar seus filmes, como "Ver Depois", "Visto" e "Favoritos".
    ///  Essa enumeração é utilizada para identificar o tipo de lista ao criar ou gerenciar as listas pessoais dos utilizadores.
    /// </summary>
    public enum TipoLista
    {
        VerDepois,
        Visto,
        Favoritos
    }
}