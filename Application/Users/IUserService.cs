using System;

namespace Money2.Application.Users
{
    public interface IUserService
    {
        UserDto GetUser( String email );

        UserDto GetUser( Int32 id );

        Boolean RegisterUser( RegisterDto register );

        UserDto ValidateUser( LoginDto login );
    }
}
