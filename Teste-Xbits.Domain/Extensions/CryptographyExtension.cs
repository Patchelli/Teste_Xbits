using System.Security.Cryptography;
using System.Text;

namespace Teste_Xbits.Domain.Extensions;

public static class CryptographyExtension
{
    public static string ConvertMd5(this string password, string identifier)
    {
        if (string.IsNullOrWhiteSpace(password)) return string.Empty;
            
        var signatureProcessed = $"{password}{identifier}";
        var dataBytes = MD5.HashData(Encoding.Default.GetBytes(signatureProcessed));
        var sbString = new StringBuilder();

        foreach (var pass in dataBytes)
        {
            sbString.Append(pass.ToString("x2"));
        }

        return sbString.ToString();
    }
}