namespace Infrastructure.Utilities
{
    public class ExternalAuthenticationResponseData
    {
        public string AccessToken { get; private set; }

        public string Username { get; private set; }

        public ExternalAuthenticationResponseData(string token, string username)
        {
            this.AccessToken = token;
            this.Username = username;
        }
    }
}
