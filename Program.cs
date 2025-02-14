using Microsoft.EntityFrameworkCore;
using PeopleAndBook.Repository;

namespace PeopleAndBook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Старт PeopleAndBook");

            BookRepository bookRepository = new BookRepository();

            UserRepository userRepository = new UserRepository();

            //bookRepository.SelectBook(1);
            //bookRepository.DeleteBook(2);
            //bookRepository.UpdateYearBook(1, 999);

            

            using (var db = new AppContext())
            {
                var book1 = new Book() { Name = "Капитанская дочка", Author = "Пушкин", Year = 1836, Genre = "Роман" };

                var user1 = new User() { Name = "Максим", Email = "gmail@gmail.com" };

                db.Users.AddRange(user1);
                db.Books.AddRange(book1);
                db.SaveChanges();
            }




                userRepository.AddBooklUser(1, "Капитанская дочка");
        }
    }
}
