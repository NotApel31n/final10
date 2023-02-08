using FileObcerver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shop_manager
{
    internal class SignIn
    {
        keyboard keyboard = new keyboard();

        User user1;

        int noUser = 1;

        private string login = " ";
        private string password = " ";

        private int index = 1;

        register nowReg = register.down;
        enum register
        {
            up,
            down
        }

        public User Auth()
        {
            DrawMenu();
            Console.CursorVisible = false;
            Arrow(0);
            while (noUser == 1)
            {
                ConsoleKey consoleKey;
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                consoleKey = keyInfo.Key;
                KeyLogick(consoleKey);
            }
            return sinin(login, password);

        }
        public void KeyLogick(ConsoleKey consoleKey)
        {

            switch (consoleKey)
            {

                case ConsoleKey.UpArrow:
                    Arrow(-1);
                    break;
                case ConsoleKey.DownArrow:
                    Arrow(1);
                    break;
                case ConsoleKey.Escape:
                    Process.GetCurrentProcess().Kill();
                    break;
                case ConsoleKey.Enter:
                    switch (index)
                    {
                        case 1:
                            Console.CursorVisible = true;
                            Console.SetCursorPosition(10, index);
                            login = Console.ReadLine();
                            Console.CursorVisible = false;
                            Arrow(0);
                            break;
                        case 2:
                            cleanErrors();
                            ReadLine();
                            break;
                        case 3:
                            cleanErrors();
                            if (sinin(login, password) != null)
                            {
                                user1 = sinin(login, password);
                                noUser = 0;
                            }
                            break;
                    }
                    break;
            }


        }

        private void DrawMenu()
        {
            Console.SetCursorPosition(0, 1);
            Console.WriteLine(" Логин: ");
            Console.SetCursorPosition(0, 2);
            Console.WriteLine(" Пароль: ");
            Console.SetCursorPosition(0, 3);
            Console.WriteLine(" Авторизация ");
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("Управление регистром пароля - Tab. По умолчанию - нижний");
        }

        private void Arrow(int mod)
        {
            int prexindex = index;
            index += mod;
            if (index < 1) { index = 3; }
            else if (index > 3) { index = 1; }
            else
            {
                Console.SetCursorPosition(0, prexindex);
                Console.Write(" ");
                Console.SetCursorPosition(0, index);
                Console.Write(">");
            }
        }

        private User sinin(string login, string password)
        {
            UsersList users = new UsersList();
            for (int i = 0; i < users.Users.Count; i++)
            {
                if (users.Users[i].Login == login)
                {
                    if (users.Users[i].Password == password)
                    {
                        return users.Users[i];
                    }
                    Console.SetCursorPosition(1, 10);
                    Console.Write("Неверный пароль");
                    return null;
                }
                Console.SetCursorPosition(1, 10);
                Console.Write("Пользователь несуществует");
            }
            return null;
        }

        private void ReadLine()
        {
            Console.CursorVisible = true;
            Console.SetCursorPosition(10, index);
            while (true)
            {
                ConsoleKeyInfo input;
                input = Console.ReadKey(true);
                if (input.Key == ConsoleKey.Enter)
                {
                    Console.CursorVisible = false;
                    password = password.Trim();
                    break;
                }
                switch (input.Key)
                {
                    case ConsoleKey.RightArrow:
                        Console.CursorLeft += 1;
                        break;
                    case ConsoleKey.LeftArrow:
                        Console.CursorLeft += -1;
                        break;
                    case ConsoleKey.Backspace:
                        password = password.Remove(password.Length - 1, 1);
                        Console.CursorLeft += -1;
                        Console.Write(" ");
                        Console.CursorLeft += -1;
                        break;
                    case ConsoleKey.Tab:
                        if (nowReg == register.up)
                        {
                            nowReg = register.down;
                        }
                        else
                        {
                            nowReg = register.up;
                        }
                        break;
                    default:
                        Console.Write("*");
                        char inkey = (char)input.Key;
                        if (nowReg == register.up)
                        {
                            inkey = Char.ToUpper(inkey);
                        }
                        else
                        {
                            inkey = Char.ToLower(inkey);
                        }
                        password += inkey;
                        break;
                }

            }
            if (password.Length < 3)
            {
                Console.SetCursorPosition(1, 10);
                Console.Write("Слишком короткий пароль");
            }
        }

        private void cleanErrors()
        {
            Console.SetCursorPosition(1, 10);
            for (int j = 0; j < 56; j++)
            {
                Console.Write(' ');
            }
        }
    }
}