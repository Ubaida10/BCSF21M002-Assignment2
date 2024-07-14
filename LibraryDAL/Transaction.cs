namespace LibraryDAL
{
    public class Transaction
    {
        public int TransactionId { get; private set; }
        public int BookId { get; private set; }
        public int BorrowerId { get; private set; }
        public DateTime Date { get; private set; }
        public bool IsBorrowed { get; private set; }

        public Transaction()
        {
            TransactionId = 0;
            BookId = 0;
            BorrowerId = 0;
            Date = DateTime.Now;
            IsBorrowed = false;
        }

        public Transaction(int transactionId, int bookId, int borrowerId, DateTime date, bool isBorrowed)
        {
            TransactionId = transactionId;
            BookId = bookId;
            BorrowerId = borrowerId;
            Date = date;
            IsBorrowed = isBorrowed;
        }
        
        public void RecordTransaction(Transaction transaction)
        {
            if (!CheckBorrowerAvailability(transaction.BorrowerId))
            {
                Console.WriteLine("Borrower does not exist.");
                return;
            }
            
            DataAccess access = new DataAccess();
            
            if (!transaction.IsBorrowed)
            {
                if (CheckBookAvailability(transaction.BookId))
                {
                    Console.WriteLine("This book has not been issued yet on this Id.");
                    return;
                }

                if (!CheckValidReturningBorrower(transaction.BorrowerId, transaction.BookId))
                {
                    Console.WriteLine("Book is not returned by the same borrower");
                    Console.WriteLine("Transaction could not be recorded");
                }
                else
                {
                    UpdateAvailabilityStatusOfBook(transaction.BookId, transaction.IsBorrowed);
                    access.WriteTransactionData(transaction);
                    Console.WriteLine("Transaction recorded successfully");
                }
            }
            else if (CheckBookAvailability(transaction.BookId) && CheckBorrowerAvailability(transaction.BorrowerId) && IsUniqueTransactionId(transaction.TransactionId))
            {
                access.WriteTransactionData(transaction);
                UpdateAvailabilityStatusOfBook(transaction.BookId, transaction.IsBorrowed);
                Console.WriteLine("Transaction recorded successfully");
            }
            else
            {
                Console.WriteLine("Book is already issued to someone else.");
            }
        }

        public List<Transaction> GetBorrowedBooksByBorrower(int borrowerId)
        {
            DataAccess access = new DataAccess();
            var transactions = access.ReadTransactionData();
            List<Transaction> borrowedBooks = new List<Transaction>();

            // Dictionary to keep track of the latest status of each book borrowed by the user
            Dictionary<int, Transaction> latestTransactions = new Dictionary<int, Transaction>();

            foreach (var transaction in transactions)
            {
                if (transaction.BorrowerId == borrowerId)
                {
                    if (!latestTransactions.ContainsKey(transaction.BookId))
                    {
                        latestTransactions[transaction.BookId] = transaction;
                    }
                    else if (transaction.Date > latestTransactions[transaction.BookId].Date)
                    {
                        latestTransactions[transaction.BookId] = transaction;
                    }
                }
            }

            // Only include books that are currently borrowed
            foreach (var transaction in latestTransactions.Values)
            {
                if (transaction.IsBorrowed)
                {
                    borrowedBooks.Add(transaction);
                }
            }

            return borrowedBooks;
        }


        private bool CheckBooks(List<Book> books, int bookId)
        {
            foreach (var book in books)
            {
                if (book.BookId == bookId)
                {
                    if (!book.IsAvailable)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsUniqueTransactionId(int transactionId)
        {
            DataAccess access = new DataAccess();
            var transactions = access.ReadTransactionData();
            foreach (var transaction in transactions)
            {
                if (transaction.TransactionId == transactionId)
                {
                    return false;
                }
            }
            return true;
        }
        
        private bool IsValidBookId(int bookId)
        {
            DataAccess access = new DataAccess();
            var books = access.ReadBooksData();
            foreach (var book in books)
            {
                if (book.BookId == bookId)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckBookAvailability(int bookId)
        {
            if (IsValidBookId(bookId))
            {
                DataAccess access = new DataAccess();
                var books = access.ReadBooksData();
                foreach (var book in books)
                {
                    if (book.BookId == bookId)
                    {
                        return book.IsAvailable;
                    }
                }
            }
            return false;
        }

        private bool CheckBorrowerAvailability(int borrowerId)
        {
            DataAccess access = new DataAccess();
            var borrowers = access.ReadBorrowersData();
            foreach (var borrower in borrowers)
            {
                if (borrower.BorrowerId == borrowerId)
                {
                    return true;
                }
            }
            return false;
        }

        private bool ValidIssuedBook(int bookId)
        {
            DataAccess access = new DataAccess();
            var books = access.ReadBooksData();
            foreach (var book in books)
            {
                if (book.BookId == bookId)
                {
                    return !book.IsAvailable;
                }
            }
            return false;
        }

        private bool CheckValidReturningBorrower(int returningBorrowerId, int bookId)
        {
            DataAccess access = new DataAccess();
            var transactions = access.ReadTransactionData();
            foreach (var transaction in transactions)
            {
                if (transaction.BookId == bookId)
                {
                    return returningBorrowerId == transaction.BorrowerId;
                }
            }
            return false;
        }

        private void UpdateAvailabilityStatusOfBook(int bookId, bool isBorrowed)
        {
            DataAccess access = new DataAccess();
            var books = access.ReadBooksData();
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].BookId == bookId)
                {
                    books[i].IsAvailable = !isBorrowed;
                    access.UpdateBookData(books[i]);
                    return;
                }
            }
        }

        public override string ToString()
        {
            return $"TransactionId: {TransactionId}, BookId: {BookId}, BorrowerId: {BorrowerId}, Date: {Date}, IsBorrowed: {IsBorrowed}"; 
        }
    }
}
