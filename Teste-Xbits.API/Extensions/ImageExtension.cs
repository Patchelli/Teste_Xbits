namespace Teste_Xbits.API.Extensions;

public static class ImageExtension
{
    private static byte[]? ImageToByte(this IFormFile image)
    {
        var extensionList = new List<string>
        {
            ".jpg",
            ".png"
        };

        if (image.Length <= 0) return null;

        var imageExtension = Path.GetExtension(image.FileName);

        if (!extensionList.Contains(imageExtension))
            return null;

        using var stream = new MemoryStream();

        image.CopyTo(stream);

        return stream.ToArray();
    }
}