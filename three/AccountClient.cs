using System.Collections;
namespace three{
    class InsufficientException : ApplicationException
    {
        public InsufficientException(string msg): base(msg){}
    }
    class InvalidAccountException : ApplicationException
    {
        public InvalidAccountException(string msg): base(msg){}
    }
    class BankRepository : IBankRepository
    {
        int transactionIdCounter = 0;
        List<SBAccount> accounts = new List<SBAccount>();
        List<SBTransaction> transactions = new List<SBTransaction>();
        public void NewAccount(SBAccount acc)
        {
            accounts.Add(acc);
        }
        public List<SBAccount> GetAllAccounts()
        {
            return accounts.ToList();
        }
        public SBAccount GetAccountDetails(int accno)
        {
            return accounts.FirstOrDefault(acc => acc.AccountNo == accno);
        }
        public void DepositAmount(int accno, float amt)
        {
            SBAccount account = GetAccountDetails(accno);
            if (account != null)
            {
                account.CurrBalance += amt;
                RecordTransaction(accno, amt, "Deposit");
            }
            else
            throw new InvalidAccountException("Account with entered Account Number not found.");
        }
        public void WithdrawAmount(int accno, float amt)
        {
            SBAccount account = GetAccountDetails(accno);
            if (account != null)
            {
                if (account.CurrBalance >= amt)
                {
                    account.CurrBalance -= amt;
                    RecordTransaction(accno, amt, "Withdrawal");
                }
                else
                throw new InsufficientException("Insufficient funds.");                
            }
            else
            throw new InvalidAccountException("Account with entered Account Number not found.");
        }
        public List<SBTransaction> GetTransactions(int accno)
        {
            return transactions.Where(t => t.Account == accno).ToList();
        }
        private void RecordTransaction(int accno, float amt, string transactionType)
        {
            SBTransaction transaction = new SBTransaction(transactionIdCounter++, DateTime.Now, accno, amt, transactionType);
            transactions.Add(transaction);
        }

        public static void Main()
        {
            IBankRepository bankrepo = new BankRepository();
            string ch;
            do{
            Console.WriteLine("Enter a choice : ");
            int choice = Convert.ToInt32(Console.ReadLine());
            if(choice==1)
            {
                SBAccount entry = new SBAccount();
                Console.WriteLine("Enter account number : ");
                entry.AccountNo = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter customer name : ");
                entry.CustomerName = Console.ReadLine();
                Console.WriteLine("Enter customer address : ");
                entry.CustomerAddress = Console.ReadLine();
                entry.CurrBalance = (float)0;
                bankrepo.NewAccount(entry);
            }
            else if(choice==2)
            {
                Console.WriteLine("Enter account no.:");
                int acno = Convert.ToInt32(Console.ReadLine());
                SBAccount entry = bankrepo.GetAccountDetails(acno);
                Console.WriteLine("Account {0} : {1} residing at {2}. Current balance is {3}",entry.AccountNo,entry.CustomerName,entry.CustomerAddress,entry.CurrBalance);
            }
            else if(choice==3)
            {
                List<SBAccount> ss = bankrepo.GetAllAccounts();
                foreach(var entry in ss)
                {
                    Console.WriteLine("Account {0} : {1} residing at {2}. Current balance is {3}",entry.AccountNo,entry.CustomerName,entry.CustomerAddress,entry.CurrBalance);
                }
            }
            else if(choice==4)
            {
                Console.WriteLine("Enter account no.:");
                int acno = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter amount to be deposited:");
                int amt = Convert.ToInt32(Console.ReadLine());
                bankrepo.DepositAmount(acno,amt);
            }
            else if(choice==5)
            {
                Console.WriteLine("Enter account no.:");
                int acno = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter amount to be withdrawn:");
                int amt = Convert.ToInt32(Console.ReadLine());
                bankrepo.WithdrawAmount(acno,amt);
            }
            else if(choice==6)
            {
                Console.WriteLine("Enter account no.:");
                int acno = Convert.ToInt32(Console.ReadLine());
                List<SBTransaction> tr = bankrepo.GetTransactions(acno);
                foreach(var entry in tr)
                {
                    Console.WriteLine("Account {0} => ID {1} on {2} of {3}. Type : {4}",entry.Account,entry.TransactionID,entry.TransactionDate,entry.Amount,entry.TransactionType);
                }
            }
            else
            {
                Console.WriteLine("Invalid Choice Chosen!");
            }
            Console.WriteLine("Enter y if you wish to continue :");
            ch = Console.ReadLine(); 
            }while(ch=="y");
        } 
    }
}
