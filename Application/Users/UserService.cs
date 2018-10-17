using System;
using Money2.Domain.Users;

namespace Money2.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService( IUserRepository userRepository )
        {
            _userRepository = userRepository;
        }

        public UserDto GetUser( String email )
        {
            User user = _userRepository.GetUser( email );
            if( user == null )
            {
                return null;
            }

            return new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }

        public UserDto GetUser( Int32 id )
        {
            User user = _userRepository.Get( id );
            if ( user == null )
            {
                return null;
            }

            return new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }

        public Boolean RegisterUser( RegisterDto register )
        {
            User user = _userRepository.GetUser( register.Email );
            if( user != null )
            {
                return false;
            }

            user = new User()
            {
                Email = register.Email,
                Name = register.Name
            };

            _userRepository.Save( user, register.Password );

            return true;
        }

        public UserDto ValidateUser( LoginDto login )
        {
            User user = _userRepository.GetUser( login.Email, login.Password );
            if ( user == null )
            {
                return null;
            }

            return new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };
        }
    }
}