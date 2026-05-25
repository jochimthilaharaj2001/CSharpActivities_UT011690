using System;
using System.Collections.Generic;

namespace BankingApplication
{
    public class BankAccount
    {
        public string BankName { get; private set; }
        public string AccountNumber { get; private set; }
        public string UserName { get; private set; }
        public decimal Balance { get; private set; }

        private List<string> transactions = new List<string>();

        public BankAccount(string bankName, string accountNumber, string userName, decimal openingBalance)
        {
            BankName = bankName;
            AccountNumber = accountNumber;
            UserName = userName;
            Balance = openingBalance;

            transactions.Add($"Account opened with Rs.{openingBalance:F2} on {DateTime.Now}");
        }

        public void ShowAccountDetails()
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("                   View Account");
            Console.WriteLine("==================================================");
            Console.WriteLine($"Bank: {BankName}");
            Console.WriteLine($"Account Holder: {UserName}");
            Console.WriteLine($"Account Number: {AccountNumber}");
            Console.WriteLine($"Current Balance: Rs.{Balance:F2}");
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("                     Deposit");
                Console.WriteLine("==================================================");
                Console.WriteLine($"Deposited Rs.{amount:F2}. New Balance: Rs.{Balance:F2}");
                transactions.Add($"Deposited Rs.{amount:F2} on {DateTime.Now}");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("                     Deposit");
                Console.WriteLine("==================================================");
                Console.WriteLine("Deposit amount must be positive.");
            }
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("                   Withdrawal");
                Console.WriteLine("==================================================");
                Console.WriteLine("Withdrawal amount must be greater than zero.");
                return false;
            }
            else if (amount > Balance)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("                   Withdrawal");
                Console.WriteLine("==================================================");
                Console.WriteLine($"Insufficient funds! Available balance: Rs.{Balance:F2}");
                return false;
            }
            else
            {
                Balance -= amount;
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("                   Withdrawal");
                Console.WriteLine("==================================================");
                Console.WriteLine($"Withdrew Rs.{amount:F2}. New Balance: Rs.{Balance:F2}");
                transactions.Add($"Withdrew Rs.{amount:F2} on {DateTime.Now}");
                return true;
            }
        }

        public void ShowTransactions()
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("              Transaction History");
            Console.WriteLine("==================================================");

            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions have been made yet.");
            }
            else
            {
                foreach (string record in transactions)
                {
                    Console.WriteLine(record);
                }
            }
        }
    }

    public class User
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public BankAccount Account { get; private set; }

        public User(string userName, string password, BankAccount account)
        {
            UserName = userName;
            Password = password;
            Account = account;
        }
    }

    internal class Program
    {
        static List<User> users = new List<User>();
        static User loggedInUser = null;

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("        Welcome To Our BOC Banking Portal");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.WriteLine("==================================================");
                Console.Write("Choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid choice. Please enter 1–3.");
                    Pause();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Register();
                        break;
                    case 2:
                        Login();
                        if (loggedInUser != null)
                        {
                            AccountMenu();
                        }
                        break;
                    case 3:
                        running = false;
                        Console.Clear();
                        Console.WriteLine("Thank you for using BOC Banking Portal!");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                if (running) Pause();
            }
        }

        static void Register()
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("                      SignUp");
            Console.WriteLine("==================================================");
            Console.Write("Enter Username: ");
            string userName = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            
            foreach (var user in users)
            {
                if (user.UserName == userName)
                {
                    Console.WriteLine("Username already exists. Try another.");
                    return;
                }
            }

            string accountNumber = Guid.NewGuid().ToString().Substring(0, 10);
            BankAccount newAccount = new BankAccount("BOC", accountNumber, userName, 1000); 
            User newUser = new User(userName, password, newAccount);

            users.Add(newUser);
            Console.WriteLine($"Registration successful! Your account number is {accountNumber}");
        }

        static void Login()
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("                      Login");
            Console.WriteLine("==================================================");
            Console.Write("Enter Username: ");
            string userName = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            foreach (var user in users)
            {
                if (user.UserName == userName && user.Password == password)
                {
                    loggedInUser = user;
                    Console.Clear();
                    Console.WriteLine($"Login successful! Welcome {loggedInUser.UserName}");
                    return;
                }
            }

            Console.WriteLine("Login failed. Invalid username or password.");
        }

        static void AccountMenu()
        {
            bool loggedIn = true;
            while (loggedIn)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine($" Welcome {loggedInUser.UserName} - BOC Banking Portal");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. View Account");
                Console.WriteLine("2. Check Balance");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Withdraw");
                Console.WriteLine("5. View Transactions");
                Console.WriteLine("6. Logout");
                Console.WriteLine("==================================================");
                Console.Write("Choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid choice. Please enter 1–6.");
                    Pause();
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        loggedInUser.Account.ShowAccountDetails();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("==================================================");
                        Console.WriteLine("                Account Balance");
                        Console.WriteLine("==================================================");
                        Console.WriteLine($"Balance: Rs.{loggedInUser.Account.Balance:F2}");
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("==================================================");
                        Console.WriteLine("                     Deposit");
                        Console.WriteLine("==================================================");
                        Console.Write("Enter deposit amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal deposit))
                            loggedInUser.Account.Deposit(deposit);
                        else
                            Console.WriteLine("Invalid input.");
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("==================================================");
                        Console.WriteLine("                   Withdrawal");
                        Console.WriteLine("==================================================");
                        Console.Write("Enter withdrawal amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal withdrawal))
                        {
                            bool success = loggedInUser.Account.Withdraw(withdrawal);
                            if (!success)
                                Console.WriteLine("Transaction failed. Please try again.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid input.");
                        }
                        break;
                    case 5:
                        loggedInUser.Account.ShowTransactions();
                        break;
                    case 6:
                        loggedIn = false;
                        loggedInUser = null;
                        Console.WriteLine("Logged out successfully.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                if (loggedIn) Pause();
            }
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
