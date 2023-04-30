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
            BookWriter bookWriter = await _repository.GetAsync(x => x.Id == id);
            if(bookWriter == null)
            {
                return "Book writer was not found";
            }
            if (!string.IsNullOrEmpty(await ValidateBook(name, price, discountPrice, stockNum)))
            {
                return await ValidateBook(name, price, discountPrice, stockNum);
            }

            Book book=new Book(name, price, discountPrice,category,bookWriter,stockNum);
            bookWriter.Books.Add(book);
            return "Successfully created";
        }
        public async Task<string> DeleteAsync(int writerId, int bookId)
        {
            BookWriter bookWriter = await _repository.GetAsync(x => x.Id == writerId);
            if(bookWriter == null)
            {
                return "Book writer was not found";
            }
            Book book=bookWriter.Books.FirstOrDefault(x=> x.Id == bookId);
            if(book == null)
            {
                return "Book was not found";
            }
            bookWriter.Books.Remove(book);
            return "Successfully Removed";
        }

        public async Task GetAllAsync()
        {
            foreach(var item in await _repository.GetAllAsync())
            {
                foreach (var book in item.Books)
                {
                    Console.WriteLine(book);
                }
            }
        }

        public async Task<Book> GetAsync(int writerId, int bookId)
        {
            BookWriter bookWriter = await _repository.GetAsync(x => x.Id == writerId);
            if (bookWriter == null)
            {
                Console.WriteLine( "Book writer was not found");
                return null;
            }
            Book book = bookWriter.Books.FirstOrDefault(x => x.Id == bookId);
            if (book == null)
            {
                Console.WriteLine("Book was not found");
                return null;
            }
            return book;
        }
        public async Task<object> InStockAsync(int writerId, int bookId)
        {
            BookWriter bookWriter = await _repository.GetAsync(x => x.Id == writerId);
            if (bookWriter == null)
            {
                Console.WriteLine("Book writer was not found");
                return null;
            }
            Book book = bookWriter.Books.FirstOrDefault(x => x.Id == bookId);
            if (book == null)
            {
                Console.WriteLine("Book was not found");
                return null;
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
            BookWriter bookWriter = await _repository.GetAsync(x => x.Id == writerId);
            if (bookWriter == null)
            {
                return "Book writer was not found";
            }
            if (!string.IsNullOrEmpty(await ValidateBook(name, price, discountPrice, stockNum)))
            {
                return await ValidateBook(name, price, discountPrice, stockNum);
            }
            Book book = bookWriter.Books.FirstOrDefault(x => x.Id == bookId);
            book.Name = name;
            book.Price = price;
            book.DiscountPrice = discountPrice;
            return "Successfully updated";
        }
        private async Task<string> ValidateBook(string name, double price, double discountPrice, int stockNum)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "Add correct name";
            }
            if (stockNum <= 0)
            {
                return "Add correct number of stock";
            }
            if (price <= 0)
            {
                return "Please add correct price";
            }
            if (discountPrice <= 0 || discountPrice > price)
            {
                return "Please add correct discount price";
            }
            return "";
        }
    }
}