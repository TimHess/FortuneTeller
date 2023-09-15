using Microsoft.EntityFrameworkCore;

namespace FortuneTellerService.Models;

public class FortuneRepository(FortuneContext db) : IFortuneRepository
{
    private readonly FortuneContext _db = db;
    readonly Random _random = new();

    public Task<List<FortuneEntity>> GetAllAsync()
    {
        return _db.Fortunes.ToListAsync();
    }

    public async Task<FortuneEntity> RandomFortuneAsync()
    {
        var count = _db.Fortunes.Count();
        var index = _random.Next() % count;
        var all = await _db.Fortunes.ToListAsync();
        return all[index];
    }
}
