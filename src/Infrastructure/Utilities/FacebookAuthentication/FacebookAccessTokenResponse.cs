using Newtonsoft.Json;

namespace Infrastructure.Utilities.FacebookAuthentication
{
    public class FacebookAccessTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
