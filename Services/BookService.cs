using biblio.Models;
using biblio.Repositories;
using biblio.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace biblio.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Book> GetAllBooks() => _repository.GetAllBooks();

        public Book GetBookById(int id) => _repository.GetBookById(id);

        public IEnumerable<Book> GetAllByTitle(string title)
        {
            return _repository.GetByTitle(title);
                              
        }

        public void AddBook(BookVM bookVM)
        {
            var book = new Book
            {
                Title = bookVM.Title,
                ISBN = bookVM.ISBN,
                Author = bookVM.Author,
                Price = bookVM.Price,
                Quantity = bookVM.Quantity,
                PublishYear = bookVM.PublishYear
            };

            _repository.AddBook(book);
        }

        public void UpdateBook(int id, BookVM bookVM)
        {
            var existingBook = _repository.GetBookById(id);
            if (existingBook == null)
            {
                throw new KeyNotFoundException("Book not found.");
            }

            existingBook.Title = bookVM.Title;
            existingBook.ISBN = bookVM.ISBN;
            existingBook.Author = bookVM.Author;
            existingBook.Price = bookVM.Price;
            existingBook.Quantity = bookVM.Quantity;
            existingBook.PublishYear = bookVM.PublishYear;

            _repository.UpdateBook(existingBook);
        }

        public void DeleteBook(int id) => _repository.DeleteBook(id);
    }
}
