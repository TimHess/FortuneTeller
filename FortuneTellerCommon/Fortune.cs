namespace FortuneTeller;

public record Fortune(int Id, string Text)
{
    public override string ToString() => $"Fortune[{Id},{Text}]";
}