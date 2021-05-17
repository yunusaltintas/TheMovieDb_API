using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TMDB.Data.Entities;
using TMDB.Data.Security;
using TMDB.Data.SystemModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TMDB.IService;

namespace TMDB.Service
{
    public class TokenHandler : ITokenHandler
    {
        private readonly TokenOptions _tokenOptions;

        public TokenHandler(IOptions<TokenOptions> options)
        {
            _tokenOptions = options.Value;
        }
        public AccessToken CreateAccessToken(User user)
        {
            var AccessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var SecuityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            SigningCredentials SigningCredential = new SigningCredentials(SecuityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                    issuer: _tokenOptions.Issuer,
                    audience: _tokenOptions.Audience,
                    expires: AccessTokenExpiration,
                    claims: GetClaim(user),
                    notBefore: DateTime.Now,
                    signingCredentials: SigningCredential

                    );
            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            string GetRefreshToken = CreateRefreshToken();

            var AccessToken = new AccessToken()
            {
                Token = token,
                RefreshToken = GetRefreshToken,
                Expiration = AccessTokenExpiration
            };

            return AccessToken;
        }
        private string CreateRefreshToken()
        {
            var NumberByte = new Byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(NumberByte);

                return Convert.ToBase64String(NumberByte);
            }
        }
        private List<Claim> GetClaim(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(ClaimTypes.Name,$"{user.Name} {user.Surname}"),
                new  Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            return claims;
        }
        public void RemoveRefreshToken(string RefreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
