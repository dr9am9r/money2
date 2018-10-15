using System;

namespace Money2.WebApi.Security
{
    public interface ISecurityProvider
    {
        String CalculateSha256( String text );
    }
}
