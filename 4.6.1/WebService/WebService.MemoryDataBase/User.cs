using System;

namespace WebService.MemoryDataBase
{
    public class User
    {
        public string AccessToken { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public DateTime DateTime { get; set; }

        public int Id { get; set; }
    }
}