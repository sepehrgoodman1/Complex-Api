namespace Taav.Contracts.BaseClass;

public class Media
{
    public Media() { }

    public Media(string id)
    {
        Id = id;
    }

    public string? Extension { get; set; }
    public string Id { get; set; }
}