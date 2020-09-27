using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unit_4_Modify;
using Unit_4_Origin;

namespace Unit_4_Origin
{
    public class CheckingAccount
    {
        private int transferLimit = 100;
        public Transfer MakeTransfer(string counterAccount, Money amount)
        {
            if (amount.GreaterThan(this.transferLimit))
            {
                throw new BusinessException("Limit exceeded !");
            }
            // 2. 假設結果是9位數的銀行帳號,驗證 11-test:
            int sum = 0;
            for (int i = 0; i < counterAccount.Length; i++)
            {
                sum = sum + (9 - i) * (int)Char.GetNumericValue(
                    counterAccount[i]);
            }
            if (sum % 11 == 0)
            {
                // 3. 查詢轉入帳號並且建立轉帳物件: 
                CheckingAccount acct = Accounts.FindAcctByNumber(counterAccount);
                Transfer result = new Transfer(this, acct, amount);
                return result;
            }
            else
            {
                throw new BusinessException("Invalid account number!");
            }
        }
    }

    public class SavingsAccount
    {
        public CheckingAccount RegisteredCounterAccount { get; set; }
        public Transfer makeTransfer(string counterAccount, Money amount)
        {
            // 1. 假設結果是九位數的銀行帳號，使用11-test進行驗證
            int sum = 0;
            for (int i = 0; i < counterAccount.Length; i++)
            {
                sum = sum + (9 - i) * (int)Char.GetNumericValue(
                    counterAccount[i]);
            }
            if (sum % 11 == 0)
            {
                // 2. 查詢轉入帳號並且建立轉帳物件: 
                CheckingAccount acct = Accounts.FindAcctByNumber(counterAccount);
                Transfer result = new Transfer(this, acct, amount);
                // 3. 檢查提款方是否為已經註冊的轉入帳號: 
                if (result.CounterAccount.Equals(this.RegisteredCounterAccount))
                {
                    return result;
                }
                else
                {
                    throw new BusinessException("Counter-account not registered!");
                }
            }
            else
            {
                throw new BusinessException("Invalid account number!!");
            }
        }
    }

    public class RepeatProgram
    {
        private string givenName;
        private string familyName;

        private float pageWidthInCm;

        public void SetGivenName(string givenName)
        {
            this.givenName = givenName;
        }

        public void SetFamilyName(string familyName)
        {
            this.familyName = familyName;
        }

        public void SetPageWidthInInches(float newWidth)
        {
            float cmPerInch = 2.54f;
            this.pageWidthInCm = newWidth * cmPerInch;
        }

        public void SetPageWidthInPoints(float newWidth)
        {

            float cmPerPoint = 0.0352777f;
            this.pageWidthInCm = newWidth * cmPerPoint;
        }

    }

    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
        }
    }

    public struct Money
    {
        public decimal Value;
        public bool GreaterThan(int value) => this.Value > value;
    }

    public struct Transfer
    {
        public CheckingAccount CounterAccount;
        public Transfer(CheckingAccount from, CheckingAccount counterAccount, Money amount)
        {
            this.CounterAccount = counterAccount;
        }

        public Transfer(SavingsAccount from, CheckingAccount counterAccount, Money amount)
        {
            this.CounterAccount = counterAccount;
        }

        public Transfer(Account from, CheckingAccount counterAccount, Money amount)
        {
            this.CounterAccount = counterAccount;
        }
    }

    public class Accounts
    {
        public static CheckingAccount FindAcctByNumber(string accountNumber) => null;



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
}

