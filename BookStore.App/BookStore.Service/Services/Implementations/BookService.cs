using BookStore.Core.Enums;
using BookStore.Core.Models;
using BookStore.Data.Repositories;
using BookStore.Data.Repositories.BookWriters;
using BookStore.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookStore.Service.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly BookWriterRepository _repository = new BookWriterRepository();
        public async Task<string> CreateAsync(int id,string name, double price, double discountPrice, BookCategory category, int stockNum)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            BookWriter bookWriter = await _repository.GetAsync(wrt => wrt.Id == id);
            if(bookWriter == null)
                return "Book writer was not found";
            if (await ValidateBook(name, price, discountPrice, stockNum) != null)
            {
                return await ValidateBook(name, price, discountPrice, stockNum);
            }

            Book book=new Book(name, price, discountPrice,category,bookWriter,stockNum);
            bookWriter.Books.Add(book);
            Console.ForegroundColor = ConsoleColor.Green;
            return "Successfully created";
        }
        public async Task<string> DeleteAsync(int writerId, int bookId)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            BookWriter bookWriter = await _repository.GetAsync(x => x.Id == writerId);
            if(bookWriter == null)
                return "Book writer was not found";
            Book book=bookWriter.Books.FirstOrDefault(x=> x.Id == bookId);
            if(book == null)
                return "Book was not found";
            bookWriter.Books.Remove(book);
            Console.ForegroundColor = ConsoleColor.Green;
            return "Successfully Removed";
        }
        public async Task GetAllAsync()
        {
            foreach(var item in await _repository.GetAllAsync())
            {
                    Console.WriteLine(item);
            }
        }
        public async Task<Book> GetAsync(int writerId, int bookId)
        {
            BookWriter bookWriter = await _repository.GetAsync(wrt => wrt.Id == writerId);
            if (bookWriter == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine( "Book writer was not found");
                return null;
            }
            Book book = bookWriter.Books.FirstOrDefault(bok => bok.Id == bookId);
            if (book == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Book was not found");
                return null;
            }
            return book;
        }
        public async Task<bool> InStockAsync(int writerId, int bookId)
        {
            BookWriter bookWriter = await _repository.GetAsync(x => x.Id == writerId);
            if (bookWriter == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Book writer was not found");
                return false;
            }
            Book book = bookWriter.Books.FirstOrDefault(x => x.Id == bookId);
            if (book == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Book was not found");
                return false;
            }
            if (book.StockNum > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<string> UpdateAsync(int writerId, int bookId, string name, double price, double discountPrice, int stockNum)
        {
            BookWriter bookWriter = await _repository.GetAsync(wrt => wrt.Id == writerId);
                Console.ForegroundColor = ConsoleColor.Red;
            if (bookWriter == null)
            return "Book writer was not found";

            if (await ValidateBook(name, price, discountPrice, stockNum) != null) 
            {
                return await ValidateBook(name, price, discountPrice, stockNum);
            }
            Book book = bookWriter.Books.FirstOrDefault(bok => bok.Id == bookId);
            book.Name = name;
            book.Price = price;
            book.DiscountPrice = discountPrice;
            book.StockNum = stockNum;
            Console.ForegroundColor = ConsoleColor.Green;
            return "Successfully updated";
        }
        private async Task<string> ValidateBook(string name, double price, double discountPrice, int stockNum)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "Add correct name";
            }
            if (stockNum <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "Add correct number of stock";
            }
            if (price <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "Please add correct price";
            }
            if (discountPrice <= 0 || discountPrice > price)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "Please add correct discount price";
            }
            return null;
        }
    }
}