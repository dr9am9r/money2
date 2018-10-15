using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Money2.WebApi.Security
{
    public class JwtService : IJwtService
    {
        public String GenerateToken( Int32 id, String email, String name )
        {
            var secretKey = new SymmetricSecurityKey( JwtConfig.SecretKey );
            var signinCredentials = new SigningCredentials( secretKey, SecurityAlgorithms.HmacSha256 );

            var tokeOptions = new JwtSecurityToken(
                issuer: JwtConfig.Issuer,
                audience: JwtConfig.Audience,
                claims: new List<Claim>() {
                    new Claim( ClaimTypes.NameIdentifier, id.ToString() ),
                    new Claim( ClaimTypes.Email, email ),
                    new Claim( ClaimTypes.Name, name ) },
                expires: DateTime.Now.AddMinutes( 5 ),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken( tokeOptions );
        }
    }
}