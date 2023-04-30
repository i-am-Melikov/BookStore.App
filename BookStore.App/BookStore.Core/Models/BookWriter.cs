using BookStore.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Models
{
    public class BookWriter : BaseModel
    {
        private static int _id; 
        public string SureName { get; set; }
        public int Age { get; set; }
        public List<Book> Books;
        public BookWriter(string name,string sureName, int age) 
        {
            _id++;
            Id = _id;
            Name= name;
            SureName = sureName;
            Age = age;
            
            CreatedDate = DateTime.UtcNow.AddHours(4);
        }
        public override string ToString()
        {
            return $"Name:{Name},   Surename:{SureName},   Age:{Age},    Books:{Books},    Created Date:{CreatedDate},    Updated Date:{UpdatedDate}";
        }
    }
}
