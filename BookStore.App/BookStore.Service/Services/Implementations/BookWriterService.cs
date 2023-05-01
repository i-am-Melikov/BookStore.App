using BookStore.Core.Models;
using BookStore.Data.Repositories.BookWriters;
using BookStore.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Service.Services.Implementations
{
    public class BookWriterService : IBookWriterService
    {
        private readonly BookWriterRepository _repository = new BookWriterRepository();
        public async Task<string> CreateAsync(string name,string surname,int age)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "Please Add Valid Name";
            }
            if (string.IsNullOrWhiteSpace(surname))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "Please Add Valid Surename";
            }
            if (age <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "Please Add Valid Age";
            }
            BookWriter bookWriter=new BookWriter(name, surname, age);
            await _repository.AddAsync(bookWriter);
            Console.ForegroundColor = ConsoleColor.Green;
            return "Successfully Created!";
        }

        public async Task<string> DeleteAsync(int id)
        {
            BookWriter bookWriter = await _repository.GetAsync(writer => writer.Id == id); 
            if (bookWriter == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "Book writer was not found";
            }
            await _repository.RemoveAsync(bookWriter);
            Console.ForegroundColor = ConsoleColor.Green;
            return "Successfully removed";
        }

        public async Task GetAllAsync()
        {
            foreach(var item in await _repository.GetAllAsync())
            {
                Console.WriteLine(item);
            }
        }

        public async Task<List<Book>> GetAllBooksAsync(int id)
        {
            BookWriter bookWriter = await _repository.GetAsync(writer => writer.Id == id);
            if (bookWriter == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Book writer was not found");
                return null;
            }
            return bookWriter.Books;
        }

        public async Task<BookWriter> GetAsync(int id)
        {
            BookWriter bookWriter = await _repository.GetAsync(writer => writer.Id == id);
            if (bookWriter == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Book writer was not found");
                return null;
            }
            return bookWriter;
        }

        public async Task<string> UpdateAsync(int id, string name, string surname, int age)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "please add valid name";
            }
            if (string.IsNullOrWhiteSpace(surname))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "please add valid surename";
            }
            if (age <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "please add valid age";
            }
            BookWriter bookWriter = await _repository.GetAsync(x=> x.Id == id);
            if(bookWriter==null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "Book writer was not found";
            }
            bookWriter.Name = name;
            bookWriter.SureName = surname;
            bookWriter.Age = age;
            Console.ForegroundColor = ConsoleColor.Green;
            return "Successfully updated";
        }
    }
}
