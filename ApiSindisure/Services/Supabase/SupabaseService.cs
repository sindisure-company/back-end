using Supabase;
using Supabase.Gotrue;
using SupabaseClient = Supabase.Client;


namespace ApiSindisure.Services.Supabase
{
    public class SupabaseService
    {
        private readonly SupabaseClient _client;

        public SupabaseService(IConfiguration configuration)
        {
            var url = configuration["Supabase:Url"];
            var anonKey = configuration["Supabase:AnonKey"];

            var options = new SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            if(url is not null)
            {
                _client = new SupabaseClient(url, anonKey, options);
                _client.InitializeAsync().Wait();
            }        
        }

        public async Task<Session?> SignIn(string email, string password)
        {
            try
            {
                var session = await _client.Auth.SignIn(email, password);
                return session;
            }
            catch (Exception ex) 
            {
                return null;
            }            
        }

        public async Task<User?> GetUser(string jwt)
        {
            return await _client.Auth.GetUser(jwt);
        }

        public async Task SignOut()
        {
            await _client.Auth.SignOut();
        }

        public SupabaseClient GetClient()
        {
            return _client;
        }
    }
}
