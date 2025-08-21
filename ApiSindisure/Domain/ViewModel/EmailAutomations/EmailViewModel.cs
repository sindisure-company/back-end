using System.Text.Json.Serialization;

namespace ApiSindisure.Domain.ViewModel.EmailViewModel
{
    public class EmailViewModel
    {
        public class Request
        {
            public Automation? Automation { get; set; }
            public User? User { get; set; }
            public Condominium? Condominium { get; set; }
        }

        public class Automation
        {
            [JsonPropertyName("id")]
            public string? Id { get; set; }

            [JsonPropertyName("responsible_name")]
            public string? ResponsibleName { get; set; }

            [JsonPropertyName("email")]
            public string? Email { get; set; }

            [JsonPropertyName("send_cash_flow")]
            public bool SendCashFlow { get; set; }
        }

        public class User
        {
            [JsonPropertyName("id")]
            public string? Id { get; set; }

            [JsonPropertyName("email")]
            public string? email { get; set; }

            [JsonPropertyName("name")]
            public string? Name { get; set; }
        }

        public class Condominium
        {
            [JsonPropertyName("id")]
            public string? Id { get; set; }            

            [JsonPropertyName("name")]
            public string? Name { get; set; }
        }
    }
        


    
}

