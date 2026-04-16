namespace Service.Interfaces.Services;

public interface IHashService
{
    string Hash(string input);
    bool Verify(string hashedPassword, string input);
}