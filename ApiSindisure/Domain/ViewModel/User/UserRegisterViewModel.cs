using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiSindisure.Domain.Entity;

namespace ApiSindisure.Domain.ViewModel.UserRegisterViewModel
{
    public class UserRegisterViewModel
    {      

        public class CreateRequest
        {              

            [Required]
            [JsonPropertyName("login")]
            public Login Login { get; set; }     
            
            [Required]
            [JsonPropertyName("date_of_birth")]
            public string? DateOfBirth { get; set; }            

            [Required]
            [JsonPropertyName("document_number")]
            public string? DocumentNumber { get; set; }       

            [Required]
            [JsonPropertyName("first_name")]
            public string? FirstName { get; set; }

            [Required]
            [JsonPropertyName("last_name")]
            public string? LastName { get; set; }

            [Required]
            [JsonPropertyName("address")]
            public string? Address { get; set; }

            [Required]
            [JsonPropertyName("neighborhood")]
            public string? Neighborhood { get; set; }

            [Required]
            [JsonPropertyName("number")]
            public string? Number { get; set; }

            [Required]
            [JsonPropertyName("cep")]
            public string? Cep { get; set; }

            [JsonPropertyName("complement")]
            public string? Complement { get; set; }

            [Required]
            [JsonPropertyName("phone")]
            public string? Phone { get; set; }

            [Required]
            [JsonPropertyName("city")]
            public string? City { get; set; }

            [Required]
            [JsonPropertyName("state")]
            public string? State { get; set; }
        }

         public class Login
        {
            [Required]
            [JsonPropertyName("email")]
            public string? Email { get; set; } 

            [Required]
            [JsonPropertyName("password")]
            public string? Password { get; set; } 
        }


        public class Response : BaseEntity
        {
            [Required]
            [JsonPropertyName("id")]
            public string? Id { get; set; }

        }
    }
}

