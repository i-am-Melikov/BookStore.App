using BookStore.Core.Enums;
using BookStore.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Models
{
    public class Book : BaseModel
    {
        private static int _id;
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public string BookWriter { get; set; }
        public bool inStock { get; set; }
        public BookCategory Category { get; set; }
        public BookWriter Writer { get; set; }
        public Book(string name,double price,double discountPrice,string bookWriter,BookCategory category,BookWriter writer) 
        {
            _id++;
            Id = _id;
            Name = name;
            Price = price;
            DiscountPrice = discountPrice;
            BookWriter = bookWriter;
            Category = category;
            Writer = writer;

        }
    }
}
