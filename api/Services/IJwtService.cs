namespace api.Services
{
    public interface IJwtService
    {
        string GenerateToken(string userId);
        bool ValidateToken(string token);
    }
}
