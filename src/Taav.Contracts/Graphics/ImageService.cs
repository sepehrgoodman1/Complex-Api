namespace Taav.Contracts.Graphics;

public interface ImageService
{
    byte[] GetThumbnail(byte[] fileBytes, int size);
}
