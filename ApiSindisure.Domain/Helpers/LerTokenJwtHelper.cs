using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace ApiSindisure.Domain.Helpers
{
    public class LerTokenJwtHelper
    {
        public LerTokenJwtHelper() { }
        public static string GetFisrtName(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
          
            var userMetadataClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "user_metadata")?.Value;

            if (userMetadataClaim == null)
                return null;
       
            var json = JsonDocument.Parse(userMetadataClaim);
            var root = json.RootElement;

            if (root.TryGetProperty("first_name", out var firstNameProperty))
            {
                return firstNameProperty.GetString();
            }

            return null;
        }

        public static string GetConfirmationMail(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var userMetadataClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "user_metadata")?.Value;

            if (userMetadataClaim == null)
                return null;

            var json = JsonDocument.Parse(userMetadataClaim);
            var root = json.RootElement;

            if (root.TryGetProperty("email_verified", out var emailConfirmedProperty))
            {
                return "true";
            }

            return null;
        }
    }
}
