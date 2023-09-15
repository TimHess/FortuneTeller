using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FortuneTellerUI.Pages;

public class IndexModel(IConfiguration config, ILogger<IndexModel> logger) : PageModel
{
    private readonly IConfiguration _config = config;
    private readonly ILogger<IndexModel> _logger = logger;

    public void OnGet()
    {
        _logger?.LogTrace("Processing request for site index");
    }
}
