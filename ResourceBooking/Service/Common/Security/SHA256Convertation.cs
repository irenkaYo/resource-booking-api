using System.Security.Cryptography;
using System.Text;
using Service.Interfaces.Services;

namespace Service.Interfaces;

public class Sha256Convertation : IHashService
{
    public string Hash(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        var builder = new StringBuilder();
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
    }
}