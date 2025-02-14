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
        public void UpdateYearBook(int id, int year)
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

        //Task 25.5.4
        /// <summary>
        /// Получать список книг определенного жанра и вышедших между определенными годами.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="year"></param>
        public void GetBook(string genre, int initial_year, int final_year)
        {
            try
            {
                using (var db = new AppContext())
                {
                    var usersQuery = db.Books.Where(b => b.Genre == genre).Where(b => initial_year < b.Year && b.Year < final_year);

                    foreach (var user in usersQuery)
                    {
                        Console.WriteLine(user.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникло исключение {ex.Message}");
                return;
            }

        }
        /// <summary>
        /// Получать количество книг определенного автора в библиотеке.
        /// </summary>
        /// <param name="author"></param>
        public int GetNumBookAuthor(string author)
        {
            try
            {
                using (var db = new AppContext())
                {
                    var usersQuery = db.Books.Where(b => b.Author == author);

                    return usersQuery.Count();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникло исключение {ex.Message}");
                return 0;
            }

        }
        /// <summary>
        /// Получать количество книг определенного жанра в библиотеке.
        /// </summary>
        /// <param name="genre"></param>
        /// <returns></returns>
        public int GetNumBookGenre(string genre)
        {
            try
            {
                using (var db = new AppContext())
                {
                    var usersQuery = db.Books.Where(b => b.Genre == genre);

                    return usersQuery.Count();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникло исключение {ex.Message}");
                return 0;
            }

        }

        /// <summary>
        /// Получать булевый флаг о том, есть ли книга определенного автора и с определенным названием в библиотеке.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="year"></param>
        public bool HasBookAuthorName(string author, string name)
        {
            try
            {
                using (var db = new AppContext())
                {
                    var usersQuery = db.Books.Where(b => b.Author == author).Where(b => b.Name == name);

                    return (usersQuery != null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникло исключение {ex.Message}");
                return false;
            }

        }
        /// <summary>
        /// Получать булевый флаг о том, есть ли определенная книга на руках у пользователя.
        /// </summary>
        /// <param name="id_user"></param>
        /// <param name="name_book"></param>
        /// <returns></returns>
        public bool HasBookUser(int id_user, string name_book)
        {
            try
            {
                using (var db = new AppContext())
                {

                    var booklist = new List<Book>() { new Book() { Name = name_book} };
                    
                    var usersQuery = db.Users.Where(u => u.Id == id_user).Where(u => u.Books == booklist);

                    return (usersQuery != null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникло исключение {ex.Message}");
                return false;
            }

        }
        /// <summary>
        /// Получать количество книг на руках у пользователя.
        /// </summary>
        /// <param name="id_user"></param>
        /// <param name="name_book"></param>
        /// <returns></returns>
        public int HasBookUserNum(int id_user, string name_book)
        {
            try
            {
                using (var db = new AppContext())
                {

                    var booklist = new List<Book>() { new Book() { Name = name_book } };

                    var usersQuery = db.Users.Where(u => u.Id == id_user).Where(u => u.Books == booklist);

                    return usersQuery.Count();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникло исключение {ex.Message}");
                return 0;
            }

        }
        /// <summary>
        /// Получение последней вышедшей книги.
        /// </summary>
        public void GetNewestBook()
        {
            try
            {
                using (var db = new AppContext())
                {
                    var book = db.Books.OrderBy(b=> b.Year).FirstOrDefault(); // получение одной книги

                    if (book == null) { Console.WriteLine($"Книги не найдены!"); return; }

                    Console.WriteLine($"Самая свежая книга - {book.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникло исключение {ex.Message}");
                return;
            }

        }

        /// <summary>
        /// Получение списка всех книг, отсортированного в алфавитном порядке по названию.
        /// </summary>
        public void GetAlltBookAtoYa()
        {
            try
            {
                using (var db = new AppContext())
                {
                    var selBook = db.Books.ToList().OrderBy(b=>b.Name);
                    
                    foreach (var book in selBook)
                    {
                        Console.WriteLine(book);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникло исключение {ex.Message}");
                return;
            }
        }

        /// <summary>
        /// Получение списка всех книг, отсортированного в порядке убывания года их выхода.
        /// </summary>
        public void GetAlltBookYear()
        {
            try
            {
                using (var db = new AppContext())
                {
                    var selBook = db.Books.ToList().OrderBy(b => b.Year);

                    foreach (var book in selBook)
                    {
                        Console.WriteLine(book);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникло исключение {ex.Message}");
                return;
            }

        }


        public void UpdateYearBook1(int id, int year)
        {
            try
            {
                using (var db = new AppContext())
                {

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
