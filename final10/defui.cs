using FileObcerver;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_manager
{
    internal static class DeffUI
    {
        static keyboard keyboard = new keyboard();
        public static void drawGrid(string[] description) 
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 1);
            for (int i = 0; i < Console.BufferWidth; i++)
            {
                Console.Write("-");
            }
            for (int i = 2; i < 17; i++)
            {
                Console.SetCursorPosition(Console.BufferWidth / 5 * 3, i);
                Console.Write("|");
            }
            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 1, 2 + description.Length);
            for (int i = Console.BufferWidth / 5 * 3 + 1; i < Console.BufferWidth; i++)
            {
                Console.Write("-");
            }
            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 1, 2);
            for (int i = 0; i < description.Length; i++)
            {
                Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 1, 2 + i);
                Console.WriteLine(description[i]);
            }
        }

        public static void usersGrid(string[] description01)
        {
            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 2, 2 + description01.Length + 1);
            Console.Write("Id");

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 2, 2 + description01.Length + 2);
            Console.Write("Login");

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 2, 2 + description01.Length + 3);
            Console.Write("Password");

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 2, 2 + description01.Length + 4);
            Console.Write("Role");

        }

        public static void EmployeeGrid(string[] description01)
        {
            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 2, 2 + description01.Length + 1);
            Console.Write("Id");

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 2, 2 + description01.Length + 2);
            Console.Write("Фамилия");

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 2, 2 + description01.Length + 3);
            Console.Write("Имя");

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 2, 2 + description01.Length + 4);
            Console.Write("Отчество");

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 2, 2 + description01.Length + 5);
            Console.Write("Role");

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 2, 2 + description01.Length + 6);
            Console.Write("Паспорт");

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 2, 2 + description01.Length + 7);
            Console.Write("ЗП");

            Console.SetCursorPosition(Console.BufferWidth / 5 * 3 + 2, 2 + description01.Length + 8);
            Console.Write("Users Id");

        }


    }
}