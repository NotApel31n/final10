using FileObcerver;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shop_manager
{
    internal class AdminUI : CRUD
    {
        UsersList usersList = new UsersList();

        EmployeeList employeeList = new EmployeeList();

        User user;

        User user01;

        keyboard keyboard = new keyboard();

        int crudIndex = 0;

        string[] description01 = new string[9] { "F1 - создать запись", "F2 - поиск записи", "ESC - завершение программы", "0 - Пользователь", "1 - Админ", "2 - HR", "3 - Складовик", "4 - Кассир", "5 - Бухгалтер" };

        int index = 4;

        int ID = 0;
        string login = "";
        string password = "";
        int role = 0;

        enum menuMode
        {
            list, search
        }

        menuMode curientMenu = menuMode.list;

        public AdminUI(User user)
        {
            this.user01 = user;
            this.user = user;
        }

        public void menu()
        {
            Console.Clear();
            View();
            keyboard.OnChange += Detector;
            new Thread(keyboard.keyControl).Start();
            Arrow(0);
            User user = usersList.Users[index - 4];
        }

        public void View()
        {
            DeffUI.drawGrid(description01);
            int isNamed = 0;
            if (employeeList.Employees != null)
            {
                for (int i = 0; i < employeeList.Employees.Count; i++)
                {
                    if (employeeList.Employees[i].UserId == user01.Id)
                    {
                        Console.SetCursorPosition(Console.BufferWidth / 2 - employeeList.Employees[i].Name.Length / 2, 0);
                        Console.Write("Приветсвуем! " + employeeList.Employees[i].Name);
                        isNamed++;
                        break;
                    }
                }
                if (isNamed == 0)
                {
                    Console.SetCursorPosition(Console.BufferWidth / 2 - user01.Login.Length / 2, 0);
                    Console.Write("Приветсвуем! " + user01.Login);
                }
            }
            else
            {
                Console.SetCursorPosition(Console.BufferWidth / 2 - user01.Login.Length / 2, 0);
                Console.Write("Приветсвуем! " + user01.Login);
            }


            Console.SetCursorPosition(1, 3);
            Console.Write("Id");
            Console.SetCursorPosition(6, 3);
            Console.Write("Логин");
            Console.SetCursorPosition(18 + 6, 3);
            Console.Write("Пароль");
            Console.SetCursorPosition(18 * 2 + 6, 3);
            Console.Write("Роль");

            for (int i = 0; i < usersList.Users.Count; i++)
            {
                Console.SetCursorPosition(1, 4 + i);
                Console.Write(usersList.Users[i].Id);
                Console.SetCursorPosition(6, 4 + i);
                Console.Write(usersList.Users[i].Login);
                Console.SetCursorPosition(18 + 6, 4 + i);
                Console.Write(usersList.Users[i].Password);
                Console.SetCursorPosition(18 * 2 + 6, 4 + i);

                switch (usersList.Users[i].Role)
                {
                    case 0:
                        Console.Write("Пользователь");
                        break;

                    case 1:
                        Console.Write("Администратор");
                        break;

                    case 2:
                        Console.Write("Менеджер_персонала");
                        break;

                    case 3:
                        Console.Write("Склад_менеджер");
                        break;

                    case 4:
                        Console.Write("Кассир");
                        break;

                    case 5:
                        Console.Write("Бухгалтер");
                        break;
                }


            }
        }

        public void Create()
        {
            DeffUI.usersGrid(description01);

            CRUDcontrol();

            User user = new User(usersList.Users[usersList.Users.Count - 1].Id + 1, login, password, role);

            usersList.Users.Add(user);
            usersList.SaveUsersList();
            Console.Clear();
            View();
            Arrow(0);

        }

        public void Delete()
        {
            user = usersList.Users[index - 4];
            usersList.Users.Remove(user);
            usersList.SaveUsersList();
            Console.Clear();
            View();
            Arrow(0);
        }

        public void Read()
        {
            user = usersList.Users[index - 4];
            DeffUI.usersGrid(description01);
            UsersRead();
        }

        public void Update()
        {
            user = usersList.Users[index - 4];
            ID = user.Id;
            login = user.Login;
            password = user.Password;
            role = user.Role;
            DeffUI.usersGrid(description01);
            UsersRead();
            CRUDcontrol();
            user.Login = login;
            user.Password = password;
            user.Role = role;
            usersList.Users[index - 4] = user;
            usersList.SaveUsersList();
            Console.Clear();
            View();
            Arrow(0);
        }

        public void Detector(ConsoleKey consoleKey)
        {
            switch (consoleKey)
            {

                case ConsoleKey.UpArrow:
                    Arrow(-1);
                    break;
                case ConsoleKey.DownArrow:
                    Arrow(1);
                    break;
                case ConsoleKey.F1:
                    Create();
                    break;
                case ConsoleKey.F2:
                    DeffUI.usersGrid(description01);
                    curientMenu = menuMode.search;
                    CRUDcontrol();
                    User user = new User(ID, login, password, role);
                    index = Seracher(user);
                    Console.Clear();
                    View();
                    curientMenu = menuMode.list;
                    Arrow(0);
                    break;
                case ConsoleKey.Escape:
                    Process.GetCurrentProcess().Kill();
                    break;
                case ConsoleKey.Enter:
                    Read();
                    break;
                case ConsoleKey.Spacebar:
                    Update();
                    break;
                case ConsoleKey.Delete: Delete(); break;
            }

        }

        private void Arrow(int mod)
        {
            int prexindex = index;
            index += mod;
            if (index < 4) { index = 4; }
            else if (index - 3 > usersList.Users.Count) { index = 4; }
            else
            {
                Console.SetCursorPosition(0, prexindex);
                Console.Write(" ");
                Console.SetCursorPosition(0, index);
                Console.Write(">");
            }
        }


        private void UsersRead()
        {

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 1);
            Console.Write(user.Id);

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 2);
            Console.Write(user.Login);

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 3);
            Console.Write(user.Password);

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 4);
            Console.Write(user.Role);
        }

        private bool CRUDcontrol()
        {
            ConsoleKeyInfo input;
            Console.CursorVisible = false;
            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 1, 2 + description01.Length + 1);
            CRUDArrow(0);
            while (true)
            {
                input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        CRUDArrow(-1);
                        break;
                    case ConsoleKey.DownArrow:
                        CRUDArrow(1);
                        break;
                    case ConsoleKey.Enter:
                        if (curientMenu == menuMode.list)
                        {
                            UserEdit();
                        }
                        else
                        {
                            UserEditSearcher();
                        }
                        break;
                    case ConsoleKey.Escape:
                        return true;
                }



            }
        }

        private void CRUDArrow(int mod)
        {
            int prexindex = crudIndex;
            crudIndex += mod;
            if (crudIndex < 0) { crudIndex = 3; }
            else if (crudIndex > 3) { crudIndex = 0; }
            else
            {
                Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 1, 2 + description01.Length + prexindex + 1);
                Console.Write(" ");
                Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 1, 2 + description01.Length + crudIndex + 1);
                Console.Write(">");
            }
        }

        private void UserEdit()
        {
            Console.CursorVisible = true;
            switch (crudIndex)
            {
                case 0:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 14 + 2, 2 + description01.Length + 1);
                    Console.Write("Нельзя менять Id");
                    break;
                case 1:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 2);
                    login = Console.ReadLine();
                    break;
                case 2:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 3);
                    password = Console.ReadLine();
                    break;
                case 3:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 4);
                    role = Convert.ToInt32(Console.ReadLine());
                    if (role < 0 || role > 4)
                    {
                        role = 0;
                    }
                    break;
            }
            Console.CursorVisible = false;
        }

        private void UserEditSearcher()
        {
            Console.CursorVisible = true;
            switch (crudIndex)
            {
                case 0:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 14 + 2, 2 + description01.Length + 1);
                    ID = Convert.ToInt32(Console.ReadLine());
                    break;
                case 1:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 2);
                    login = Console.ReadLine();
                    break;
                case 2:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 3);
                    password = Console.ReadLine();
                    break;
                case 3:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 4);
                    role = Convert.ToInt32(Console.ReadLine());
                    if (role < 0 || role > 4)
                    {
                        role = 0;
                    }
                    break;
            }
            Console.CursorVisible = false;
        }

        private int Seracher(User target)
        {
            for (int i = 0; i < usersList.Users.Count; i++)
            {
                if (target.Login == usersList.Users[i].Login) { return i + 4; }
                if (target.Password == usersList.Users[i].Password) { return i + 4; }
                if (target.Role == usersList.Users[i].Role) { return i + 4; }
            }
            for (int i = 0; i < usersList.Users.Count; i++)
            {
                if (target.Id == usersList.Users[i].Id) { return i + 4; }
            }
            return index;
        }
    }
}