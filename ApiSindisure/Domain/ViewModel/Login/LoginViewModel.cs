using ApiSindisure.Domain.Dtos;
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
        
        public class Response
        {
            public UserDto? User { get; set; }
            public SessionDto? Session { get; set; }
            public bool? WeakPassword { get; set; }
            public string Error { get; set; }
        }        

    }
}
