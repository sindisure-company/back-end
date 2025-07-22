namespace ApiSindisure.Domain.Dtos
{
    public class SessionDto
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresAt { get; set; }

    }
}
