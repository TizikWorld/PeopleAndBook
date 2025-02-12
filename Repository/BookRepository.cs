using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleAndBook.Repository
{
    internal class BookRepository
    {
        /// <summary>
        /// Выбор объекта из БД по его идентификатору
        /// </summary>
        /// <param name="id">ИД</param>
        public void SelectBook(int id)
        {
            using (var db = new AppContext())
            {
                var selBook = db.Books.Where(book => book.Id == id); //выбор объекта по его индентификатору

                db.SaveChanges();
                foreach (var book in selBook)
                {
                    Console.WriteLine($"Вот ваша книга - {book.Name} - {book.Year} год");
                }
            }
        }
        /// <summary>
        /// Выбор всех объектов
        /// </summary>
        public void SelectAllBook()
        {
            var selBook = new List<Book>();
            using (var db = new AppContext())
            {
                selBook = db.Books.ToList();
                db.SaveChanges();
                foreach (var book in selBook)
                {
                    Console.WriteLine(book);
                }
            }
        }
        /// <summary>
        /// Добавление книги в БД
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="year">Год</param>
        public void AddBook(string name, int year)
        {
            using (var db = new AppContext())
            {
                db.Books.Add(new Book { Name = name, Year = year });
                db.SaveChanges();
                Console.WriteLine($"Книга {name} успешно добавлена");
            }
        }
        /// <summary>
        /// Удаление книги из БД
        /// </summary>
        /// <param name="id">ИД</param>
        public void DeleteBook(int id)
        {
            try
            {
                using (var db = new AppContext())
                {
                    var book = db.Books.Where(book => book.Id == id).FirstOrDefault();//Создание переменной

                    if (book == null) { Console.WriteLine($"Книга с ИД({id}) не найдена!"); return; }

                    string name = book.Name;

                    db.Books.RemoveRange(book);

                    db.SaveChanges();

                    Console.WriteLine($"Книга {name} успешно удалена");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникло исключение {ex.Message}");
                return;
            }

        }
        /// <summary>
        /// Обновление года выпуска книги
        /// </summary>
        /// <param name="id">ИД</param>
        public void UpdateYearBook(int id,int year)
        {
            try
            {
                using (var db = new AppContext())
                {
                    var book = db.Books.Where((book) => book.Id == id).FirstOrDefault(); // получение одной книги

                    if (book == null) { Console.WriteLine($"Книга с ИД({id}) не найдена!"); return; }

                    var name = book.Name;

                    book.Year = year;

                    db.Books.Update(book);

                    db.SaveChanges();

                    Console.WriteLine($"Книга {book.Name} успешно изменила год выпуска на {book.Year}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникло исключение {ex.Message}");
                return;
            }

        }
    }
}
