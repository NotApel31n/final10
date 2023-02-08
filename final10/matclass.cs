using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop_manager
{
    internal class Product
    {
        private int id;
        private string name;
        private int price;
        private int amount;

        public Product(int id, string name, int price, int amount)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.amount = amount;
        }

        public int Id { get => id; }
        public string Name { get => name; set => name = value; }
        public int Price { get => price; set => price = value; }
        public int Amount { get => amount; set => amount = value; }
    }

    internal class SelectedProduct : Product
    {
        private int selectedAmount;
        public SelectedProduct(int id, string name, int price, int amount, int selectedAmount) : base(id, name, price, amount)
        {
            this.selectedAmount = selectedAmount;
        }

        public int SelectedAmount { get => selectedAmount; set => selectedAmount = value; }
    }

    internal class BookkeepingEntry
    {
        private int id;
        private string name;
        private int amount;
        private int date;
        private char mark;

        public BookkeepingEntry(int id, string name, int amount, int date, char mark)
        {
            this.id = id;
            this.name = name;
            this.amount = amount;
            this.date = date;
            this.mark = mark;
        }

        public int Id { get => id; }
        public string Name { get => name; set => name = value; }
        public int Amount { get => amount; set => amount = value; }
        public int Date { get => date; set => date = value; }
        public char Mark { get => mark; set => mark = value; }
    }

    internal class ProductList
    {
        public List<Product> products;

        public ProductList()
        {
            this.products = FileWorks.Deserialize<List<Product>>("ProductList");
        }

        public void SaveProductList()
        {
            FileWorks.Serialize<List<Product>>(this.products, "ProductList");
        }
    }

    internal class SelectedProductList
    {
        List<SelectedProduct> selectedProductList;
        public SelectedProductList()
        {
            this.selectedProductList = FileWorks.Deserialize<List<SelectedProduct>>("SelectedProductList");
        }
        public void SaveSelectedProductList()
        {
            FileWorks.Serialize<List<SelectedProduct>>(this.selectedProductList, "SelectedProductList");
        }
    }

    internal class BookkeepingList
    {
        List<BookkeepingEntry> bookkeepingList;
        public BookkeepingList()
        {
            this.bookkeepingList = FileWorks.Deserialize<List<BookkeepingEntry>>("BookkeepingList");
        }
        public void SaveSelectedProductList()
        {
            FileWorks.Serialize<List<BookkeepingEntry>>(this.bookkeepingList, "BookkeepingList");
        }
    }

    internal class UsersList
    {
        public List<User> Users { get; set; }

        public UsersList()
        {
            this.Users = FileWorks.Deserialize<List<User>>("UsersList");
        }

        public void SaveUsersList()
        {
            FileWorks.Serialize<List<User>>(this.Users, "UsersList");
        }
    }

    internal class EmployeeList
    {
        public List<Employee> Employees { get; set; }

        public EmployeeList()
        {
            this.Employees = FileWorks.Deserialize<List<Employee>>("EmployeeList");
        }

        public void SaveUsersList()
        {
            FileWorks.Serialize<List<Employee>>(this.Employees, "EmployeeList");
        }
    }
}