using Microsoft.AspNetCore.Identity;
using Service.Interfaces.Services;

namespace Service.Interfaces;

public class PasswordHashService : IHashService
{
    private readonly PasswordHasher<string> _hasher = new();

    public string Hash(string input)
    {
        return _hasher.HashPassword(null, input);
    }

    public bool Verify(string hashedPassword, string input)
    {
        var result = _hasher.VerifyHashedPassword(null, hashedPassword, input);
        return result == PasswordVerificationResult.Success;
    }
}