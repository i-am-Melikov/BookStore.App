using BookStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Services.Interfaces
{
    public interface IBookWriterService
    {
        public Task<string> CreateAsync(string name,string sureName,int age);
        public Task<string> DeleteAsync(int id);
        public Task<string> UpdateAsync(int id, string name, string sureName, int age);
        public Task<BookWriter> GetAsync(int id);
        public Task GetAllAsync();
        public Task<List<Book>> GetAllBooksAsync(int id);
    }
}
