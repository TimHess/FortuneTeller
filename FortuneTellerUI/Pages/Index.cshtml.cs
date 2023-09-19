using FortuneTellerUI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace FortuneTellerUI.Pages;

public class IndexModel(IOptionsSnapshot<FortuneServiceOptions> fortuneServiceOptions, ILogger<IndexModel> logger) : PageModel
{
    public FortuneServiceOptions FortuneServiceOptions = fortuneServiceOptions.Value;

    public void OnGet()
    {
        logger.LogCritical("A critical message");
        logger.LogError("An error message");
        logger.LogWarning("A warning message");
        logger.LogInformation("An informational message");
        logger.LogDebug("A debug message");
        logger.LogTrace("A trace message");
    }
}
