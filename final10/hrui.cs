using FileObcerver;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Shop_manager
{
    internal class HR_UI : CRUD
    {
        UsersList usersList = new UsersList();

        EmployeeList employeeList = new EmployeeList();

        User user;

        keyboard keyboard = new keyboard();

        int index;
        int crudIndex;

        Employee employee01 = new Employee(0, "", "", "", "", "", 0, 0);
        int ID = 0;

        enum menuMode
        {
            list, search
        }
        menuMode curientMenu = menuMode.list;

        string[] description01 = new string[3] { "F1 - создать запись", "F2 - поиск записи", "ESC - завершение программы" };

        public HR_UI(User user)
        {
            index = 0;
            crudIndex = 0;
            this.user = user;
        }

        public void menu()
        {
            ViewMenu();
            keyboard.OnChange += Detector;
            new Thread(keyboard.keyControl).Start();
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
                    curientMenu = menuMode.search;
                    employee01 = new Employee(0, "", "", "", "", "", 0, 0);
                    DeffUI.EmployeeGrid(description01);
                    CRUD_View();
                    CRUDcontrol();
                    index = Seracher(employee01);
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
            if (index < 0) { index = employeeList.Employees.Count - 1; }
            else if (index > employeeList.Employees.Count) { index = 0; }
            else
            {
                Console.SetCursorPosition(0, prexindex + 3);
                Console.Write(" ");
                Console.SetCursorPosition(0, index + 3);
                Console.Write(">");
            }
        }

        public void Create()
        {
            employee01 = new Employee(employeeList.Employees[employeeList.Employees.Count - 1].Id + 1, "", "", "", "", "", 0, 0);
            DeffUI.EmployeeGrid(description01);
            CRUDcontrol();
            for (int i = 0; i < employeeList.Employees.Count; i++)
            {
                if (employee01.UserId == employeeList.Employees[i].UserId)
                {
                    employee01.UserId = 0;
                }
            }
            employeeList.Employees.Add(employee01);
            Console.Clear();
            ViewMenu();
            Arrow(0);
            employeeList.SaveUsersList();
        }

        public void Delete()
        {
            employeeList.Employees.RemoveAt(index);
            employeeList.SaveUsersList();
            Console.Clear();
            ViewMenu();
            Arrow(0);
        }

        public void Read()
        {
            DeffUI.EmployeeGrid(description01);
            CRUD_View();
        }

        public void Update()
        {
            employee01 = employeeList.Employees[index];
            DeffUI.EmployeeGrid(description01);
            CRUD_View();
            CRUDcontrol();
            employeeList.Employees[index] = employee01;
            Console.Clear();
            ViewMenu();
            Arrow(0);
            employeeList.SaveUsersList();
        }

        public void ViewMenu()
        {
            DeffUI.drawGrid(description01);
            View();
            Console.SetCursorPosition(1, 2);
            Console.Write("Id");
            Console.SetCursorPosition(6, 2);
            Console.Write("Имя");
            Console.SetCursorPosition(20 + 6, 2);
            Console.Write("Фамилия");
            Console.SetCursorPosition(20 * 2 + 6, 2);
            Console.Write("Отчество");
            int isNamed = 0;
            if (employeeList.Employees != null)
            {
                for (int i = 0; i < employeeList.Employees.Count; i++)
                {
                    if (employeeList.Employees[i].UserId == user.Id)
                    {
                        Console.SetCursorPosition(Console.BufferWidth / 2 - employeeList.Employees[i].Name.Length / 2, 0);
                        Console.Write("Приветсвуем! " + employeeList.Employees[i].Name);
                        isNamed++;
                    }
                }
                if (isNamed == 0)
                {
                    Console.SetCursorPosition(Console.BufferWidth / 2 - user.Login.Length / 2, 0);
                    Console.Write("Приветсвуем! " + user.Login);
                }
            }
            else
            {
                Console.SetCursorPosition(Console.BufferWidth / 2 - user.Login.Length / 2, 0);
                Console.Write("Приветсвуем! " + user.Login);
            }
        }

        private void View()
        {
            if (employeeList.Employees != null)
            {
                for (int i = 0; i < employeeList.Employees.Count; i++)
                {
                    Console.SetCursorPosition(1, 3 + i);
                    Console.Write(employeeList.Employees[i].Id);
                    Console.SetCursorPosition(6, 3 + i);
                    Console.Write(employeeList.Employees[i].Name);
                    Console.SetCursorPosition(20 + 6, 3 + i);
                    Console.Write(employeeList.Employees[i].Surname);
                    Console.SetCursorPosition(20 * 2 + 6, 3 + i);
                    Console.Write(employeeList.Employees[i].Patronymic);
                }
            }
        }

        private void CRUD_View()
        {
            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 2 + 10, 2 + description01.Length + 1);
            Console.Write(employeeList.Employees[index].Id);

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 2);
            Console.Write(employeeList.Employees[index].Surname);

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 3);
            Console.Write(employeeList.Employees[index].Name);

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 4);
            Console.Write(employeeList.Employees[index].Patronymic);

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 5);
            Console.Write(employeeList.Employees[index].DOB1);

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 6);
            Console.Write(employeeList.Employees[index].PassportInfo);

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 7);
            Console.Write(employeeList.Employees[index].Salary);

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 8);
            Console.Write(employeeList.Employees[index].UserId);
        }

        private void CRUDArrow(int mod)
        {
            int prexindex = crudIndex;
            crudIndex += mod;
            if (crudIndex < 0) { crudIndex = 7; }
            else if (crudIndex > 7) { crudIndex = 0; }
            else
            {
                Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 1, 2 + description01.Length + prexindex + 1);
                Console.Write(" ");
                Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 1, 2 + description01.Length + crudIndex + 1);
                Console.Write(">");
            }
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
                    employee01.Surname = Console.ReadLine();
                    break;
                case 2:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 3);
                    employee01.Name = Console.ReadLine();
                    break;
                case 3:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 4);
                    employee01.Patronymic = Console.ReadLine();
                    break;
                case 4:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 5);
                    employee01.DOB1 = Console.ReadLine();
                    break;
                case 5:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 6);
                    employee01.PassportInfo = Console.ReadLine();
                    break;
                case 6:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 7);
                    employee01.Salary = Convert.ToInt32(Console.ReadLine());
                    break;
                case 7:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 8);
                    employee01.UserId = Convert.ToInt32(Console.ReadLine());
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
                    employee01.Surname = Console.ReadLine();
                    break;
                case 2:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 3);
                    employee01.Name = Console.ReadLine();
                    break;
                case 3:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 4);
                    employee01.Patronymic = Console.ReadLine();
                    break;
                case 4:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 5);
                    employee01.DOB1 = Console.ReadLine();
                    break;
                case 5:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 6);
                    employee01.PassportInfo = Console.ReadLine();
                    break;
                case 6:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 7);
                    employee01.Salary = Convert.ToInt32(Console.ReadLine());
                    break;
                case 7:
                    Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 10 + 2, 2 + description01.Length + 8);
                    employee01.UserId = Convert.ToInt32(Console.ReadLine());
                    break;
            }
            Console.CursorVisible = false;
        }

        private int Seracher(Employee target)
        {
            for (int i = 0; i < employeeList.Employees.Count; i++)
            {
                if (target.Name == employeeList.Employees[i].Name) { return i; }
                if (target.Surname == employeeList.Employees[i].Surname) { return i; }
                if (target.Patronymic == employeeList.Employees[i].Patronymic) { return i; }
                if (target.UserId == employeeList.Employees[i].UserId) { return i; }
                if (target.Salary == employeeList.Employees[i].Salary) { return i; }
                if (target.DOB1 == employeeList.Employees[i].DOB1) { return i; }
                if (target.PassportInfo == employeeList.Employees[i].PassportInfo) { return i; }
            }
            for (int i = 0; i < usersList.Users.Count; i++)
            {
                if (target.Id == employeeList.Employees[i].Id) { return i; }
            }
            return index;
        }
    }
}