namespace LibraryManagemeentSystem;

public class Menu
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Choose an option form menu");
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

            int choosen = -1;
            int choice;
            bool validInput = false;

            while (!validInput)
            {
                Console.Write("Enter your choice (an integer): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out choice))
                {
                    choosen = int.Parse(input);
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }
            }
            Program program = new Program(choosen);
            program.Run();
        }
    }
}