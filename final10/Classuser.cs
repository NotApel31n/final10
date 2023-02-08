using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_manager
{
    internal enum Roles
    {
        Пользователь,
        Администратор,
        Менеджер_персонала,
        Склад_менеджер,
        Кассир,
        Бухгалтер
    }

    internal class User
    {
        private int id;
        private string login;
        private string password;
        private int role = 0;



        public User(int id, string login, string password, int role)
        {
            this.id = id;
            this.login = login;
            this.password = password;
            this.role = role;
        }

        public int Id { get => id; }
        public string Login { get => login; set => login = value; }
        public string Password { get => password; set => password = value; }
        public int Role { get => role; set => role = value; }
    }

    internal class Employee
    {
        private int id;
        private string name;
        private string surname;
        private string patronymic;
        private string DOB;
        private string passportInfo;
        private int salary;
        private int userId;

        public Employee(int id, string name, string surname, string patronymic, string dOB, string passportInfo, int salary, int userId)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.patronymic = patronymic;
            this.DOB = dOB;
            this.passportInfo = passportInfo;
            this.salary = salary;
            this.userId = userId;
        }

        public int Id { get { return id; } }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Patronymic { get => patronymic; set => patronymic = value; }
        public string DOB1 { get => DOB; set => DOB = value; }
        public string PassportInfo { get => passportInfo; set => passportInfo = value; }
        public int Salary { get => salary; set => salary = value; }
        public int UserId { get => userId; set => userId = value; }
    }
}