using System;
using static Assignment1.BankAccount;

namespace Assignment1
{

	public class SavingsAccount : BankAccount
	{
        public SavingsAccount(string owner, decimal initialBalance, decimal interestRate) : base(owner, initialBalance)
        {
            if (interestRate < 0)
                throw new ArgumentException("Interest rate cannot be negative.");

            InterestRate = interestRate;
        }
        public decimal InterestRate { get; set; }

        public void ApplyInterest()
        {
            decimal interest = Balance * InterestRate;
            setBalance(Balance + interest);
        }
        public override string GetAccountType()
        {
            return "Savings";
        }

    }
}