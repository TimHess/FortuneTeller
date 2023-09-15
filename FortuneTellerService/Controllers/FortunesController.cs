using FortuneTeller;
using FortuneTellerService.Models;
using Microsoft.AspNetCore.Mvc;

namespace FortuneTellerService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FortunesController(ILogger<FortunesController> logger, IFortuneRepository fortunes) : ControllerBase
{

    // GET: api/fortunes/all
    [HttpGet("all")]
    public async Task<IEnumerable<Fortune>> AllFortunesAsync()
    {
        logger?.LogTrace("AllFortunesAsync");
        var entities = await fortunes.GetAllAsync();
        return entities.Select(fortune => new Fortune(fortune.Id, fortune.Text));
    }

    // GET api/fortunes/random
    [HttpGet("random")]
    public async Task<Fortune> RandomFortuneAsync()
    {
        logger?.LogTrace("RandomFortuneAsync");
        var fortuneEntity = await fortunes.RandomFortuneAsync();
        return new Fortune(fortuneEntity.Id, fortuneEntity.Text);
    }
}
