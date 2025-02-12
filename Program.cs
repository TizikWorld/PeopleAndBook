using Microsoft.EntityFrameworkCore;

namespace PeopleAndBook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new PeopleAndBook.AppContext())
            {
                var user1 = new User { Name = "Arthur", Email = "Admin" };
                var user2 = new User { Name = "Klim", Email = "User" };

                db.Users.Add(user1);
                db.Users.Add(user2);
                db.SaveChanges();
                Console.ReadKey();
            }
        }
    }
}
