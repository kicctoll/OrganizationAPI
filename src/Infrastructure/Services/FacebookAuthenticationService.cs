using System;
using System.Collections.Generic;
using System.Net.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.ValueObjects;
using Microsoft.Extensions.Configuration;
using Infrastructure.Data;
using Infrastructure.Utilities;
using Infrastructure.Utilities.FacebookAuthentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Infrastructure.Services
{
    public class FacebookAuthenticationService : IExternalAuthenticationService<ExternalAuthenticationResponseData>
    {
        private readonly IUserRepository _userRepository;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly IConfiguration _configuration;

        public FacebookAuthenticationService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<ExternalAuthenticationResponseData> Login(string code, string redirectURI)
        {
            string appId = _configuration["Authentication:Facebook:AppId"];
            string appSecret = _configuration["Authentication:Facebook:AppSecret"];

            var accessTokenResponse = await _httpClient.GetStringAsync($"{FacebookDefaultOptions.AccessTokenURI}" +
                                                                  $"client_id={appId}&" +
                                                                  $"client_secret={appSecret}&" +
                                                                  $"redirect_uri={redirectURI}&" +
                                                                  $"code={code}");
            var accessToken = JsonConvert.DeserializeObject<FacebookAccessTokenResponse>(accessTokenResponse);

            // Getting user info
            var userInfoResponse = await _httpClient.GetStringAsync($"{FacebookDefaultOptions.UserInfoURI}" +
                                                              $"fields=email,first_name,last_name,location&" +
                                                              $"access_token={accessToken.AccessToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserInfoResponse>(userInfoResponse);
            userInfo.ChangeLocation(ConvertNameToAddress(userInfo.Location.name.Value));

            // Issuing token, if user is not exist then created
            var user = await _userRepository.FindUserByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var newUser = new User(userInfo.Name, userInfo.Surname, userInfo.Email, userInfo.Location);

                await _userRepository.AddAsync(newUser);

                user = new User(userInfo.Name, userInfo.Surname, userInfo.Email, userInfo.Location);
            }

            var identity = GetIdentity(user.Name, user.Surname, user.Email);

            var jwtToken = new JwtSecurityToken(
                issuer: JwtBearerDefaultOptions.ISSUER,
                audience: JwtBearerDefaultOptions.AUDIENCE,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(JwtBearerDefaultOptions.ExpirationInSeconds),
                claims: identity.Claims,
                signingCredentials: new SigningCredentials(JwtBearerDefaultOptions.GetSecurityKey(), SecurityAlgorithms.HmacSha256));

            var jwtTokenEncoded = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return new ExternalAuthenticationResponseData(jwtTokenEncoded, $"{user.Name} {user.Surname}");
        }

        private ClaimsIdentity GetIdentity(string name, string surname, string email)
        {
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.GivenName, name),
                new Claim (ClaimTypes.Surname, surname),
                new Claim (ClaimTypes.Email, email)
            };

            return new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
        }

        private Address ConvertNameToAddress(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                string[] partsOfName = name.Split(", ");
                return new Address(partsOfName[1], partsOfName[0]);
            }
            else
            {
                return new Address("", "");
            }
        }
    }
}
