using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.BehavioralPatterns
{
    /// <summary>
    /// § Visitor Pattern
    /// Represent an operation to be performed on the elements of an object structure. Visitor lets you 
    /// define a new operation without changing the classes of the elements on which it operates.
    /// 
    /// In Visitor pattern, we use a visitor class which changes the executing algorithm of an element class. 
    /// By this way, execution algorithm of element can vary as and when visitor varies. As per the pattern, 
    /// element object has to accept the visitor object so that visitor object handles the operation on the 
    /// element object.
    /// 
    /// § Participants
    /// The classes and objects participating in this pattern are:
    /// ► Visitor  (Visitor)
    ///   → Declares a Visit operation for each class of ConcreteElement in the object structure.The operation's name and signature identifies the class that sends the Visit request to the visitor. That lets the visitor determine the concrete class of the element being visited. Then the visitor can access the elements directly through its particular interface
    /// ► ConcreteVisitor  (IncomeVisitor, VacationVisitor)
    ///   → Implements each operation declared by Visitor.Each operation implements a fragment of the algorithm defined for the corresponding class or object in the structure.ConcreteVisitor provides the context for the algorithm and stores its local state.This state often accumulates results during the traversal of the structure.
    /// ► Element  (Element)
    ///   → Defines an Accept operation that takes a visitor as an argument.
    /// ► ConcreteElement(Employee)
    ///   → Implements an Accept operation that takes a visitor as an argument
    /// ► ObjectStructure(Employees)
    ///   → Can enumerate its elements
    ///   → May provide a high-level interface to allow the visitor to visit its elements
    ///   → May either be a Composite(pattern) or a collection such as a list or a set
    /// </summary>
    public static class VisitorPattern
    {
        /// <summary>
        /// This real-world code demonstrates the Visitor pattern in which two objects traverse a list of 
        /// Employees and performs the same operation on each Employee. The two visitor objects define different 
        /// operations -- one adjusts vacation days and the other income.
        /// </summary>
        public static void Compute()
        {
            Console.WriteLine("\n\nVisitor Pattern\n");

            // Setup employee collection
            Employees emps = new Employees();
            emps.Attach(new VisClerk());
            emps.Attach(new VisDirector());
            emps.Attach(new VisPresident());

            // Employees are 'visited'
            emps.Accept(new IncomeVisitor());
            emps.Accept(new VacationVisitor());
        }
    }

    /// <summary>
    /// The 'Visitor' interface
    /// </summary>
    interface IVisitor
    {
        void Visit(Element element);
    }

    /// <summary>
    /// A 'ConcreteVisitor' class
    /// </summary>
    class IncomeVisitor : IVisitor
    {
        public void Visit(Element element)
        {
            Employee employee = element as Employee;

            // Provide 10% pay raise
            employee.Income *= 1.10;
            Console.WriteLine("{0} {1}'s new income: {2:C}", employee.GetType().Name, employee.Name, employee.Income);
        }
    }

    /// <summary>
    /// A 'ConcreteVisitor' class
    /// </summary>
    class VacationVisitor : IVisitor
    {
        public void Visit(Element element)
        {
            Employee employee = element as Employee;

            // Provide 3 extra vacation days
            employee.VacationDays += 3;
            Console.WriteLine("{0} {1}'s new vacation days: {2}", employee.GetType().Name, employee.Name, employee.VacationDays);
        }
    }

    /// <summary>
    /// The 'Element' abstract class
    /// </summary>
    abstract class Element
    {
        public abstract void Accept(IVisitor visitor);
    }

    /// <summary>
    /// The 'ConcreteElement' class
    /// </summary>
    class Employee : Element
    {
        private string _name;
        private double _income;
        private int _vacationDays;

        // Constructor
        public Employee(string name, double income, int vacationDays)
        {
            this._name = name;
            this._income = income;
            this._vacationDays = vacationDays;
        }

        // Gets or sets the name
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        // Gets or sets income
        public double Income
        {
            get { return _income; }
            set { _income = value; }
        }

        // Gets or sets number of vacation days
        public int VacationDays
        {
            get { return _vacationDays; }
            set { _vacationDays = value; }
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    /// <summary>
    /// The 'ObjectStructure' class
    /// </summary>
    class Employees
    {
        private List<Employee> _employees = new List<Employee>();

        public void Attach(Employee employee)
        {
            _employees.Add(employee);
        }

        public void Detach(Employee employee)
        {
            _employees.Remove(employee);
        }

        public void Accept(IVisitor visitor)
        {
            foreach (Employee e in _employees)
            {
                e.Accept(visitor);
            }
            Console.WriteLine();
        }
    }

    // Three employee types

    class VisClerk : Employee
    {
        // Constructor
        public VisClerk()
          : base("Hank", 25000.0, 14)
        {
        }
    }

    class VisDirector : Employee
    {
        // Constructor
        public VisDirector()
          : base("Elly", 35000.0, 16)
        {
        }
    }

    class VisPresident : Employee
    {
        // Constructor
        public VisPresident()
          : base("Dick", 45000.0, 21)
        {
        }
    }
}
