using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetoES.Interfaces;
using ProjetoES.Models;

namespace ProjetoES.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IFestivalService _festivalService;

        public IndexModel(IFestivalService festivalService)
        {
            _festivalService = festivalService;
        }

        // Esta lista vai guardar os festivais que vêm da Base de Dados
        public List<Festival> ListaFestivais { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Vamos buscar todos os festivais guardados
            ListaFestivais = await _festivalService.ObterTodosFestivaisAsync();
        }
    }
}