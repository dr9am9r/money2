using System;
using System.Text;

namespace Money2.WebApi.Security
{
    internal static class JwtConfig
    {
        public const String Issuer = "money2";
        public const String Audience = "money2";

        public static readonly Byte[] SecretKey = Encoding.UTF8.GetBytes( "r12!supsdferSeey@44" );
    }
}