using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Models
{
    public class Book
    {
        public Guid _ID { get; set; }
        public string _Name { get; set; }
        public string _Author { get; set; }
        public string _ISBN { get; set; }
        public int _Year { get; set; }


        public Book(Guid ID, string Name, string Author, string ISBN, int Year)
        {
            this._ID = ID;
            this._Name = Name;
            this._Author = Author;
            this._ISBN = ISBN;
            this._Year = Year;
        }
    }
}
