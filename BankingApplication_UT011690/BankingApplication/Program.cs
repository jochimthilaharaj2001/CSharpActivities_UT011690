using System;
using System.Collections.Generic;

namespace BankingApplication
{
    public class BankAccount
    {
        public string BankName { get; private set; }
        public string AccountNumber { get; private set; }
        public string AccountHolder { get; private set; }
        public decimal Balance { get; private set; }

        // Transaction history list
        private List<string> transactions = new List<string>();

        public BankAccount(string bankName, string accountNumber, string accountHolder, decimal openingBalance)
        {
            BankName = bankName;
            AccountNumber = accountNumber;
            AccountHolder = accountHolder;
            Balance = openingBalance;

            transactions.Add($"Account opened with Rs.{openingBalance:F2} on {DateTime.Now}");
        }

        public void ShowAccountDetails()
        {
            Console.WriteLine($"Bank: {BankName}");
            Console.WriteLine($"Account Holder: {AccountHolder}");
            Console.WriteLine($"Account Number: {AccountNumber}");
            Console.WriteLine($"Current Balance: Rs.{Balance:F2}");
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                Console.WriteLine($"Deposited Rs.{amount:F2}. New Balance: Rs.{Balance:F2}");
                transactions.Add($"Deposited Rs.{amount:F2} on {DateTime.Now}");
            }
            else
            {
                Console.WriteLine("Deposit amount must be positive.");
            }
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Withdrawal amount must be greater than zero.");
                return false;
            }
            else if (amount > Balance)
            {
                Console.WriteLine($"Insufficient funds! Available balance: Rs.{Balance:F2}");
                return false;
            }
            else
            {
                Balance -= amount;
                Console.WriteLine($"Withdrew Rs.{amount:F2}. New Balance: Rs.{Balance:F2}");
                transactions.Add($"Withdrew Rs.{amount:F2} on {DateTime.Now}");
                return true;
            }
        }

        public void ShowTransactions()
        {
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

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("        Welcome To Our BOC Banking Portal");
            Console.WriteLine("==================================================");

            Console.Write("Enter Your Name: ");
            string accountHolder = Console.ReadLine();

            Console.Write("Enter Opening Balance: ");
            decimal openingBalance;
            while (!decimal.TryParse(Console.ReadLine(), out openingBalance) || openingBalance < 0)
            {
                Console.WriteLine("Invalid amount. Enter a valid opening balance:");
            }

            BankAccount myAccount = new BankAccount("BOC", "1234567890", accountHolder, openingBalance);

            Console.WriteLine($"Hello, {accountHolder}! Your account has been created with a balance of Rs.{openingBalance:F2}");

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("        Welcome To Our BOC Banking Portal");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. View Account");
                Console.WriteLine("2. Check Balance");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Withdraw");
                Console.WriteLine("5. View Transactions");
                Console.WriteLine("6. Exit");
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
                        myAccount.ShowAccountDetails();
                        break;

                    case 2:
                        Console.WriteLine($"Balance: Rs.{myAccount.Balance:F2}");
                        break;

                    case 3:
                        Console.Write("Enter deposit amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal deposit))
                            myAccount.Deposit(deposit);
                        else
                            Console.WriteLine("Invalid input.");
                        break;

                    case 4:
                        Console.Write("Enter withdrawal amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal withdrawal))
                        {
                            bool success = myAccount.Withdraw(withdrawal);
                            if (!success)
                            {
                                Console.WriteLine("Transaction failed. Please try again.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input.");
                        }
                        break;

                    case 5:
                        myAccount.ShowTransactions();
                        break;

                    case 6:
                        running = false;
                        Console.WriteLine("==================================================");
                        Console.WriteLine(" Thank you for using BOC Banking Portal!");
                        Console.WriteLine("==================================================");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                if (running)
                {
                    Pause();
                }
            }
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }
    }
}
