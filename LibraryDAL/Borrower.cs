//
// Created by abu 
//
//DataAccess is used to access the data of each file

namespace LibraryDAL
{
    public class Borrower
    {
        public int BorrowerId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }


        public Borrower()
        {
            BorrowerId = 0;
            Name = "";
            Email = "";
        }

        public Borrower(int id, string name, string emailAddress)
        {
            BorrowerId = id;
            Name = name;
            Email = emailAddress;
        }

        public void RegisterBorrower(Borrower borrower)
        {
            //Validating whether the borrower exists or not.
            if (!IsValidBorrower(borrower.BorrowerId, borrower.Email))
            {
                Console.WriteLine("A borrower with same id or email already exists.");
            }
            else
            {
                // If borrower does not exist,
                DataAccess access = new DataAccess();
                access.WriteBorrowersData(borrower);
                Console.WriteLine("Borrower registered successfully.");
            }
        }

        private bool IsValidBorrower(int borrowerId)
        {
            //Validating borrower
            DataAccess access = new DataAccess();
            var borrowers = access.ReadBorrowersData();

            for (int i = 0; i < borrowers.Count; i++)
            {
                //Substring is provided because of formatting.
                int id = borrowers[i].BorrowerId;
                if (id == borrowerId)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsValidBorrower(int borrowerId, string email)
        {
            //Validating borrower on basis of id and email.
            DataAccess access = new DataAccess();
            var borrowers = access.ReadBorrowersData();

            foreach (var borrower in borrowers)
            {
                int id = borrower.BorrowerId;
                string mail = borrower.Email;
                if (id == borrowerId || mail == email)
                {
                    return false;
                }
            }
            return true;
        }

        public void UpdateBorrower(Borrower borrower)
        {
            if (!IsValidBorrower(borrower.BorrowerId,borrower.Email))
            {
                //Means that borrower exists in the file system
                DataAccess access = new DataAccess();
                //Reading all the borrowers from file to an array of strings. Where each string is a borrower's data.
                var borrowers = access.ReadBorrowersData();
                for (int i = 0; i < borrowers.Count; i++)
                {
                    int id = borrowers[i].BorrowerId;
                    if (id == borrower.BorrowerId)
                    {
                        access.UpdateBorrowerData(borrower);
                        Console.WriteLine("Borrower updated successfully");
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("Borrower not found with id " + borrower.BorrowerId+" so no update will be made ");
            }
        }

        public void DeleteBorrower(int borrowerId)
        {
            // If borrower has any books, can not delete it.
            Transaction tx = new Transaction();
            var borrowedBooks = tx.GetBorrowedBooksByBorrower(borrowerId);
            if (borrowedBooks.Count > 0)
            {
                Console.WriteLine("Cannot delete borrower. They have borrowed books.");
                return;
            }

            DataAccess access = new DataAccess();
            var borrowers = access.ReadBorrowersData();
            if (!IsValidBorrower(borrowerId))
            {
                foreach (var borrower in borrowers)
                {
                    //Substring is provided because of the formatting.
                    int id = borrower.BorrowerId;
                    if (id == borrowerId)
                    {
                        access.DeleteBorrowerData(borrower.BorrowerId);
                        Console.WriteLine("Deleted Successfully");
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("Borrower not found with id " + borrowerId);
            }
        }
    }
}