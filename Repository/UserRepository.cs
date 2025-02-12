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
                    var user = db.Users.Where(user => user.Id == id).FirstOrDefault();//Создание переменной

                    if (user == null) { Console.WriteLine($"Пользователь с ИД({id}) не найден!"); return; }

                    string name = user.Name;

                    db.Users.RemoveRange(user);

                    db.SaveChanges();

                    Console.WriteLine($"Пользователь {name} успешно удален");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Возникло исключение {ex.Message}");
                return;
            }

        }
        /// <summary>
        /// Обновление почты пользователя
        /// </summary>
        /// <param name="id">ИД</param>
        public void UpdateEmailUser(int id, string email)
        {
            try
            {
                using (var db = new AppContext())
                {
                    var user = db.Users.Where(user => user.Id == id).FirstOrDefault(); // получение одной книги

                    if (user == null) { Console.WriteLine($"Пользователь с ИД({id}) не найден!"); return; }

                    user.Email = email;

                    db.Users.Update(user);

                    db.SaveChanges();

                    Console.WriteLine($"Пользователь {user.Name} успешно изменил почту на {user.Email}");
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
