using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.StructuralPatterns
{
    /// <summary>
    /// § Façade Pattern
    /// Provide a unified interface to a set of interfaces in a sub-system. 
    /// Façade defines a higher-level interface that makes the subsystem easier to use.
    /// 
    /// Façade pattern hides the complexities of the system and provides an interface to 
    /// the client using which the client can access the system. This type of design 
    /// pattern comes under structural pattern as this pattern adds an interface to 
    /// existing system to hide its complexities.
    /// 
    /// This pattern involves a single class which provides simplified methods required by 
    /// client and delegates calls to methods of existing system classes.
    /// 
    /// Participants 
    /// The classes and objects participating in this pattern are:
    /// ► Façade (MortgageApplication)
    ///   → Knows which subsystem classes are responsible for a request.
    ///   → Delegates client requests to appropriate subsystem objects.
    /// ► Subsystem classes (Bank, Credit, Loan)
    ///   → Implement subsystem functionality.
    ///   → Handle work assigned by the Façade object.
    ///   → Have no knowledge of the façade and keep no reference to it.
    /// </summary>
    public static class FacadePattern
    {
        /// <summary>
        /// This real-world code demonstrates the Façade pattern as a Mortgage Application 
        /// object which provides a simplified interface to a large subsystem of classes 
        /// measuring the credit worthiness of an applicant.
        /// </summary>
        public static void Compute()
        {
            Console.WriteLine("\n\nFaçade Pattern\n");

             // Façade
            Mortgage mortgage = new Mortgage();

            // Evaluate mortgage eligibility for customer
            Customer customer = new Customer("Ann McKinsey");
            bool eligible = mortgage.IsEligible(customer, 125000);

            Console.WriteLine("\n" + customer.Name + " has been " + (eligible ? "Approved" : "Rejected"));
        }
    }

    /// <summary>
    /// The 'Subsystem ClassA' class
    /// </summary>
    class Bank
    {
        public bool HasSufficientSavings(Customer c, int amount)
        {
            Console.WriteLine("Check bank savings for " + c.Name);
            return true;
        }
    }

    /// <summary>
    /// The 'Subsystem ClassB' class
    /// </summary>
    class Credit
    {
        public bool HasGoodCredit(Customer c)
        {
            Console.WriteLine("Check credit for " + c.Name);
            return true;
        }
    }

    /// <summary>
    /// The 'Subsystem ClassC' class
    /// </summary>
    class Loan
    {
        public bool HasNoBadLoans(Customer c)
        {
            Console.WriteLine("Check loans for " + c.Name);
            return true;
        }
    }

    /// <summary>
    /// Customer class
    /// </summary>
    class Customer
    {
        private string _name;

        // Constructor
        public Customer(string name)
        {
            this._name = name;
        }

        // Gets the name
        public string Name
        {
            get { return _name; }
        }
    }

    /// <summary>
    /// The 'Façade' class
    /// </summary>
    class Mortgage
    {
        private Bank _bank = new Bank();
        private Loan _loan = new Loan();
        private Credit _credit = new Credit();

        public bool IsEligible(Customer cust, int amount)
        {
            Console.WriteLine("{0} applies for {1:C} loan\n",
              cust.Name, amount);

            bool eligible = true;

            // Check credit worthiness of applicant
            if (!_bank.HasSufficientSavings(cust, amount))
            {
                eligible = false;
            }
            else if (!_loan.HasNoBadLoans(cust))
            {
                eligible = false;
            }
            else if (!_credit.HasGoodCredit(cust))
            {
                eligible = false;
            }

            return eligible;
        }
    }
}
