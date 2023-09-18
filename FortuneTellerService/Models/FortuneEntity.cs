using System.ComponentModel.DataAnnotations.Schema;

namespace FortuneTellerService.Models;

public class FortuneEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    public required string Text { get; set; }

    public string? MessageFromBeyond { get; set; }
}
