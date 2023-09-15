using FortuneTeller;

namespace FortuneTellerUI.Services;

public interface IFortuneService
{
    Task<List<Fortune>> AllFortunesAsync();

    Task<Fortune> RandomFortuneAsync();
}
