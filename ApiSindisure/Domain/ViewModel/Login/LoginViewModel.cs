using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiSindisure.Domain.ViewModel.Login
{
    public class LoginViewModel
    {
        public class Request 
        {           
            public string? Email { get; set; }       
            public string? Password { get; set; }
        }

        public class ResetPassword
        {
            [JsonPropertyName("email")]
            public string? Email { get; set; }

            [JsonPropertyName("redirect_to")]
            public string? RedirectTo { get; set; }

            [JsonPropertyName("access_token")]
            public string? AccessToken { get; set; }

            [JsonPropertyName("new_password")]
            public string? NewPassword { get; set; }     
        }


        public class Response
        {
            [JsonPropertyName("access_token")]
            public string? AccessToken { get; set; }

            [JsonPropertyName("token_type")]
            public string? TokenType { get; set; }

            [JsonPropertyName("expires_in")]
            public long ExpiresIn { get; set; }

            [JsonPropertyName("expires_at")]
            public long? ExpiresAt { get; set; }

            [JsonPropertyName("refresh_token")]
            public string? RefreshToken { get; set; }

            [JsonPropertyName("user")]
            public UserResponse? User { get; set; }

            public string? ErrorMessage { get; set; }
        }

        public class UserResponse
        {
            [JsonPropertyName("id")]
            public string? Id { get; set; }

            [JsonPropertyName("aud")]
            public string? Aud { get; set; }

            [JsonPropertyName("role")]
            public string? Role { get; set; }

            [JsonPropertyName("email")]
            public string? Email { get; set; }

            [JsonPropertyName("email_confirmed_at")]
            public DateTime? EmailConfirmedAt { get; set; }

            [JsonPropertyName("phone")]
            public string? Phone { get; set; }

            [JsonPropertyName("confirmed_at")]
            public DateTime? ConfirmedAt { get; set; }

            [JsonPropertyName("last_sign_in_at")]
            public DateTime? LastSignInAt { get; set; }

            [JsonPropertyName("app_metadata")]
            public AppMetadata? AppMetadata { get; set; }

            [JsonPropertyName("user_metadata")]
            public UserMetadata? UserMetadata { get; set; }

            [JsonPropertyName("identities")]
            public List<Identity>? Identities { get; set; }

            [JsonPropertyName("created_at")]
            public DateTime? CreatedAt { get; set; }

            [JsonPropertyName("updated_at")]
            public DateTime? UpdatedAt { get; set; }

            [JsonPropertyName("is_anonymous")]
            public bool? IsAnonymous { get; set; }
        }

        public class AppMetadata
        {
            [JsonPropertyName("provider")]
            public string? Provider { get; set; }

            [JsonPropertyName("providers")]
            public List<string>? Providers { get; set; }
        }

        public class UserMetadata
        {
            [JsonPropertyName("first_name")]
            public string? FirstName { get; set; }

            [JsonPropertyName("last_name")]
            public string? LastName { get; set; }

            [JsonPropertyName("address")]
            public string? Address { get; set; }

            [JsonPropertyName("cep")]
            public string? Cep { get; set; }

            [JsonPropertyName("city")]
            public string? City { get; set; }

            [JsonPropertyName("state")]
            public string? State { get; set; }

            [JsonPropertyName("neighborhood")]
            public string? Neighborhood { get; set; }

            [JsonPropertyName("number")]
            public string? Number { get; set; }

            [JsonPropertyName("phone")]
            public string? Phone { get; set; }

            [JsonPropertyName("phone_verified")]
            public bool? PhoneVerified { get; set; }

            [JsonPropertyName("document_number")]
            public string? DocumentNumber { get; set; }

            [JsonPropertyName("date_of_birth")]
            public DateTime? DateOfBirth { get; set; }

            [JsonPropertyName("sub")]
            public string? Sub { get; set; }

            [JsonPropertyName("email")]
            public string? Email { get; set; }

            [JsonPropertyName("email_verified")]
            public bool? EmailVerified { get; set; }
        }

        public class Identity
        {
            [JsonPropertyName("identity_id")]
            public string? IdentityId { get; set; }

            [JsonPropertyName("id")]
            public string? Id { get; set; }

            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }

            [JsonPropertyName("identity_data")]
            public UserMetadata? IdentityData { get; set; }

            [JsonPropertyName("provider")]
            public string? Provider { get; set; }

            [JsonPropertyName("last_sign_in_at")]
            public DateTime? LastSignInAt { get; set; }

            [JsonPropertyName("created_at")]
            public DateTime? CreatedAt { get; set; }

            [JsonPropertyName("updated_at")]
            public DateTime? UpdatedAt { get; set; }

            [JsonPropertyName("email")]
            public string? Email { get; set; }
        }
    }
}
