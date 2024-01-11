using System.Collections;
namespace three{
    
    class BankRepository : IBankRepository
    {
        /*
        The class should contain a collection of SBAccount class, and another collection of SBTransaction class.
        Use generic List for both.
        - The "New Account" method must add the details of a new account to the SBAccount collection.
        - Given an account number, the "Get Details of an Account" method must return the details of 
        the account. If the account number is not in the list, exception must be thrown.
        - The "Get Details of all Accounts" must return the details of all the accounts.
        - The "Deposit Amount" and "Withdraw Amount" methods must update the current balance in the 
        corresponding account in the SBAccount collection, and add the details of the transaction in 
        the SBTransaction collection.
        - If there is not enough balance in the account, then "Withdraw Amount" method must throw 
        an exception.
        - Given an account number, the "Get Transactions of an Account" should return the details of 
        all the transactions done by the account number.
        */
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
            Console.WriteLine("Account with Account Number {0} not found.", accno);   
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
                Console.WriteLine("Insufficient funds.");
            }
            else
            Console.WriteLine("Account with Account Number {0} not found.",accno);
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