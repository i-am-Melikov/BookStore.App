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
        private readonly static BookWriterRepository _repository = new BookWriterRepository();
        public async Task<string> CreateAsync(string name,string surname,int age)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "please add valid name";
            }
            if (string.IsNullOrWhiteSpace(surname))
            {
                return "please add valid surename";
            }
            if (age <= 0)
            {
                return "please add valid age";
            }
            BookWriter bookWriter=new BookWriter(name, surname, age);
            await _repository.AddAsync(bookWriter);
            return "Successfully created";
        }

        public async Task<string> DeleteAsync(int id)
        {
            BookWriter bookWriter = await _repository.GetAsync(writer => writer.Id == id); 
            if (bookWriter == null)
            {
                return "Book writer was not found";
            }
            await _repository.RemoveAsync(bookWriter);
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
                Console.WriteLine("Book writer was not found");
                return null;
            }
            return bookWriter;
        }

        public async Task<string> UpdateAsync(int id, string name, string surname, int age)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "please add valid name";
            }
            if (string.IsNullOrWhiteSpace(surname))
            {
                return "please add valid surename";
            }
            if (age <= 0)
            {
                return "please add valid age";
            }
            BookWriter bookWriter = await _repository.GetAsync(x=> x.Id == id);
            if(bookWriter==null)
            {
                return "Book writer was not found";
            }
            bookWriter.Name = name;
            bookWriter.SureName = surname;
            bookWriter.Age = age;
            return "Successfully updated";
        }
    }
}
