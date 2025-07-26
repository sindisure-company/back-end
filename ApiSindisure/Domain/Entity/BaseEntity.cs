namespace ApiSindisure.Domain.Entity
{
    public class BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Token { get; set; } = string.Empty;        
    }
}
