using System;

namespace Money2.WebApi.Security
{
    public interface IJwtService
    {
        String GenerateToken( Int32 id, String email, String name );
    }
}
