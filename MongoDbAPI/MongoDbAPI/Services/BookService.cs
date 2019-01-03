using System.Collections.Generic;
using System.Linq;
using MongoDbAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDbAPI.Services
{
    //this is considered the interface for me to use in the CRUD operations in the Controller
    public class BookService
    {
        //there are using MongoDB command line withn LINQ awesome

        private readonly IMongoCollection<Book> _books;

        public BookService(IConfiguration config)
        {
            //Reads the server instance
            var client = new MongoClient(config.GetConnectionString("BookStoreDb"));
            var database = client.GetDatabase("BookStoreDb");
            // the data type must be defined first in a seperate Model class for model binding
            _books = database.GetCollection<Book>("Books");
        }

        public List<Book> Get()
        {
            return _books.Find(book => true).ToList();
        }

        public Book Get(string id)
        {
            var docId = new ObjectId(id);

            return _books.Find<Book>(book => book.Id == docId).FirstOrDefault();
        }

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn)
        {
            var docId = new ObjectId(id);

            _books.ReplaceOne(book => book.Id == docId, bookIn);
        }

        public void Remove(Book bookIn)
        {
            _books.DeleteOne(book => book.Id == bookIn.Id);
        }

        public void Remove(ObjectId id)
        {
            _books.DeleteOne(book => book.Id == id);
        }
    }
}
