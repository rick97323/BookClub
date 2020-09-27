using System;
using System.Collections.Generic;
using System.Text;
using Unit_4_Origin;

namespace Unit_4_Modify
{
    public class Account
    {
        public virtual Transfer MakeTransfer(string counterAccount, Money amount)
        {            
            if (IsValid(counterAccount))
            {
                CheckingAccount acct = Accounts.FindAcctByNumber(counterAccount);
                return new Transfer(this, acct, amount);
            }
            else
            {
                throw new BusinessException("Invalid account number!");
            }
        }

        public static bool IsValid(string number)
        {
            int sum = 0;
            for (int i = 0; i < number.Length; i++)
            {
                sum = sum + (9 - i) * (int)Char.GetNumericValue(number[i]);
            }
            return sum % 11 == 0;
        }
    }

    public class CheckingAccount : Account
    {
        private int transferLimit = 100;
        public override Transfer MakeTransfer(string counterAccount, Money amount)
        {
            if (amount.GreaterThan(this.transferLimit))
            {
                throw new BusinessException("Limit exceeded !");
            }
            return base.MakeTransfer(counterAccount, amount);
        }
    }
    public class SavingsAccount : Account
    {
        public CheckingAccount RegisteredCounterAccount { get; set; }

        public override Transfer MakeTransfer(string counterAccount, Money amount)
        {
            Transfer result = base.MakeTransfer(counterAccount, amount);
            if (result.CounterAccount.Equals(this.RegisteredCounterAccount))
            {
                return result;
            }
            else
            {
                throw new BusinessException("Counter-account not registered!");
            }
        }
    }


    public class Accounts
    {
        public static CheckingAccount FindAcctByNumber(string accountNumber) => null;
    }

    public struct Transfer
    {
        public CheckingAccount CounterAccount;

        public Transfer(Account from, CheckingAccount counterAccount, Money amount)
        {
            this.CounterAccount = counterAccount;
        }
    }
}