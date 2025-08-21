using ApiSindisure.Domain.ViewModel.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiSindisure.Filters.Login
{
    public class LoginHeadersFilter : IAsyncActionFilter
    {
        public LoginHeadersFilter() { }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var headers = context.HttpContext.Request.Headers;
            if(context.ActionArguments.TryGetValue("request", out var request) && request is LoginViewModel.Request loginRequest)
            {
               
            }

            var resultContext = await next();

            if (resultContext.Result is OkObjectResult result) 
            {
                var response = result.Value as LoginViewModel.Response;

                if (response is not null)
                {
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = false, 
                        SameSite = SameSiteMode.Strict, 
                        Expires = DateTime.UtcNow.AddHours(6),
                        Path = "/"
                    };

                  
                }
            }
        }
    }
}
