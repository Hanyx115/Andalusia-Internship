using System;


namespace Assignment1;


    public class BankAccount
    {
        private decimal _balance;
        public decimal Balance { get { return _balance; } }

        public string Owner { get; set; }

        public BankAccount(string owner, decimal initialBalance)
        {
            if (string.IsNullOrWhiteSpace(owner))
            {
                throw new ArgumentException("Owner name cannot be null or whitespace.", nameof(owner));
            }
            if (initialBalance < 0)
            {
                throw new ArgumentException("Initial balance cannot be negative.", nameof(initialBalance));
            }

            Owner = owner;
            _balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive.", nameof(amount));
            }
            _balance += amount;
        }
        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be positive.", nameof(amount));
            }
            if (amount > _balance)
            {
                throw new InvalidOperationException("Insufficient funds for this withdrawal.");
            }
            _balance -= amount;
        }
        protected void setBalance(decimal newbalance)
        {
            _balance = newbalance;

        }
        public virtual string GetAccountType()
        {
            return "Standard";
        }


        
    }