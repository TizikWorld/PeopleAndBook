using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleAndBook.Repository
{
    internal class UserRepository
    {
        /// <summary>
        /// Выбор объекта из БД по его идентификатору
        /// </summary>
        /// <param name="id">ИД</param>
        public void SelectUser(int id)
        {
            using (var db = new AppContext())
            {
                var userSel = db.Users.Where(user => user.Id == id); //выбор объекта по его индентификатору

                db.SaveChanges();
                foreach (var user in userSel)
                {
                    Console.WriteLine($"Вот ваш пользователь - {user.Name} с почтой {user.Email}");
                }
            }
        }
        /// <summary>
        /// Выбор всех объектов
        /// </summary>
        public void SelectAllUser()
        {
            var selUser = new List<User>();
            using (var db = new AppContext())
            {
                selUser = db.Users.ToList();
                db.SaveChanges();
                foreach (var user in selUser)
                {
                    Console.WriteLine(user);
                }
            }
        }
        /// <summary>
        /// Добавление пользователя в БД
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="email">Год</param>
        public void AddUser(string name, string email)
        {
            using (var db = new AppContext())
            {
                db.Users.Add(new User { Name = name, Email = email });
                db.SaveChanges();
                Console.WriteLine($"Пользователь {name} успешно добавлен");
            }
        }
        /// <summary>
        /// Удаление пользователя из БД
        /// </summary>
        /// <param name="id">ИД</param>
        public void DeleteUser(int id)
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

    }
}
