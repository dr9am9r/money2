using System;

namespace Money2.Domain.Users
{
    public class User : IEntity
    {
        public Int32 Id { get; set; }

        public String Email { get; set; }

        public String Name { get; set; }

        public DateTime? LastLoginDate { get; set; }
    }
}
