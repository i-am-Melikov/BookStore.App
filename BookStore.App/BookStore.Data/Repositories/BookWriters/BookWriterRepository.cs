using BookStore.Core.Models;
using BookStore.Core.Repositories.Bookwriter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Repositories.BookWriters
{
    public class BookWriterRepository:Repository<BookWriter>,IBookWriterRepository
    {
    }
}
