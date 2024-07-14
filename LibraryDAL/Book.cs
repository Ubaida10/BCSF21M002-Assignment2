//
// Created by abu 
//
//DataAccess is used to access the data of each file
namespace LibraryDAL
{
    public class Book
    {
        public int BookId { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Genre { get; private set; }
        public bool IsAvailable{ get; set; }



        public Book()
        {
            // Initialize book object with default values
            BookId = 0;
            Title = "";
            Author = "";
            Genre = "";
            IsAvailable = false;
        }

        public Book(int id, string name, string writer, string type, bool availablity)
        {
            BookId = id;
            Title = name;
            Author = writer;
            Genre = type;
            IsAvailable = availablity;
        }

        public void AddBook(Book book)
        {
            if (!IsValidBookId(book.BookId))
            {
                DataAccess writeData = new DataAccess();
                // Write the book to the file system
                writeData.WriteBooksData(book);
                Console.WriteLine("Book added successfully");
                // Book already exists in the file system
            }
            else
            {
                Console.WriteLine("Can not add the book to the file system. Book with the same id already exists");
            }
        }

        private bool CanBeDeleted(int bookId)
        {
            DataAccess access = new DataAccess();
            var books = access.ReadBooksData();
            foreach (var book in books)
            {
                if (book.BookId == bookId)
                {
                    if (book.IsAvailable)
                    {
                        return true;
                    }

                    return false;
                }
            }
            return false;
        }

        private bool CanBeUpdated(int bookId)
        {
            DataAccess access = new DataAccess();
            var books = access.ReadBooksData();
            foreach (var book in books)
            {
                if (book.BookId == bookId)
                {
                    if (book.IsAvailable)
                    {
                        return false;
                    }

                    return true;
                }
            }
            return false;
        }

        private bool IsValidBookId(int bookId)
        {
            //Check whether the book with same id is already existing or not.
            DataAccess access = new DataAccess();
            var books = access.ReadBooksData();
            bool isValid = false;
            for (int i = 0; i < books.Count && !isValid; i++)
            {
                if (books[i].BookId == bookId)
                {
                    isValid = true;
                }
            }
            return isValid;
        }
        
        
        public void DeleteBook(int bookId)
        {
            DataAccess access = new DataAccess();
            var books = access.ReadBooksData();
            if (books.Count == 0)
            {
                Console.WriteLine("No books to delete.");
                return;
            }

            if (!IsValidBookId(bookId))
            {
                Console.WriteLine("Invalid book number.");
                return;
            }

            if (!CanBeDeleted(bookId))
            {
                Console.WriteLine("This book is currently borrowed. Cannot delete.");
                return;
            }

            access.DeleteBookData(bookId);
            Console.WriteLine("Book deleted successfully");
        }

        public void UpdateBook(Book book)
        {
            DataAccess access = new DataAccess();
            var books = access.ReadBooksData();
            if (books.Count == 0)
            {
                Console.WriteLine("No books to update.");
                return;
            }

            if (!IsValidBookId(book.BookId))
            {
                Console.WriteLine("Book does not exist.");
                return;
            }

            if (CanBeUpdated(book.BookId))
            {
                Console.WriteLine("Book is currently borrowed and cannot be updated.");
                return;
            }

            
            access.UpdateBookData(book);
            Console.WriteLine("Book updated successfully");
        }

        public List<Book> GetAllBooks()
        {
            DataAccess access = new DataAccess();
            var books = access.ReadBooksData();
            return books;
        }

        public Book GetBookById(int bookId)
        {
            DataAccess access = new DataAccess();
            var books = access.ReadBooksData();
            foreach (var book in books)
            {
                if (book.BookId == bookId)
                {
                    return book;
                }
            }
            return null;
        }

        public List<Book> SearchBooks(string query)
        {
            //Searching books based on title or author or genre
            DataAccess access = new DataAccess(); 
            var books = access.ReadBooksData();
            List<Book> bookCollection = new List<Book>();
            
            if (query == "title")
            {
                Console.WriteLine("Enter the name of the book: ");
                string bookName = Console.ReadLine().ToLower();
                while(String.IsNullOrWhiteSpace(bookName))
                {
                    Console.Write("Invalid input. Enter the name of the book: ");
                    bookName = Console.ReadLine().ToLower();
                }
                int i = 0;
                for (; i < books.Count; i++)
                {
                    string title = books[i].Title.ToLower();
                    if (title.ToLower() == bookName)
                    {
                        bookCollection.Add(books[i]);
                    }
                }
                return bookCollection;
            }
            
            if (query == "author")
            {
                Console.WriteLine("Enter the name of the author: ");
                string authName = Console.ReadLine().ToLower();
                while(String.IsNullOrWhiteSpace(authName))
                {
                    Console.Write("Invalid input. Enter the name of the author: ");
                    authName = Console.ReadLine().ToLower();
                }
                int i = 0;
                for (; i < books.Count; i++)
                {
                    // substring bcz of formatting.
                    string auth = books[i].Author.ToLower();
                    if (authName == auth.ToLower())
                    {
                        Book exist = books[i];
                        bookCollection.Add(exist);
                    }
                }
                return bookCollection;
            }
            if (query == "genre")
            {
                Console.WriteLine("Enter the genre: ");
                string category = Console.ReadLine().ToLower();
                while(String.IsNullOrWhiteSpace(category))
                {
                    Console.Write("Invalid input. Enter the genre: ");
                    category = Console.ReadLine().ToLower();
                }
                int i = 0;
                for (; i < books.Count; i++)
                {
                    // substring bcz of formatting.
                    string variety = books[i].Genre.ToLower();
                    if (variety.ToLower() == category)
                    {
                        Book exist = books[i];
                        bookCollection.Add(exist);
                    }
                }
                return bookCollection;
            }
            return bookCollection;
        }
        
        public override string ToString()
        {
            return $"ID: {BookId}, Title: {Title}, Author: {Author}, Genre: {Genre}, Availability: {IsAvailable}";
        }
    }
}