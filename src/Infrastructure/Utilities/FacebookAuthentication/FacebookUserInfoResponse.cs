using ApplicationCore.ValueObjects;
using Newtonsoft.Json;

namespace Infrastructure.Utilities.FacebookAuthentication
{
    public class FacebookUserInfoResponse
    {
        [JsonProperty("email")]
        public string Email { get; private set; }

        [JsonProperty("first_name")]
        public string Name { get; private set; }

        [JsonProperty("last_name")]
        public string Surname { get; private set; }

        [JsonProperty("location")]
        public dynamic Location { get; private set; }

        public void ChangeLocation(dynamic location)
        {
            if (location != null)
            {
                Location = location;
            }
        }
    }
}
