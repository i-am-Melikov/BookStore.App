using BookStore.Core.Enums;
using BookStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace BookStore.Service.Services.Implementations
{
    public class MenuService
    {
        public bool IsAdmin = false;
        private User[] Users = { new User { UserName = "Mahammad", Password = "MiniApp" }, new User { UserName = "Mustafa", Password = "MiniApp" } };

        public async Task<bool> Login()
        {
            Console.WriteLine("Add User Name");
            string userName=Console.ReadLine();
            Console.WriteLine("Add User Password");
            string password=Console.ReadLine();
            if (Users.Any(x => x.UserName == userName && x.Password == password))
            {
                IsAdmin = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("User Name Or Password Entered Uncorrectly");
                IsAdmin= false;
            }
            return IsAdmin;
        }
        private BookWriterService bookWriterService = new BookWriterService();
        private BookService bookService = new BookService();
        public async Task ShowMenuAdmin()
        {
            Console.ForegroundColor = ConsoleColor.White;
            string sentence = "Welcome to Mustafa`s MiniApp, YOU ARE ADMIN";
            foreach (var item in sentence)
            {
                Thread.Sleep(100);
                Console.Write(item);
            }
            Console.WriteLine("0.Close App");
            Console.WriteLine("1.Create Book Writer");
            Console.WriteLine("2.Show BookWriters");
            Console.WriteLine("3.Show BookWriter  by id");
            Console.WriteLine("4.Show BookWriter's books");
            Console.WriteLine("5.Update BookWriter");
            Console.WriteLine("6.Remove BookWriter");
            Console.WriteLine("7.Create Book");
            Console.WriteLine("8.Update Book");
            Console.WriteLine("9.Show Book by BookWriter");
            Console.WriteLine("10.Remove Book");
            Console.WriteLine("11.Show All Books");
            Console.WriteLine("12.Buy Book");
            string Request = Console.ReadLine();
            while (Request != "0")
            {
                switch (Request)
                {
                    case "1":
                        Console.Clear();
                        await CreateBookWriter();
                        break;
                    case "2":
                        Console.Clear();
                        await ShowBookWriters();
                        break;
                    case "3":
                        Console.Clear();
                        await ShowBookWriterById();
                        break;
                    case "4":
                        Console.Clear();
                        await ShowBookWriterBooks();
                        break;
                    case "5":
                        Console.Clear();
                        await UpdateBookWriter();
                        break;
                    case "6":
                        Console.Clear();
                        await RemoveBookWriter();
                        break;
                    case "7":
                        Console.Clear();
                        await CreateBook();
                        break;
                    case "8":
                        Console.Clear();
                        await UpdateBook();
                        break;
                    case "9":
                        Console.Clear();
                        await GetBookByWriter();
                        break;
                    case "10":
                        Console.Clear();
                        await RemoveBook();
                        break;
                    case "11":
                        Console.Clear();
                        await ShowAllBooks();
                        break;
                    case "12":
                        Console.Clear();
                        await BuyBook();
                        break;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please select valid option");
                        break;

                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("0.Close App");
                Console.WriteLine("1.Create Book Writer");
                Console.WriteLine("2.Show Book Writers");
                Console.WriteLine("3.Show Book Writer  By Id");
                Console.WriteLine("4.Show Book Writer's Books");
                Console.WriteLine("5.Update Book Writer");
                Console.WriteLine("6.Remove Book Writer");
                Console.WriteLine("7.Create Book");
                Console.WriteLine("8.Update Book");
                Console.WriteLine("9.Show Book By Book Writer");
                Console.WriteLine("10.Remove Book");
                Console.WriteLine("11.Show All Books");
                Console.WriteLine("12.Buy Book");
                Request = Console.ReadLine();
            }
        }
        public async Task ShowMenuUser()
        {
            Console.ForegroundColor = ConsoleColor.White;
            string sentence = "Welcome to Mustafa`s App";
            foreach (var item in sentence)
            {
                Thread.Sleep(100);
                Console.Write(item);
            }
            Console.WriteLine("0.Close App");
            Console.WriteLine("1.Show Book Writers");
            Console.WriteLine("2.Show Book Writer  By Id");
            Console.WriteLine("3.Show Book Writer's Books");
            Console.WriteLine("4.Show Book By BookWriter");
            Console.WriteLine("5.Show All Books");
            Console.WriteLine("6.Buy Book");
            string Request = Console.ReadLine();
            while (Request != "0")
            {
                switch (Request)
                {
                    case "1":
                        Console.Clear();
                        await ShowBookWriters();
                        break;
                    case "2":
                        Console.Clear();
                        await ShowBookWriterById();
                        break;
                    case "3":
                        Console.Clear();
                        await ShowBookWriterBooks();
                        break;
                    case "4":
                        Console.Clear();
                        await GetBookByWriter();
                        break;
                    case "5":
                        Console.Clear();
                        await ShowAllBooks();
                        break;
                    case "6":
                        Console.Clear();
                        await BuyBook();
                        break;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please select valid option");
                        break;

                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("0.Close App");
                Console.WriteLine("1.Show Book Writers");
                Console.WriteLine("2.Show Book Writer  by id");
                Console.WriteLine("3.Show Book Writer's books");
                Console.WriteLine("4.Show Book by BookWriter");
                Console.WriteLine("5.Show All Books");
                Console.WriteLine("6.Buy Book");
                Request = Console.ReadLine();
            }
         }
        private async Task CreateBookWriter()
        {
            Console.WriteLine("Add Name");
            string name = Console.ReadLine();
            Console.WriteLine("Add SureName");
            string sureName = Console.ReadLine();
            Console.WriteLine("Add Age");
            int.TryParse(Console.ReadLine(),out int age);
            string message = await bookWriterService.CreateAsync(name, sureName, age);
            Console.WriteLine(message);
        }
        private async Task ShowBookWriters()
        {
            await bookService.GetAllAsync();
        }
        private async Task ShowBookWriterById()
        {
            Console.WriteLine("Add Book Writer`s Id");
            int.TryParse(Console.ReadLine(), out int id);
            BookWriter bookWriter = await bookWriterService.GetAsync(id);
            if (bookWriter != null)
            {
                Console.WriteLine(bookWriter);
            }
        }
        private async Task ShowBookWriterBooks()
        {
            Console.WriteLine("Add Restaurant Id");
            int.TryParse(Console.ReadLine(), out int id);
            List<Book> books = await bookWriterService.GetAllBooksAsync(id);
            if (books != null)
            {
                foreach (var item in books)
                {
                    Console.WriteLine(item);
                }
            }
        }
        private async Task UpdateBookWriter()
        {
            Console.WriteLine("Enter Id");
            int.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("Add Name");
            string name = Console.ReadLine();
            Console.WriteLine("Add SureName");
            string sureName = Console.ReadLine();
            Console.WriteLine("Add Age");
            int.TryParse(Console.ReadLine(),out int age);
            string message = await bookWriterService.UpdateAsync(id, name, sureName, age);
            Console.WriteLine(message);
        }
        private async Task RemoveBookWriter()
        {
            Console.WriteLine("Add Book Writer`s Id");
            int.TryParse(Console.ReadLine(), out int id);
            string message = await bookWriterService.DeleteAsync(id);
            Console.WriteLine(message);
        }
        private async Task CreateBook()
        {
            Console.WriteLine("Enter Book Writers Id");
            int.TryParse(Console.ReadLine(), out int id);
            Console.WriteLine("Add Name");
            string name = Console.ReadLine();
            Console.WriteLine("Add Price");
            double.TryParse(Console.ReadLine(), out double price);
            Console.WriteLine("Add Discount Price");
            double.TryParse(Console.ReadLine(), out double discountPrice);
            Console.WriteLine("Add Number Of Books in Stock");
            int.TryParse(Console.ReadLine(), out int stockNum);
            BookCategory category;
            Console.WriteLine("Choose Category");
            foreach (var item in Enum.GetValues(typeof(BookCategory)))
            {
                Console.WriteLine((int)item + "." + item);
            }
            int.TryParse(Console.ReadLine(), out int categoryIndex);
            var result = Enum.GetName(typeof(BookCategory), categoryIndex);
            while (result==null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please select valid category");
                int.TryParse(Console.ReadLine(), out categoryIndex);
                result = Enum.GetName(typeof(BookCategory), categoryIndex);
            }
            category = (BookCategory)categoryIndex;
            string massage = await bookService.CreateAsync(id, name, price, discountPrice, category, stockNum);
            Console.WriteLine(massage);
        }
        private async Task UpdateBook()
        {
            Console.WriteLine("Enter Book Writers Id");
            int.TryParse(Console.ReadLine(), out int writerId);
            Console.WriteLine("Enter Book Id");
            int.TryParse(Console.ReadLine(), out int bookId);
            Console.WriteLine("Add Name");
            string name = Console.ReadLine();
            Console.WriteLine("Add Price");
            double.TryParse(Console.ReadLine(), out double price);
            Console.WriteLine("Add Discount Price");
            double.TryParse(Console.ReadLine(), out double discountPrice);
            Console.WriteLine("Add Number of Books In Stock");
            int.TryParse(Console.ReadLine(), out int stockNum);

            string massage = await bookService.UpdateAsync(writerId, bookId, name, price, discountPrice, stockNum);
            Console.WriteLine(massage);
        }
        private async Task GetBookByWriter()
        {
            Console.WriteLine("Enter Book Writers Id");
            int.TryParse(Console.ReadLine(), out int writerId);
            Console.WriteLine("Enter Book Id");
            int.TryParse(Console.ReadLine(), out int bookId);

            Book book = await bookService.GetAsync(writerId, bookId);
            if(book != null)
            {
                Console.WriteLine(book);
            }
        }
        private async Task RemoveBook()
        {
            Console.WriteLine("Enter Book Writers Id");
            int.TryParse(Console.ReadLine(), out int writerId);
            Console.WriteLine("Enter Book Id");
            int.TryParse(Console.ReadLine(), out int bookId);

            string message = await bookService.DeleteAsync(writerId, bookId);
            Console.WriteLine(message);
        }
        private async Task ShowAllBooks()
        {
            await bookService.GetAllAsync();
        }
        private async Task BuyBook()
        {
            Console.WriteLine("Enter Book Writers Id");
            int.TryParse(Console.ReadLine(), out int writerId);
            Console.WriteLine("Enter Book Id");
            int.TryParse(Console.ReadLine(), out int bookId);

            BookService bookService = new BookService();
            bool message = await bookService.InStockAsync(writerId, bookId);
            if(message == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You Can Buy This Book");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You Can Not Buy This Book");
            }
        }
    }
}
