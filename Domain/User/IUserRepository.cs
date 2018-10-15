using System;

namespace Money2.Domain.Users
{
    /// <summary>
    /// Репозиторий Пользователей
    /// </summary>
    public interface IUserRepository
    {
        User GetUser( String email );

        User GetUser( String email, String password );

        void Save( User user, String password );
    }
}
