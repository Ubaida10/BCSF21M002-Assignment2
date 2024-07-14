using LibraryDAL;

namespace LibraryManagemeentSystem
{
    public class Program
    {
        Book b = new Book();
        Borrower borrower = new Borrower();
        Transaction t = new Transaction();
        private DataAccess access = new DataAccess();
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
                    //Add a new book
                    Console.Clear();
                    Console.WriteLine("Add a new book's information: ");
                    Console.WriteLine("*----------------------------*");
                    Console.Write("Enter BookId (int): ");
                    int bookId;
                    while (!int.TryParse(Console.ReadLine(), out bookId))
                    {
                        Console.Write("Invalid input. Enter BookId (int): ");
                    }

                    // Read Title
                    Console.Write("Enter Title (string): ");
                    string title = Console.ReadLine();
                    while (String.IsNullOrWhiteSpace(title))
                    {
                        Console.Write("Invalid input. Enter Title (string): ");
                        title = Console.ReadLine();
                    }

                    // Read Author
                    Console.Write("Enter Author (string): ");
                    string author = Console.ReadLine().Trim();
                    while (String.IsNullOrEmpty(author))
                    {
                        Console.Write("Invalid input. Enter Author (string): ");
                        author = Console.ReadLine();
                    }

                    // Read Genre
                    Console.Write("Enter Genre (string): ");
                    string genre = Console.ReadLine();
                    while (String.IsNullOrWhiteSpace(genre))
                    {
                        Console.Write("Invalid input. Enter Genre (string): ");
                        genre = Console.ReadLine();
                    }
                    // Read and parse IsAvailable
                    Console.Write("Enter IsAvailable (bool, true/false): ");
                    bool isAvailable;
                    while (!bool.TryParse(Console.ReadLine(), out isAvailable))
                    {
                        Console.Write("Invalid input. Enter IsAvailable (bool, true/false): ");
                    }
                    Book book = new Book(bookId, title, author, genre, isAvailable);
                    b.AddBook(book);
                    Console.WriteLine("*----------------------------*");
                    break;
                
                case 2:
                    //Remove a book
                    if (access.ReadBooksData().Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("*----------------------------*");
                        Console.WriteLine("Database is empty. No books to delete");
                        Console.WriteLine("*----------------------------*");
                        break;
                    }
                    Console.Clear();
                    Console.WriteLine("Book To Delete");
                    Console.WriteLine("*----------------------------*");
                    Console.Write("Enter BookId (int): ");
                    int id;
                    while (!int.TryParse(Console.ReadLine(), out id))
                    {
                        Console.Write("Invalid input. Enter BookId (int): ");
                    }
                    b.DeleteBook(id);
                    Console.WriteLine("*----------------------------*");
                    break;
                
                case 3:
                    //Update a book
                    if (access.ReadBooksData().Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("*----------------------------*");
                        Console.WriteLine("Database is empty. No books to update");
                        Console.WriteLine("*----------------------------*");
                        break;
                    }
                    Console.Clear();
                    Console.WriteLine("Book To Update");
                    Console.WriteLine("*----------------------------*");
                    Console.Write("Enter the new BookId (int): ");
                    int idOfBook;
                    while (!int.TryParse(Console.ReadLine(), out idOfBook))
                    {
                        Console.Write("Invalid input. Enter BookId (int): ");
                    }

                    // Read Title
                    Console.Write("Enter Title (string): ");
                    string titleOfBook = Console.ReadLine().Trim();
                    while (String.IsNullOrWhiteSpace(titleOfBook))
                    {
                        Console.Write("Invalid input. Enter Title (string): ");
                        titleOfBook = Console.ReadLine().Trim();
                    }

                    // Read Author
                    Console.Write("Enter Author (string): ");
                    string authorOfBook = Console.ReadLine().Trim();
                    while (String.IsNullOrEmpty(authorOfBook))
                    {
                        Console.Write("Invalid input. Enter Author (string): ");
                        authorOfBook = Console.ReadLine().Trim();
                    }
                    
                    // Read Genre
                    Console.Write("Enter Genre (string): ");
                    string genreOfBook = Console.ReadLine().Trim();
                    while (String.IsNullOrWhiteSpace(genreOfBook))
                    {
                        Console.Write("Invalid input. Enter Genre (string): ");
                        genreOfBook = Console.ReadLine().Trim();
                    }
                    // Read and parse IsAvailable
                    Book bookToUpdate = new Book(idOfBook, titleOfBook, authorOfBook, genreOfBook, true);
                    b.UpdateBook(bookToUpdate);
                    Console.WriteLine("*----------------------------*");
                    break;
                case 4:
                    //Register a new borrower
                    if (access.ReadBooksData().Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("*----------------------------*");
                        Console.WriteLine("No books in the library. So, no borrower can be registered.");
                        Console.WriteLine("*----------------------------*");
                        break;
                    }
                    Console.Clear();
                    Console.WriteLine("Register a New Borrower");
                    Console.WriteLine("*----------------------------*");
                    
                    Console.Write("Enter BorrowerId (int): ");
                    int borrowerId;
                    while (!int.TryParse(Console.ReadLine(), out borrowerId))
                    {
                        Console.Write("Invalid input. Enter BookId (int): ");
                    }
                    Console.Write("Enter name (string): ");
                    string borrowerName = Console.ReadLine().Trim();
                    while (String.IsNullOrWhiteSpace(borrowerName))
                    {
                        Console.WriteLine("Invalid input. Enter name (string): ");
                        borrowerName = Console.ReadLine().Trim();
                    }

                    Console.Write("Enter email (string): ");
                    string borrowerEmail = Console.ReadLine().Trim();
                    while (String.IsNullOrWhiteSpace(borrowerEmail) || !ValidateEmail(borrowerEmail))
                    {
                        Console.WriteLine("Invalid input. Enter correct email (string): ");
                        borrowerEmail = Console.ReadLine().Trim();
                    }
                    
                    
                    Borrower newBorrower = new Borrower(borrowerId, borrowerName, borrowerEmail);
                    borrower.RegisterBorrower(newBorrower);
                    Console.WriteLine("*----------------------------*");
                    break;
                
                case 5:
                    //Update a borrower
                    if (access.ReadBorrowersData().Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("*----------------------------*");
                        Console.WriteLine("Database is empty. No borrowers to update");
                        Console.WriteLine("*----------------------------*");
                        break;
                    }
                    
                    Console.Clear();
                    Console.WriteLine("Update a Borrower");
                    Console.WriteLine("*----------------------------*");
                    
                    Console.Write("Enter BorrowerId (int): ");
                    int idOfborrower;
                    while (!int.TryParse(Console.ReadLine(), out idOfborrower))
                    {
                        Console.Write("Invalid input. Enter BookId (int): ");
                    }
                    
                    Console.Write("Enter name (string): ");
                    string nameOfBorrower = Console.ReadLine().Trim();
                    while (String.IsNullOrWhiteSpace(nameOfBorrower))
                    {
                        Console.WriteLine("Invalid input. Enter name (string): ");
                        nameOfBorrower = Console.ReadLine().Trim();
                    }

                    Console.Write("Enter email (string): ");
                    string emailOfBorrower = Console.ReadLine().Trim();
                    while (String.IsNullOrWhiteSpace(emailOfBorrower) || !ValidateEmail(emailOfBorrower))
                    {
                        Console.WriteLine("Invalid input. Enter email (string): ");
                        emailOfBorrower = Console.ReadLine().Trim();
                    }

                    Borrower borrowerToUpdate = new Borrower(idOfborrower, nameOfBorrower, emailOfBorrower);
                    borrower.UpdateBorrower(borrowerToUpdate);
                    Console.WriteLine("*----------------------------*");
                    break;
                
                case 6:
                    //Delete a borrower
                    if (access.ReadBorrowersData().Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("*----------------------------*");
                        Console.WriteLine("Database is empty. No borrowers to delete");
                        Console.WriteLine("*----------------------------*");
                        break;
                    }
                    Console.Clear();
                    Console.WriteLine("Deleting a Borrower");
                    Console.WriteLine("*--------------------------------*");
                    
                    Console.Write("Enter BorrowerId (int): ");
                    int idOfborrowerToDelete;
                    while (!int.TryParse(Console.ReadLine(), out idOfborrowerToDelete))
                    {
                        Console.Write("Invalid input. Enter BookId (int): ");
                    }
                    borrower.DeleteBorrower(idOfborrowerToDelete);
                    Console.WriteLine("*----------------------------*");
                    break;
                
                case 7:
                    //Borrow a book
                    if (access.ReadBorrowersData().Count == 0 || access.ReadBorrowersData().Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("*----------------------------*");
                        Console.WriteLine("One of the data base is empty.");
                        Console.WriteLine("*----------------------------*");
                        break;
                    }
                    Console.Clear();
                    Console.WriteLine("Borrowing a book");
                    Console.WriteLine("*----------------------------*");
                    
                    Console.Write("Enter TransactionId (int): ");
                    int transactionId;
                    while (!int.TryParse(Console.ReadLine(), out transactionId))
                    {
                        Console.Write("Invalid input. Enter TransactionId (int): ");
                    }

                    Console.Write("Enter BookId (int): ");
                    int bookID;
                    while (!int.TryParse(Console.ReadLine(), out bookID))
                    {
                        Console.Write("Invalid input. Enter BookId (int): ");
                    }

                    Console.Write("Enter BorrowerId (int): ");
                    int borrowerID;
                    while (!int.TryParse(Console.ReadLine(), out borrowerID))
                    {
                        Console.Write("Invalid input. Enter BorrowerId (int): ");
                    }

                    DateTime date = DateTime.Now;
                    
                    Transaction transaction = new Transaction(transactionId, bookID, borrowerID, date, true);
                    t.RecordTransaction(transaction);
                    Console.WriteLine("*----------------------------*");
                    break;
                
                case 8:
                    //Return a book
                    
                    if (access.ReadBorrowersData().Count == 0 || access.ReadBorrowersData().Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("*----------------------------*");
                        Console.WriteLine("One of the data base is empty.");
                        Console.WriteLine("*----------------------------*");
                        break;
                    }
                    Console.Clear();
                    Console.WriteLine("Returning a book");
                    Console.WriteLine("*----------------------------*");
                    Console.Write("Enter return TransactionId (int): ");
                    int returnTransactionId;
                    while (!int.TryParse(Console.ReadLine(), out returnTransactionId))
                    {
                        Console.Write("Invalid input. Enter TransactionId (int): ");
                    }Console.Write("Enter BookId (int): ");
                    int returnBookId;
                    while (!int.TryParse(Console.ReadLine(), out returnBookId))
                    { 
                        Console.Write("Invalid input. Enter BookId (int): ");
                    }
                    Console.Write("Enter returning BorrowerId (int): ");
                    int returnBorrowerId;
                    while (!int.TryParse(Console.ReadLine(), out returnBorrowerId))
                    {
                        Console.Write("Invalid input. Enter BookId (int): ");
                    }
                    
                    DateTime returnDate = DateTime.Now;
                    
                    Transaction returnTransaction = new Transaction(returnTransactionId, returnBookId, returnBorrowerId, returnDate, false);
                    t.RecordTransaction(returnTransaction);
                    Console.WriteLine("*--------------------------------*");
                    break;
                case 9:
                    //Search for books by title, author, or genre
                    if (access.ReadBooksData().Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("*----------------------------*");
                        Console.WriteLine("Database is empty. No books to search");
                        Console.WriteLine("*----------------------------*");
                        break;
                    }
                    
                    Console.Clear();
                    Console.WriteLine("Filtering books");
                    Console.WriteLine("*--------------------------------*");
                    string query;
                    Console.WriteLine("Enter how do you want to search for books: title, author, or genre");
                    query = Console.ReadLine();
                    var ans = query.ToLower();
                    while (ans != "title" && ans != "author" && ans != "genre")
                    {
                        Console.WriteLine("Wrong input");
                        Console.WriteLine("Enter how do you want to search for books: title, author, or genre");
                        query = Console.ReadLine();
                        ans = query.ToLower();
                    }

                    var result = b.SearchBooks(ans);
                    if (result.Count > 0)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            Console.WriteLine(result[i]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No books found");
                    }
                    Console.WriteLine("*----------------------------*");
                    break;
                case 10:
                    //View all books
                    if (access.ReadBooksData().Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("*----------------------------*");
                        Console.WriteLine("Database is empty. No books found");
                        Console.WriteLine("*----------------------------*");
                        break;
                    }

                    Console.Clear();
                    Console.WriteLine("Seeing all the books");
                    Console.WriteLine("*---------------------------------*");
                    var books = b.GetAllBooks();
                    for (int i = 0; i < books.Count; i++)
                    {
                        Console.WriteLine(books[i]);
                    }
                    Console.WriteLine("*---------------------------------*");
                    break;
                case 11:
                    //View borrowed books by a specific borrower
                    if (access.ReadBooksData().Count == 0 || access.ReadBorrowersData().Count == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("*----------------------------*");
                        Console.WriteLine("One of the data base is empty. No books or borrowers found");
                        Console.WriteLine("*----------------------------*");
                        break;
                    }

                    Console.Clear();
                    Console.WriteLine("Seeing borrowed books by a specific borrower");
                    Console.WriteLine("*---------------------------------*");
                    
                    Console.Write("Enter BorrowerId (int): ");
                    int borrowerIdBooks;
                    while (!int.TryParse(Console.ReadLine(), out borrowerIdBooks))
                    {
                        Console.Write("Invalid input. Enter BookId (int): ");
                    }
                    var res = t.GetBorrowedBooksByBorrower(borrowerIdBooks);
                    if (res.Count > 0 && res!=null)
                    {
                        foreach (var borrowed in res)
                        {
                            Console.WriteLine(borrowed);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No books found");
                    }
                    Console.WriteLine("*---------------------------------*");
                    break;
                case 12:
                    //Exit the application
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Option Selected");
                    break;
            }
        }
        private static bool ValidateEmail(string email)
        {
            string[] chunks = email.Split('@');
            if (chunks.Length != 2)
                return false;

            if (chunks[0].Length == 0 || chunks[1].Length < 3)
                return false;

            if (!chunks[1].Contains("."))
                return false;

            if (!Char.IsLetter(chunks[0][0]))
                return false;

            foreach (char c in email)
            {
                if (!Char.IsLetter(c) && !Char.IsNumber(c) && c != '_' && c != '.' && c != '@')
                    return false;
            }

            if (email.Contains("..") || email.Contains(".@") || email.Contains("@.") || email.Contains("._."))
                return false;

            if (email.EndsWith("."))
                return false;

            return true;
        }
    }
}