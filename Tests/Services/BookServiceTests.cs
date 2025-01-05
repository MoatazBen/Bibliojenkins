using biblio.Models;
using biblio.Repositories;
using biblio.Services;
using biblio.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace biblio.Tests.Services
{
    public class BookServiceTests
    {
        private readonly IBookService _bookService;

        public BookServiceTests()
        {
            _bookService = new BookService(new BookRepository());
        }
        [Fact]
        public void GetAllByTitle_ShouldReturnMatchingBooks()
        {
            // Arrange
            _bookService.AddBook(new BookVM { Title = "Clean Code", ISBN = "1111111111", Author = "Robert C. Martin", Price = 25.99, Quantity = 5, PublishYear = 2008 });
            _bookService.AddBook(new BookVM { Title = "Code Complete", ISBN = "2222222222", Author = "Steve McConnell", Price = 30.99, Quantity = 3, PublishYear = 1993 });
            _bookService.AddBook(new BookVM { Title = "The Pragmatic Programmer", ISBN = "3333333333", Author = "Andy Hunt & Dave Thomas", Price = 40.00, Quantity = 7, PublishYear = 1999 });

            // Act
            var booksWithCode = _bookService.GetAllByTitle("Code");

            // Assert
            Assert.Equal(2, booksWithCode.Count()); // There are 2 books with "Code" in the title
            Assert.Contains(booksWithCode, b => b.Title == "Clean Code");
            Assert.Contains(booksWithCode, b => b.Title == "Code Complete");
        }

        [Fact]
        public void AddBook_ShouldAddBook()
        {
            // Arrange
            var bookVM = new BookVM
            {
                Title = "New Book",
                ISBN = "1234567890123",
                Author = "New Author",
                Price = 25.99,
                Quantity = 10,
                PublishYear = 2023
            };

            _bookService.AddBook(bookVM);

            var addedBook = _bookService.GetAllBooks().FirstOrDefault(b => b.Title == "New Book");
            Assert.NotNull(addedBook);
            Assert.Equal("1234567890123", addedBook.ISBN);
            Assert.Equal(2023, addedBook.PublishYear);
        }


        [Fact]
        public void UpdateBook_ShouldUpdateExistingBook()
        {
            var bookVM = new BookVM { Title = "Old Book", ISBN = "1234567890", Author = "Author 1", Price = 10.0, Quantity = 5, PublishYear = 2000 };
            _bookService.AddBook(bookVM);
            var book = _bookService.GetAllBooks().FirstOrDefault();

            var updatedBookVM = new BookVM
            {
                Title = "Updated Book",
                ISBN = "0987654321",
                Author = "Updated Author",
                Price = 20.0,
                Quantity = 10,
                PublishYear = 2025
            };

            _bookService.UpdateBook(book.Id, updatedBookVM);

            var updatedBook = _bookService.GetBookById(book.Id);
            Assert.Equal("Updated Book", updatedBook.Title);
            Assert.Equal("0987654321", updatedBook.ISBN);
            Assert.Equal(2025, updatedBook.PublishYear);
        }

        [Fact]
        public void DeleteBook_ShouldRemoveBook()
        {
            var bookVM = new BookVM { Title = "Book to Delete", ISBN = "1234567890", Author = "Author 1", Price = 10.0, Quantity = 5, PublishYear = 1995 };
            _bookService.AddBook(bookVM);
            var book = _bookService.GetAllBooks().FirstOrDefault();

            _bookService.DeleteBook(book.Id);

            var result = _bookService.GetBookById(book.Id);
            Assert.Null(result);
        }
    }
}
