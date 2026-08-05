using System;
namespace Assignment1
{
    class Program
    {
        static void Main()
        {
            BankAccount account1 = new BankAccount("Ahmed", 1000);

            SavingsAccount account2 = new SavingsAccount("Sara", 2000, 0.05m);
            account2.ApplyInterest();

            PremiumSavingsAccount account3 = new PremiumSavingsAccount("Omar", 3000, 0.05m);
            account3.ApplyInterest();

            BankAccount[] accounts =
            {
            account1,
            account2,
            account3
        };

            foreach (BankAccount account in accounts)
            {
                Console.WriteLine($"Owner: {account.Owner}");
                Console.WriteLine($"Type: {account.GetAccountType()}");
                Console.WriteLine($"Balance: {account.Balance:C}");
                Console.WriteLine();
            }

            // This will NOT compile because Balance is read-only.
            // account1.Balance = 5000m;
        }
    }
    }
