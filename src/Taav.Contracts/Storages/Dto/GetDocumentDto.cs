namespace Taav.Contracts.Storages.Dto;

public class GetDocumentDto
{
    public byte[]? Data { get; set; }
    public string Extension { get; set; } = default!;
}