namespace three{

    public interface IBankRepository
    {
        void NewAccount(SBAccount acc);
        List<SBAccount> GetAllAccounts();
        SBAccount GetAccountDetails(int accno);
        void DepositAmount(int accno, float amt);
        void WithdrawAmount(int accno, float amt);
        List<SBTransaction> GetTransactions(int accno);
}
    public class SBAccount
    {
        public int AccountNo {get; set;}
        public string CustomerName {get; set;}
        public string CustomerAddress {get; set;}
        public float CurrBalance {get; set;}
    }
    public class SBTransaction
    {
        public int TransactionID {get; set;}
        public DateTime TransactionDate {get; set;}
        public int Account {get; set;}
        public float Amount {get; set;}
        public string TransactionType {get; set;}
        public SBTransaction(int id,DateTime date,int acno,float amt,string type)
        {
            TransactionID = id;
            TransactionDate = date;
            Account = acno;
            Amount = amt;
            TransactionType = type;
        }
        
    }
}