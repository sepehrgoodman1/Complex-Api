using Taav.Contracts.Storages.Dto;

namespace Taav.Contracts.Storages;

public interface IStorageService
{
    Task<string> Upload(byte[] file, string extension);
    Task<GetDocumentDto> Download(string id);
}
