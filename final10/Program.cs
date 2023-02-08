namespace Shop_manager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SignIn signIn = new SignIn();
            User user = signIn.Auth();

            switch (user.Role)
            {
                case 0:
                    Console.Clear();
                    Console.SetCursorPosition(1, 1);
                    Console.WriteLine(" Обратитесь к администратору для получения роли или восстановления учетной записи");
                    break;
                case 1:
                    AdminUI adminUI = new(user);
                    adminUI.menu();
                    break;
                case 2:
                    HR_UI hR_UI = new HR_UI(user);
                    hR_UI.menu();
                    break;
            }
        }

    }

    internal interface CRUD
    {
        void Create();
        void Read();
        void Update();
        void Delete();

    }
}