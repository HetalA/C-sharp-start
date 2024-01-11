using System.Collections;
namespace three
{
    public class Employee
    {
        public int Empid {get; set;}
        public string Name {get; set;}
        public float Salary {get; set;}
        public Employee(){}
        public Employee(int id,string name,float sal)
        {
            Empid = id;
            Name = name;
            Salary = sal;
        }
        public override string ToString()
        {
            return Name + " (" + Empid + ")" + " : " + Salary;
        }
    }
    class arraylistseg
    {
        public static void main()
        {
            List<Employee> emp = new List<Employee>();
            emp.Add(new Employee(1,"Ronit",90000));
            emp.Add(new Employee(2,"Hetal",80000));
            emp.Add(new Employee(3,"Samar",70000));
            
            foreach(var item in emp)
            {
                Console.WriteLine(item);
            }
            
        }
    }
}
