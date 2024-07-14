//
// Created by abu 
//
//DataAccess is used to access the data of each file

using Microsoft.Data.SqlClient;

namespace LibraryDAL

{
    public class DataAccess
    {
        private readonly string _connectionString;

        public DataAccess()
        {
            _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }

        public List<Book> ReadBooksData()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Books";
                SqlCommand command = new SqlCommand(query, connection);
                
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                List<Book> books = new List<Book>();

                while (reader.Read())
                {
                    int bookId = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string author = reader.GetString(2);
                    string genre = reader.GetString(3);
                    bool isAvailable = reader.GetBoolean(4);
                    
                    Book book = new Book(bookId, title, author, genre, isAvailable);
                    books.Add(book);
                }
                
                connection.Close();
                return books;
            }
        }

        public void WriteBooksData(Book book)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO BOOKS VALUES (@BookId, @Title, @Author, @Genre, @IsAvailable)";
                SqlCommand command = new SqlCommand(query, connection);
            
            
                command.Parameters.AddWithValue("@BookId", book.BookId);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@Genre", book.Genre);
                command.Parameters.AddWithValue("@IsAvailable", book.IsAvailable);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteBookData(int bookId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"DELETE FROM BOOKS WHERE BookId = {bookId}";
                SqlCommand command = new SqlCommand(query, connection);
                
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }


        public void UpdateBookData(Book book)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE BOOKS SET BookId = @BookId, Title = @Title, Author = @Author, Genre = @Genre, IsAvailable=@IsAvailable WHERE BookId = @BookId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookId", book.BookId);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Author", book.Author);
                command.Parameters.AddWithValue("@Genre", book.Genre);
                command.Parameters.AddWithValue("@IsAvailable", book.IsAvailable);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        

        public List<Borrower> ReadBorrowersData()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Borrower";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                List<Borrower> borrowers = new List<Borrower>();
                while (reader.Read())
                {
                    int borrowerId = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string email = reader.GetString(2);
                    
                    Borrower borrower = new Borrower(borrowerId, name, email);
                    borrowers.Add(borrower);
                }
                connection.Close();
                return borrowers;
            }
        }
        
        public void WriteBorrowersData(Borrower borrower)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = $"INSERT INTO Borrower VALUES(@borrowerId, @name, @email)";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@borrowerId", borrower.BorrowerId);
                command.Parameters.AddWithValue("@name", borrower.Name);
                command.Parameters.AddWithValue("@email", borrower.Email);
                
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateBorrowerData(Borrower borrower)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Borrower SET BorrowerId = @BorrowerId , Name = @Name , Email = @Email WHERE BorrowerId = @BorrowerId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BorrowerId", borrower.BorrowerId);
                command.Parameters.AddWithValue("@Name", borrower.Name);
                command.Parameters.AddWithValue("@Email", borrower.Email);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }


        public void DeleteBorrowerData(int borrowerId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Borrower WHERE BorrowerId = @BorrowerId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BorrowerId", borrowerId);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

        }


        public List<Transaction> ReadTransactionData()
        {
            List<Transaction> transactions = new List<Transaction>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Transsaction";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int transactionId = reader.GetInt32(0);
                    int borrowerId = reader.GetInt32(1);
                    int bookId = reader.GetInt32(2);
                    DateTime transactionDate = reader.GetDateTime(3);
                    bool isBorrowed = reader.GetBoolean(4);
                    
                    Transaction transaction = new Transaction(transactionId, borrowerId, bookId, transactionDate, isBorrowed);
                    transactions.Add(transaction);
                }
                connection.Close();
                return transactions;
            }
        }

        public void WriteTransactionData(Transaction transaction)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Transsaction VALUES (@TransactionId, @BookId, @BorrowerId,@TransactionDate, @IsBorrowed)";
                SqlCommand command = new SqlCommand(query, connection);
        
                command.Parameters.AddWithValue("@TransactionId", transaction.TransactionId);
                command.Parameters.AddWithValue("@BorrowerId", transaction.BorrowerId);
                command.Parameters.AddWithValue("@BookId", transaction.BookId);
                command.Parameters.AddWithValue("@TransactionDate", transaction.Date);
                command.Parameters.AddWithValue("@IsBorrowed", transaction.IsBorrowed);
                
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}