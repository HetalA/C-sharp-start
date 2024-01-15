using bankcore.Models;
namespace bankcore;
class Test
{
    private static Ace52024Context ctx = new Ace52024Context();
    public static void Main()
    {
        int choice;
        string ch;
        do{
            Console.WriteLine("Enter your choice : ");
            Console.WriteLine("1. Create an account");
            Console.WriteLine("2. Show all existing accounts");
            Console.WriteLine("3. Show my account");
            Console.WriteLine("4. Deposit");
            Console.WriteLine("5. Withdraw");
            Console.WriteLine("6. Show my transactions");
            choice = Convert.ToInt32(Console.ReadLine());
            if(choice==1)
            {
                NewAccount();
            }
            else if(choice==2)
            {
                GetAllAccounts();
            }
            else if(choice==3)
            {
                GetAccountDetails();
            }
            else if(choice==4)
            {
                Deposit();
            }
            else if(choice==5)
            {
                Withdraw();
            }
            else if(choice==6)
            {
                ShowTransactions();
            }
            Console.WriteLine("Enter y if you wish to continue :");
            ch = Console.ReadLine();
        }while(ch=="y");
    }

    public static void NewAccount()
    {
        HetalSbaccount hr = new HetalSbaccount();
        Console.WriteLine("Enter Customer Name : ");
        hr.CustomerName = Console.ReadLine();
        Console.WriteLine("Enter Customer Address : ");
        hr.CustomerAddress = Console.ReadLine();
        hr.Balance = 0;
        ctx.HetalSbaccounts.Add(hr);
        ctx.SaveChanges();
        Console.WriteLine("Changes saved.");
    }
    public static void GetAllAccounts()
    {
        foreach(var item in ctx.HetalSbaccounts)
        {
            Console.WriteLine("{0} {1} - {2}",item.AccountNo,item.CustomerName,item.Balance);
        }
    }
    public static void GetAccountDetails()
    {
        Console.WriteLine("Enter your account number");
        int accno = Convert.ToInt32(Console.ReadLine());
        //HetalSbaccount accounts = new HetalSbaccount();
        var item = ctx.HetalSbaccounts.FirstOrDefault(acc => acc.AccountNo == accno);
        Console.WriteLine("{0} {1} - {2}",item.AccountNo,item.CustomerName,item.Balance);
    }
    public static void RecordTransaction(DateTime dt, int accno, float amt, string type)
    {
        HetalSbtransaction hr = new HetalSbtransaction();
        hr.TransactionDate = dt;
        hr.AccountNo = accno;
        hr.Amount = amt;
        hr.TransactionType = type;
        ctx.HetalSbtransactions.Add(hr);
        ctx.SaveChanges();
        Console.WriteLine("Changes saved.");
    }
    public static void Deposit()
    {
        HetalSbaccount hr = new HetalSbaccount();
        HetalSbtransaction hrn = new HetalSbtransaction();
        Console.WriteLine("Enter your account number");
        int accno = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter amount to be deposited");
        float amt = float.parse(Console.ReadLine());
        var item = ctx.HetalSbaccounts.FirstOrDefault(acc => acc.AccountNo == accno);
        item.Balance += amt;
        RecordTransaction(DateTime.Now,accno,amt,"Deposit");
    }
    public static void Withdraw()
    {
        HetalSbaccount hr = new HetalSbaccount();
        HetalSbtransaction hrn = new HetalSbtransaction();
        Console.WriteLine("Enter your account number");
        int accno = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter amount to be withdrawn");
        float amt = float.parse(Console.ReadLine());
        var item = ctx.HetalSbaccounts.FirstOrDefault(acc => acc.AccountNo == accno);
        if(item.Balance<amt)
        {
            Console.WriteLine("Insufficient Balance!");
        }
        else{
            item.Balance -= amt;
            RecordTransaction(DateTime.Now,accno,amt,"Withdawal");
        }
    }
    public static void ShowTransactions()
    {
        Console.WriteLine("Enter your account number");
        int accno = Convert.ToInt32(Console.ReadLine());
        var items = ctx.HetalSbtransactions.Where(t => t.Account == accno).ToList();
        foreach(var item in items)
        {
            Console.WriteLine("{0} {1} - {3} of {2}",item.TransactionID,item.TransactionDate,amt,item.TrancationType);
        }
    }
}
