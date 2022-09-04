using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TP01_SW2.Negocio;

namespace TP01_SW2.Repository
{

    internal class BookRepositoryCSV
    {
        private static readonly string pathCSV = @"C:\Users\Nikolas Caldeira\Documents\ADS\671\2ª & 3ª -Sistema Web II\TP01_SW2-master\TP01_SW2-master\TP01_SW2\Repository\livros.csv";
        private List<Book> _books = new List<Book>();

        public BookRepositoryCSV()
        {
            using (var file = File.OpenText(BookRepositoryCSV.pathCSV))
            {
                while(!file.EndOfStream)
                {
                    var line = file.ReadLine();
                    if(string.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    var bookInfo = line.Split(';');
                    var bookName = bookInfo[0];
                    var authors = bookInfo[1].Split('/');
                    var bookPrice = Double.Parse(bookInfo[2]);
                    var bookQty = int.Parse(bookInfo[3]);

                    List<Author> bookAuthors = new List<Author>();

                    foreach(string author in authors)
                    {
                        var authorInfo = author.Split(',');
                        var authorClass = new Author(authorInfo[0], authorInfo[1], Char.Parse(authorInfo[2]));
                        bookAuthors.Add(authorClass);
                    }

                    Book book = new Book(bookName, bookAuthors.ToArray(), bookPrice, bookQty);
                    _books.Add(book);
                }
            }

            Console.WriteLine("----- CSV -----");
            foreach(Book book in _books)
            {
                Console.WriteLine(book.ToString());
            }
        }

        public Book[] Books => _books.ToArray();
    }
}
