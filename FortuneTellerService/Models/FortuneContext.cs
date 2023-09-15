using Microsoft.EntityFrameworkCore;

namespace FortuneTellerService.Models;

public class FortuneContext(DbContextOptions<FortuneContext> options) : DbContext(options)
{
    public DbSet<FortuneEntity> Fortunes { get; set; }
}
