using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjetoES.Pages
{
    [Authorize] 
    public class CarrinhoModel : PageModel
    {
        public void OnGet() { }
    }
}