using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiSindisure.Domain.Entity;

namespace ApiSindisure.Domain.ViewModel.MessageSupportViewModel
{
    public class MessageSupportViewModel
    {
        public class GetRequest : BaseEntity
        {}

        public class CreateRequest
        {
            [Required]
            [JsonPropertyName("message")]
            public string? Message { get; set; }

            [Required]
            [JsonPropertyName("read")]
            public bool Read { get; set; }

            [JsonPropertyName("response")]
            public string? Response { get; set; }

            [Required]
            [JsonPropertyName("subject")]
            public string? Subject { get; set; }
            
            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }
         
        }

        public class UpdateRequest : BaseEntity
        {    
            [JsonPropertyName("message")]
            public string? Message { get; set; }
      
            [JsonPropertyName("read")]
            public bool Read { get; set; }
          
            [JsonPropertyName("response")]
            public string? Response { get; set; }

            [JsonPropertyName("subject")]
            public string? Subject { get; set; }

            [JsonPropertyName("category")]
            public string? UpdatedAt { get; set; }

            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }
           
        }

        public class DeleteRequest : BaseEntity
        {}

        public class Response : BaseEntity
        {
            [Required]        
            [JsonPropertyName("message")]
            public string? Message { get; set; }

            [Required]        
            [JsonPropertyName("read")]
            public bool Read { get; set; }

            [Required]
            [JsonPropertyName("subject")]
            public string? Subject { get; set; }

            [Required]
            [JsonPropertyName("updated_at")]
            public DateTime UpdatedAt { get; set; }

            [Required]
            [JsonPropertyName("created_at")]
            public DateTime CreatedAt { get; set; }

            [Required]
            [JsonPropertyName("user_id")]
            public string? UserId { get; set; }
        }
    }
}

