/*
namespace LibraryManagementSystem
{
    public class Menu
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Choose an option from the menu");
            while (true)
            {
                Console.WriteLine("1.  Add a new book");
                Console.WriteLine("2.  Remove a book");
                Console.WriteLine("3.  Update a book");
                Console.WriteLine("4.  Register a new borrower");
                Console.WriteLine("5.  Update a borrower");
                Console.WriteLine("6.  Delete a borrower");
                Console.WriteLine("7.  Borrow a book");
                Console.WriteLine("8.  Return a book");
                Console.WriteLine("9.  Search for books by title, author, or genre");
                Console.WriteLine("10. View all books");
                Console.WriteLine("11. View borrowed books by a specific borrower");
                Console.WriteLine("12. Exit the application");
                Console.WriteLine("Enter your choice: ");

                int choice = -1;
                bool validInput = false;

                while (!validInput)
                {
                    Console.Write("Enter your choice (an integer): ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out choice) && choice >= 1 && choice <= 12)
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid integer between 1 and 12.");
                    }
                }
                Program program = new Program(choice);
                program.Run();
            }
        }
    }

    public class Program
    {
        private readonly Book _bookService = new Book();
        private readonly Borrower _borrowerService = new Borrower();
        private readonly Transaction _transactionService = new Transaction();
        private readonly DataAccess _access = new DataAccess();
        private int Choosen { get; set; }

        public Program(int option)
        {
            Choosen = option;
        }

        public void Run()
        {
            switch (Choosen)
            {
                case 1:
                    AddBook();
                    break;
                case 2:
                    RemoveBook();
                    break;
                case 3:
                    UpdateBook();
                    break;
                case 4:
                    RegisterBorrower();
                    break;
                case 5:
                    UpdateBorrower();
                    break;
                case 6:
                    DeleteBorrower();
                    break;
                case 7:
                    BorrowBook();
                    break;
                case 8:
                    ReturnBook();
                    break;
                case 9:
                    SearchBooks();
                    break;
                case 10:
                    ViewAllBooks();
                    break;
                case 11:
                    ViewBorrowedBooksByBorrower();
                    break;
                case 12:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Option Selected");
                    break;
            }
        }

        private void AddBook()
        {
            Console.Clear();
            Console.WriteLine("Add a new book's information: ");
            Console.WriteLine("*----------------------------*");

            Console.Write("Enter BookId (int): ");
            int bookId = ReadInt();

            Console.Write("Enter Title (string): ");
            string title = ReadString();

            Console.Write("Enter Author (string): ");
            string author = ReadString();

            Console.Write("Enter Genre (string): ");
            string genre = ReadString();

            Console.Write("Enter IsAvailable (bool, true/false): ");
            bool isAvailable = ReadBool();

            Book book = new Book(bookId, title, author, genre, isAvailable);
            _bookService.AddBook(book);
            Console.WriteLine("*----------------------------*");
        }

        private void RemoveBook()
        {
            if (_access.ReadBooksData().Count == 0)
            {
                Console.Clear();
                Console.WriteLine("*----------------------------*");
                Console.WriteLine("Database is empty. No books to delete");
                Console.WriteLine("*----------------------------*");
                return;
            }
            Console.Clear();
            Console.WriteLine("Book To Delete");
            Console.WriteLine("*----------------------------*");
            Console.Write("Enter BookId (int): ");
            int id = ReadInt();
            _bookService.DeleteBook(id);
            Console.WriteLine("*----------------------------*");
        }

        private void UpdateBook()
        {
            if (_access.ReadBooksData().Count == 0)
            {
                Console.Clear();
                Console.WriteLine("*----------------------------*");
                Console.WriteLine("Database is empty. No books to update");
                Console.WriteLine("*----------------------------*");
                return;
            }
            Console.Clear();
            Console.WriteLine("Book To Update");
            Console.WriteLine("*----------------------------*");

            Console.Write("Enter BookId (int): ");
            int idOfBook = ReadInt();

            Console.Write("Enter Title (string): ");
            string titleOfBook = ReadString();

            Console.Write("Enter Author (string): ");
            string authorOfBook = ReadString();

            Console.Write("Enter Genre (string): ");
            string genreOfBook = ReadString();

            Book bookToUpdate = new Book(idOfBook, titleOfBook, authorOfBook, genreOfBook, true);
            _bookService.UpdateBook(bookToUpdate);
            Console.WriteLine("*----------------------------*");
        }

        private void RegisterBorrower()
        {
            Console.Clear();
            Console.WriteLine("Register a New Borrower");
            Console.WriteLine("*----------------------------*");

            Console.Write("Enter BorrowerId (int): ");
            int borrowerId = ReadInt();

            Console.Write("Enter name (string): ");
            string borrowerName = ReadString();

            Console.Write("Enter email (string): ");
            string borrowerEmail = ReadEmail();

            Borrower newBorrower = new Borrower(borrowerId, borrowerName, borrowerEmail);
            _borrowerService.RegisterBorrower(newBorrower);
            Console.WriteLine("*----------------------------*");
        }

        private void UpdateBorrower()
        {
            if (_access.ReadBorrowersData().Count == 0)
            {
                Console.Clear();
                Console.WriteLine("*----------------------------*");
                Console.WriteLine("Database is empty. No borrowers to update");
                Console.WriteLine("*----------------------------*");
                return;
            }

            Console.Clear();
            Console.WriteLine("Update a Borrower");
            Console.WriteLine("*----------------------------*");

            Console.Write("Enter BorrowerId (int): ");
            int idOfBorrower = ReadInt();

            Console.Write("Enter name (string): ");
            string nameOfBorrower = ReadString();

            Console.Write("Enter email (string): ");
            string emailOfBorrower = ReadEmail();

            Borrower borrowerToUpdate = new Borrower(idOfBorrower, nameOfBorrower, emailOfBorrower);
            _borrowerService.UpdateBorrower(borrowerToUpdate);
            Console.WriteLine("*----------------------------*");
        }

        private void DeleteBorrower()
        {
            if (_access.ReadBorrowersData().Count == 0)
            {
                Console.Clear();
                Console.WriteLine("*----------------------------*");
                Console.WriteLine("Database is empty. No borrowers to delete");
                Console.WriteLine("*----------------------------*");
                return;
            }
            Console.Clear();
            Console.WriteLine("Deleting a Borrower");
            Console.WriteLine("*--------------------------------*");

            Console.Write("Enter BorrowerId (int): ");
            int idOfBorrowerToDelete = ReadInt();
            _borrowerService.DeleteBorrower(idOfBorrowerToDelete);
            Console.WriteLine("*----------------------------*");
        }

        private void BorrowBook()
        {
            if (_access.ReadBooksData().Count == 0 || _access.ReadBorrowersData().Count == 0)
            {
                Console.Clear();
                Console.WriteLine("*----------------------------*");
                Console.WriteLine("One of the databases is empty.");
                Console.WriteLine("*----------------------------*");
                return;
            }
            Console.Clear();
            Console.WriteLine("Borrowing a book");
            Console.WriteLine("*----------------------------*");

            Console.Write("Enter TransactionId (int): ");
            int transactionId = ReadInt();

            Console.Write("Enter BookId (int): ");
            int bookID = ReadInt();

            Console.Write("Enter BorrowerId (int): ");
            int borrowerID = ReadInt();

            DateTime date = DateTime.Now;

            Transaction transaction = new Transaction(transactionId, bookID, borrowerID, date, true);
            _transactionService.RecordTransaction(transaction);
            _bookService.UpdateAvailability(bookID, false);
            Console.WriteLine("*----------------------------*");
        }

        private void ReturnBook()
        {
            if (_access.ReadBooksData().Count == 0 || _access.ReadBorrowersData().Count == 0)
            {
                Console.Clear();
                Console.WriteLine("*----------------------------*");
                Console.WriteLine("One of the databases is empty.");
                Console.WriteLine("*----------------------------*");
                return;
            }
            Console.Clear();
            Console.WriteLine("Returning a book");
            Console.WriteLine("*----------------------------*");

            Console.Write("Enter TransactionId (int): ");
            int transactionId = ReadInt();

            Console.Write("Enter BookId (int): ");
            int bookID = ReadInt();

            Console.Write("Enter BorrowerId (int): ");
            int borrowerID = ReadInt();

            DateTime date = DateTime.Now;

            Transaction transaction = new Transaction(transactionId, bookID, borrowerID, date, false);
            _transactionService.RecordTransaction(transaction);
            _bookService.UpdateAvailability(bookID, true);
            Console.WriteLine("*----------------------------*");
        }

        private void SearchBooks()
        {
            Console.Clear();
            Console.WriteLine("Search for a Book");
            Console.WriteLine("*----------------------------*");

            Console.Write("Enter search query (string): ");
            string searchQuery = ReadString();

            List<Book> books = _bookService.SearchBooks(searchQuery);

            if (books.Count == 0)
            {
                Console.WriteLine("No books found matching your query.");
            }
            else
            {
                foreach (var book in books)
                {
                    Console.WriteLine($"BookId: {book.BookId}, Title: {book.Title}, Author: {book.Author}, Genre: {book.Genre}, Available: {book.IsAvailable}");
                }
            }
            Console.WriteLine("*----------------------------*");
        }

        private void ViewAllBooks()
        {
            Console.Clear();
            Console.WriteLine("All Books");
            Console.WriteLine("*----------------------------*");

            List<Book> books = _bookService.GetAllBooks();

            if (books.Count == 0)
            {
                Console.WriteLine("No books in the library.");
            }
            else
            {
                foreach (var book in books)
                {
                    Console.WriteLine($"BookId: {book.BookId}, Title: {book.Title}, Author: {book.Author}, Genre: {book.Genre}, Available: {book.IsAvailable}");
                }
            }
            Console.WriteLine("*----------------------------*");
        }

        private void ViewBorrowedBooksByBorrower()
        {
            Console.Clear();
            Console.WriteLine("View Borrowed Books by Borrower");
            Console.WriteLine("*----------------------------*");

            Console.Write("Enter BorrowerId (int): ");
            int borrowerId = ReadInt();

            List<Transaction> transactions = _transactionService.GetBorrowedBooksByBorrower(borrowerId);

            if (transactions.Count == 0)
            {
                Console.WriteLine("This borrower has no borrowed books.");
            }
            else
            {
                foreach (var transaction in transactions)
                {
                    Console.WriteLine($"TransactionId: {transaction.TransactionId}, BookId: {transaction.BookId}, Date: {transaction.Date}, Returned: {transaction.Returned}");
                }
            }
            Console.WriteLine("*----------------------------*");
        }

        private int ReadInt()
        {
            int value;
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("Invalid input. Please enter an integer: ");
            }
            return value;
        }

        private bool ReadBool()
        {
            bool value;
            while (!bool.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("Invalid input. Please enter true or false: ");
            }
            return value;
        }

        private string ReadString()
        {
            string value = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(value))
            {
                Console.Write("Invalid input. Please enter a non-empty string: ");
                value = Console.ReadLine();
            }
            return value;
        }

        private string ReadEmail()
        {
            string value = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
            {
                Console.Write("Invalid input. Please enter a valid email address: ");
                value = Console.ReadLine();
            }
            return value;
        }
    }
}
*/
