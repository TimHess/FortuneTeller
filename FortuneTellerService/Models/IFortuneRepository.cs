namespace FortuneTellerService.Models;

public interface IFortuneRepository
{
    Task<List<FortuneEntity>> GetAllAsync();

    Task<FortuneEntity> RandomFortuneAsync();
}
