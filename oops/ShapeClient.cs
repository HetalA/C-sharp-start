namespace oops
{
    class EmployeeClient
    {
        public static void Main()
        {
            Permanent p = new Permanent();
            p.AcceptDetails();
            p.GetDetails();
            p.CalculateSalary();
            p.ShowDetails();

            Trainee t = new Trainee();
            t.AcceptDetails();
            t.GetTraineeDetails();
            t.CalculateSalary();
            t.ShowTraineeDetails();
        }
    }
}