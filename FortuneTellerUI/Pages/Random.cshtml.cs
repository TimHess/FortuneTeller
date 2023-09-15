using FortuneTellerUI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FortuneTellerUI.Pages;

public class RandomModel(IFortuneService fortuneService) : PageModel
{
    public string Message { get; private set; } = "Hello from FortuneTellerUI!";

    public async Task OnGet()
    {
        var fortune = await fortuneService.RandomFortuneAsync();
        Message = fortune.Text;
    }
}