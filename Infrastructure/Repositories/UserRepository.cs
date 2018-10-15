using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Money2.Domain.Users;

namespace Money2.Infrastructure
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository( Money2Context context ) : base( context ) { }

        public User GetUser( String email )
        {
            return Query.FirstOrDefault( u => u.Email == email );
        }

        public User GetUser( String email, String password )
        {
            return Query.FirstOrDefault( u => u.Email == email && EF.Property<String>( u, "Password" ) == password );
        }

        public void Save( User user, string password )
        {
            Set.Update( user );
            Entry( user ).Property( "Password" ).CurrentValue = password;

            SaveChanges();
        }
    }
}