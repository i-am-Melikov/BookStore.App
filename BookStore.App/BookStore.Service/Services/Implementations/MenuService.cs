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
        private BookWriterService bookWriterService = new BookWriterService();
        private BookService bookService = new BookService();
        public async void ShowMenu()
        {
            string sentence = "Welcome to Mustafa`s App";
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
            Console.WriteLine("9.Get Book by BookWriter");
            Console.WriteLine("10.Remove Book");
            Console.WriteLine("11.Show All Books");
            Console.WriteLine("12.Buy Book");
            string Request = Console.ReadLine();
            while (Request != "0")
            {
                switch (Request)
                {
                    case "1":
                        await CreateBookWriter();
                        break;
                    case "2":
                        await ShowBookWriters();
                        break;
                    case "3":
                        await ShowBookWriterById();
                        break;
                    case "4":
                        await UpdateBookWriter();
                        break;
                    case "5":
                        await RemoveBookWriter();
                        break;
                    case "6":
                        await CreateBook();
                        break;
                    case "7":
                        break;
                    case "8":
                        break;
                    case "9":
                        break;
                    case "10":
                        break;
                    case "11":
                        break;
                    case "12":
                        break;
                    default: break;

                }
            }
        }
        private async Task CreateBookWriter()
        {
            Console.WriteLine("Add Name");
            string name = Console.ReadLine();
            Console.WriteLine("Add SureName");
            string sureName = Console.ReadLine();
            Console.WriteLine("Add Age");
            int age = int.Parse(Console.ReadLine());
            string message = await bookWriterService.CreateAsync(name, sureName, age);
            Console.WriteLine(message);
        }
        private async Task ShowBookWriters()
        {
            await bookService.GetAllAsync();
        }

        private async Task ShowBookWriterById()
        {
            Console.WriteLine("Add Restaurant Id");
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
            int age = int.Parse(Console.ReadLine());
            string message = await bookWriterService.UpdateAsync(id, name, sureName, age);
            Console.WriteLine(message);
        }
        private async Task RemoveBookWriter()
        {
            Console.WriteLine("Add Restaurant Id");
            int.TryParse(Console.ReadLine(), out int id);
            string message = await bookWriterService.DeleteAsync(id);
            if (message != null)
            {
                Console.WriteLine(message);
            }
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
            int.TryParse(Console.ReadLine(), out int stockNum);
            BookCategory category;
            foreach (var item in Enum.GetValues(typeof(BookCategory)))
            {
                Console.WriteLine((int)item+"."+item);
            }
            int.TryParse(Console.ReadLine(), out int categoryIndex);
            bool result = Enum.IsDefined(typeof(BookCategory), categoryIndex); 
            while(!result)
            {
                Console.WriteLine("Please select valid option");
                int.TryParse(Console.ReadLine(),out categoryIndex);
            }
            category = (BookCategory)categoryIndex;
            string massage = await bookService.CreateAsync(id,name,price,discountPrice,category,stockNum);
        }
    }
}
