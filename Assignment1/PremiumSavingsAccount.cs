using System;
namespace Assignment1
{

    public class PremiumSavingsAccount : SavingsAccount
    {
        public PremiumSavingsAccount(string owner, decimal balance, decimal interestRate): base(owner, balance, interestRate)
        {
        }
        public new void ApplyInterest()
        {
            decimal interest = Balance * InterestRate * 2;
            Deposit(interest);
        }
        public override string GetAccountType()
        {
            return "Premium Savings";
        }
    }
	
}
