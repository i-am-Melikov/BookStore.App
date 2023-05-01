using BookStore.Core.Enums;
using BookStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Services.Interfaces
{
    public interface IBookService
    {
        public Task<string> CreateAsync(int id, string name, double price,double discounntPrice, BookCategory category,int stockNum);
        public Task<string> DeleteAsync(int writerId, int bookId);
        public Task<string> UpdateAsync(int writerId, int bookId, string name, double price, double discounntPrice, int stockNum);
        public Task<Book> GetAsync(int writerId, int bookId);
        public Task<bool> InStockAsync(int writerId, int bookId);
        public Task GetAllAsync();
    }
}
