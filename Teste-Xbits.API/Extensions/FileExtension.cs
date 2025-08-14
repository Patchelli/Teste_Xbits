namespace Teste_Xbits.API.Extensions;

public static class FileExtension
{
    private static byte[]? FileToByte(this IFormFile file)
    {
        var extensionList = new List<string>
        {
            ".pdf",
            ".PDF",
            ".xls",
            ".XLS",
            ".xlsx",
            ".XLSX",
        };

        if (file.Length <= 0) return null;

        var fileExtension = Path.GetExtension(file.FileName);

        if (!extensionList.Contains(fileExtension))
            return null;

        using var stream = new MemoryStream();

        file.CopyTo(stream);

        return stream.ToArray();
    }
}