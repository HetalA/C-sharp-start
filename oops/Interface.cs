namespace oops{

    public interface IEmployee
    {
        void AcceptDetails();
        void ShowDetails();
        void CalculateSalary();
    }
    public class Employee : IEmployee
    {
        public int Empid {get; set;}
        public string Name {get; set;}
        public float Salary {get; set;}
        public DateTime Doj {get; set;}
        public void AcceptDetails()
        {
            Console.WriteLine("Enter employee ID : ");
            Empid = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter employee name : ");
            Name = Console.ReadLine();
            Console.WriteLine("Enter employee salary : ");
            Salary = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter employee date of joining : ");
            Doj = Convert.ToDateTime(Console.ReadLine());
        }
        public void DisplayDetails()
        {
            Console.WriteLine("{0} (ID: {1}) joined on {2}. Salary : {3}.",Empid, Name, Doj, Salary);
        }
        public virtual void CalculateSalary()
        {
            Console.WriteLine("Base class");
        }
    }

    class Permanent:Employee{
        public float Basicpay {get; set;}
        public float Da {get; set;}
        public float Hra {get; set;}
        public float Pf {get; set;}
        public void GetDetails()
        {
            Console.WriteLine("Enter basic pay : ");
            Basicpay = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter HRA : ");
            Hra = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter DA : ");
            Da = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter PF : ");
            Pf = float.Parse(Console.ReadLine());
        }
        public override void CalculateSalary()
        {
            Salary = Basicpay+Hra+Da-Pf;
        }
        public void ShowDetails()
        {
            Console.WriteLine("{0} (ID: {1}) joined on {2}. Salary : {3}.",Empid, Name, Doj, Salary);
        }
    }

    class Trainee:Employee{
        public float Bonus {get; set;}
        public string ProjectName {get; set;}
        public void GetTraineeDetails()
        {
            Console.WriteLine("Enter bonus : ");
            Bonus = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter project : ");
            ProjectName = Console.ReadLine();
        }
        public override void CalculateSalary()
        {
            if(ProjectName=="Banking")
            Salary += (float) 0.05*Salary;
            else if(ProjectName=="Insurance")
            Salary += (float) 0.1*Salary;
        }
        public void ShowTraineeDetails()
        {
            Console.WriteLine("{0} (ID: {1}) joined on {2}. Salary : {3}.",Empid, Name, Doj, Salary);
        }
    }
}