using System;
using System.Collections.Generic;
using System.Linq;

namespace WebService.MemoryDataBase
{
    public static class UsersStorage
    {
        private static DateTime CleaningDate = DateTime.Now;

        private static object locker = new object();

        private static Dictionary<string, User> users = new Dictionary<string, User>();

        public static User Find(string accessKey, DateTime dateTime)
        {
            Clearing();

            lock (locker)
            {
                User user;

                if (users.TryGetValue(accessKey, out user) == true)
                {
                    if (dateTime - user.DateTime < new TimeSpan(24, 0, 0))
                    {
                        return user;
                    }
                    return null;
                }
                return null;
            }
        }

        public static void Add(User user)
        {
            Clearing();

            lock (locker)
            {
                users.Add(user.AccessToken, user);
            }
        }

        public static void Remove(string login)
        {
            Clearing();

            lock (locker)
            {
                var user = users.Values.FirstOrDefault((x) => x.Login == login);
                if (user != null)
                {
                    users.Remove(user.AccessToken);
                }
            }
        }

        private static void Clearing()
        {
            lock (locker)
            {
                if (DateTime.Now - CleaningDate > TimeSpan.FromHours(1))
                {
                    var oldUsers = users.Values.Where((x) => DateTime.Now - x.DateTime >= new TimeSpan(24, 0, 0)).ToList();

                    foreach (var u in oldUsers)
                    {
                        users.Remove(u.AccessToken);
                    }

                    CleaningDate = DateTime.Now;
                }
            }
        }
    }
}