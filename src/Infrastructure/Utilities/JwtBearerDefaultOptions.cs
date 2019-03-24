using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Utilities
{
    public class JwtBearerDefaultOptions
    {
        private const string secretKey = "my_super_secret_key";

        public const string ISSUER = "MY_WEB_SITE";
        public const string AUDIENCE = "https://localhost:5001";
        public const int ExpirationInSeconds = 3600;

        public static SymmetricSecurityKey GetSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
        }
    }
}
