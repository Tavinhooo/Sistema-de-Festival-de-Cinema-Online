using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjetoES.Pages
{
    // bloqueia a entrada na página se não tiver login feito
    [Authorize] 
    public class PerfilModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}