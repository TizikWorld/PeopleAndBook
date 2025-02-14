using Microsoft.EntityFrameworkCore;
using PeopleAndBook.Repository;

namespace PeopleAndBook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Старт PeopleAndBook");

            var deletedb = new AppContext();
            deletedb.DeleteDB();

            BookRepository bookRepository = new BookRepository();

            UserRepository userRepository = new UserRepository();

            //bookRepository.SelectBook(1);
            //bookRepository.DeleteBook(2);
            //bookRepository.UpdateYearBook(1, 999);
            
            

            using (var db = new AppContext())
            {
                

                var book1 = new Book() { Name = "Капитанская дочка", Author = "Пушкин", Year = 1836, Genre = "Роман" };
                var book2 = new Book() { Name = "История села Горюхина", Author = "Пушкин", Year = 1837, Genre = "Роман" };
                var book3 = new Book() { Name = "Подсознание может всё!", Author = "Джон Кехо", Year = 1997, Genre = "Литература по саморазвитию" };
                var book4 = new Book() { Name = "Любая мечта сбывается", Author = "Дебби Макомбер", Year = 2021, Genre = "Роман" };
                var book5 = new Book() { Name = "Лето 1969", Author = "Элин Хильдебранд", Year = 2022, Genre = "Исторический жанр" };

                var user1 = new User() { Name = "Максим", Email = "gmail@gmail.com" };

                db.Users.AddRange(user1);
                db.Books.AddRange(book1,book2, book3, book4, book5);
                db.SaveChanges();
            }


            bookRepository.GetBook("Роман", 1800, 1900);

                //userRepository.AddBooklUser(1, "Капитанская дочка"); Проверка множества ко множеству
        }
    }
}
