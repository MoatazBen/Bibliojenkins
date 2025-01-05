using biblio.Models;

namespace biblio.Repositories
{
    public class BookRepository : IBookRepository
    {
        public IEnumerable<Book> GetAllBooks() => Data.Books;

        public Book GetBookById(int id) => Data.Books.FirstOrDefault(book => book.Id == id);

        public void AddBook(Book book)
        {
            book.Id = Data.Books.Max(b => b.Id) + 1;
            Data.Books.Add(book);
        }

        public IEnumerable<Book> GetByTitle(string title)
        {
            // Filter books by title (case-insensitive)
            return Data.Books.Where(b => b.Title.Contains(title, System.StringComparison.OrdinalIgnoreCase));
        }
        public void UpdateBook(Book book)
        {
            var existingBook = GetBookById(book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.ISBN = book.ISBN;
                existingBook.Author = book.Author;
                existingBook.Price = book.Price;
                existingBook.Quantity = book.Quantity;
            }
        }

        public void DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book != null)
            {
                Data.Books.Remove(book);
            }
        }
    }
}
