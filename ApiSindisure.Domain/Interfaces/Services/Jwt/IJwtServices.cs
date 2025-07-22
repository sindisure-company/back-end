namespace ApiSindisure.Domain.Interfaces.Services.Jwt
{
    public interface IJwtServices
    {
        string GenerateToken(string email);
    }
}
