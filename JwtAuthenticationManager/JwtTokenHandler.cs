using JwtAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_Secret = "zBqAkLWw1KyGAhhfIsNl8BWxfllJE2aiG7UzEVSvKGk";
        private const string JWT_Issuer = "JwtAuthenticationManager_Issuer";
        private const string JWT_Audience = "JwtAuthenticationManager_Audience";
        private const int JWT_ExpiresInMinutes = 20;
        private readonly List<UserAccount> _userAccounts;
        public JwtTokenHandler()
        {
            _userAccounts = new List<UserAccount>
           {
                new UserAccount{ UserName  = "admin", Password = "admin123", Role = "Administrator" },
                new UserAccount{ UserName  = "user01", Password = "user123", Role = "User" }
           };
        }

        public async Task<AuthenticationResponse?> GenerateJwtToken(AuthenticationRequest authenticationRequest)
        {
            if(string.IsNullOrWhiteSpace(authenticationRequest.UserName) || string.IsNullOrWhiteSpace(authenticationRequest.Password))
                return null;
            var userAccount = _userAccounts.Where(o => o.UserName == authenticationRequest.UserName && o.Password == authenticationRequest.Password).FirstOrDefault();
            if(userAccount == null) return null;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, authenticationRequest.UserName),
                new Claim("Role", userAccount.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT_Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: JWT_Issuer,
                audience: JWT_Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(JWT_ExpiresInMinutes),
                signingCredentials: creds
            );

            var _token =  new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthenticationResponse
            {
                UserName = authenticationRequest.UserName,
                ExpiresIn = (int)DateTime.UtcNow.AddMinutes(JWT_ExpiresInMinutes).Subtract(DateTime.UtcNow).TotalSeconds,
                JwtToken = _token
            };
        }
    }
}
