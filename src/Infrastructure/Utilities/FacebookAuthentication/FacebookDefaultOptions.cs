using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Utilities.FacebookAuthentication
{
    public class FacebookDefaultOptions
    {
        public const string LoginURI = "https://www.facebook.com/v3.2/dialog/oauth?";
        public const string AccessTokenURI = "https://graph.facebook.com/v3.2/oauth/access_token?";
        public const string UserInfoURI = "https://graph.facebook.com/v3.2/me?";
    }
}
