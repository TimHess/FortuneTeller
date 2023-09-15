using FortuneTeller;
using Microsoft.Extensions.Options;

namespace FortuneTellerUI.Services;

public class FortuneServiceClient(
    HttpClient httpClient,
    IOptionsSnapshot<FortuneServiceOptions> config) : IFortuneService
{
    private FortuneServiceOptions Config => config.Value;

    public async Task<List<Fortune>> AllFortunesAsync()
    {
        var response = await httpClient.GetAsync(Config.AllFortunesURL);

        return await response.Content.ReadFromJsonAsync<List<Fortune>>() ?? new List<Fortune> { new(0, "failed to get all fortunes")};
    }

    public async Task<Fortune> RandomFortuneAsync()
    {
        var response = await httpClient.GetAsync(Config.RandomFortuneURL);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Fortune>() ?? new(0, "failed to get random fortune");
        }
        else
        {
            return new(0, await response.Content.ReadAsStringAsync());
        }
    }
}