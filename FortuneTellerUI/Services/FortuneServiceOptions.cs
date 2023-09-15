namespace FortuneTellerUI.Services;

public class FortuneServiceOptions
{
    public string Scheme { get; set; } = "http";

    public string? BaseAddress { get; set; }

    public string? RandomFortunePath { get; set; }

    public string? AllFortunesPath { get; set; }

    public string RandomFortuneURL => MakeUrl(RandomFortunePath);

    public string AllFortunesURL => MakeUrl(AllFortunesPath);

    private string MakeUrl(string path) => Scheme + "://" + BaseAddress + path;
}
