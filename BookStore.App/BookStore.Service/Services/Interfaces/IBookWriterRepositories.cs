using BookStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Services.Interfaces
{
    public interface IBookWriterRepositories
    {
        public Task<string> CreateAsync();
        public Task<string> DeleteAsync();
        public Task<string> UpdateAsync();
        public Task<BookWriter> GetAsync();
        public Task GetAllAsync();
        public Task<List<Book>> GetAllBookAsync();

    }
}
