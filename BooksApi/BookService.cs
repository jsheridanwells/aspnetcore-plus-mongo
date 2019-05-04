using System.Collections.Generic;
using BooksApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace BooksApi
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;
        public BookService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("BookstoreDb"));
            var database = client.GetDatabase("BookstoreDb");
            _books = database.GetCollection<Book>("Books");
        }

        public List<Book> Get()
        {
            return _books.Find<Book>(b => true)
                .ToList();        
        }

        public Book Get(string id)
        {
            return _books.Find<Book>(b => b.Id == id)
                .FirstOrDefault();
        }

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book updatedBook)
        {
            _books.ReplaceOne(b => b.Id == id, updatedBook);
        }

        public void Remove(Book bookIn)
        {
            _books.DeleteOne(b => b.Id == bookIn.Id);
        }

        public void Remove(string id)
        {
            _books.DeleteOne(b => b.Id == id);
        }
    }
}
