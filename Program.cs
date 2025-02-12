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

            bookRepository.SelectBook(1);
            bookRepository.DeleteBook(2);
            bookRepository.UpdateYearBook(1, 999);
        }
    }
}
