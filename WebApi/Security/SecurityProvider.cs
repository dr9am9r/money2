using System;
using System.Security.Cryptography;
using System.Text;

namespace Money2.WebApi.Security
{
    public class SecurityProvider : ISecurityProvider
    {
        public String CalculateSha256( String text )
        {
            var hash = new SHA256Managed();
            var bytes = hash.ComputeHash( Encoding.UTF8.GetBytes( text ) );

            return Convert.ToBase64String( bytes );
        }
    }
}