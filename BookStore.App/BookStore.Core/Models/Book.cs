using BookStore.Core.Enums;
using BookStore.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Core.Models
{
    public class Book : BaseModel
    {
        private static int _id;
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public string BookWriter { get; set; }
        public int StockNum { get; set; }
        public bool inStock { get; set; }
        public BookCategory Category { get; set; }
        public BookWriter Writer { get; set; }
        public Book(string name, double price, double discountPrice, BookCategory category, BookWriter writer, int stockNum)
        {
            _id++;
            Id = _id;
            Name = name;
            Price = price;
            DiscountPrice = discountPrice;
            Category = category;
            Writer = writer;
            StockNum = stockNum;

        }
        public override string ToString()
        {
            if (DiscountPrice < Price)
            {
                return $"THERE IS {DiscountPrice - Price} DISCOUNT,   Name:{Name},   Discountprice:{DiscountPrice},   Category:{Category},    How many in stock{StockNum},    Created Date:{CreatedDate},    Updated Date:{UpdatedDate}";
            }
            return $"Name:{Name},   Price:{Price},   Category:{Category},    How many in stock{StockNum},    Created Date:{CreatedDate},    Updated Date:{UpdatedDate}";
        }
    }
}
