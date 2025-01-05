namespace biblio.Models
{
    public static class Data
    {
        public static List<Book> Books = new List<Book>
        {
            new Book { Id = 1, Title = "The Great Gatsby", ISBN = "9780743273565", Price = 25.99, Author = "F. Scott Fitzgerald", Quantity = 10, PublishYear = 1925 },
            new Book { Id = 2, Title = "To Kill a Mockingbird", ISBN = "9780061120084", Price = 19.99, Author = "Harper Lee", Quantity = 5, PublishYear = 1960 },
            new Book { Id = 3, Title = "1984", ISBN = "9780451524935", Price = 15.50, Author = "George Orwell", Quantity = 15, PublishYear = 1949 },
            new Book { Id = 4, Title = "The Catcher in the Rye", ISBN = "9780241950432", Price = 15.75, Author = "J.D. Salinger", Quantity = 8, PublishYear = 1951 },
            new Book { Id = 5, Title = "The Hobbit", ISBN = "9780547928227", Price = 22.99, Author = "J.R.R. Tolkien", Quantity = 12, PublishYear = 1937 }
        };
    }
}
