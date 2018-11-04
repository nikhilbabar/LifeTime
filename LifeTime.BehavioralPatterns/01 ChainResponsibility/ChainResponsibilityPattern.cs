using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.BehavioralPatterns
{
    /// <summary>
    /// § Chain of Responsibility Pattern
    /// Avoid coupling the sender of a request to its receiver by giving more than one 
    /// object a chance to handle the request. Chain the receiving objects and pass the 
    /// request along the chain until an object handles it.
    /// 
    /// § Participants
    /// The classes and objects participating in this pattern are:
    /// ► Handler(Approver)
    ///   → Defines an interface for handling the requests
    ///   → (optional) Implements the successor link
    /// ► ConcreteHandler(Director, VicePresident, President)
    ///   → Handles requests it is responsible for
    ///   → Can access its successor
    ///   → If the ConcreteHandler can handle the request, it does so; otherwise it forwards 
    ///     the request to its successor
    /// ► Client(ChainApp)
    ///   → Initiates the request to a ConcreteHandler object on the chain
    /// </summary>
    public static class ChainResponsibilityPattern
    {
        /// <summary>
        /// This real-world code demonstrates the Chain of Responsibility pattern in which 
        /// several linked managers and executives can respond to a purchase request or hand 
        /// it off to a superior. Each position has can have its own set of rules which 
        /// orders they can approve.
        /// </summary>
        public static void Compute()
        {
            Console.WriteLine("\n\nChain of Responsibility Pattern\n");

            // Setup Chain of Responsibility
            Approver larry = new Director();
            Approver sammy = new VicePresident();
            Approver tammy = new President();

            larry.SetSuccessor(sammy);
            sammy.SetSuccessor(tammy);

            // Generate and process purchase requests
            Purchase purchase = new Purchase(2034, 350.00, "Assets");
            larry.ProcessRequest(purchase);

            purchase = new Purchase(2035, 32590.10, "Project X");
            larry.ProcessRequest(purchase);

            purchase = new Purchase(2036, 122100.00, "Project Y");
            larry.ProcessRequest(purchase);
        }
    }

    /// <summary>
    /// The 'Handler' abstract class
    /// </summary>
    abstract class Approver
    {
        protected Approver successor;

        public void SetSuccessor(Approver successor)
        {
            this.successor = successor;
        }

        public abstract void ProcessRequest(Purchase purchase);
    }

    /// <summary>
    /// The 'ConcreteHandler' class
    /// </summary>
    class Director : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 10000.0)
            {
                Console.WriteLine("{0} approved request# {1}",
                  this.GetType().Name, purchase.Number);
            }
            else if (successor != null)
            {
                successor.ProcessRequest(purchase);
            }
        }
    }

    /// <summary>
    /// The 'ConcreteHandler' class
    /// </summary>
    class VicePresident : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 25000.0)
            {
                Console.WriteLine("{0} approved request# {1}",
                  this.GetType().Name, purchase.Number);
            }
            else if (successor != null)
            {
                successor.ProcessRequest(purchase);
            }
        }
    }

    /// <summary>
    /// The 'ConcreteHandler' class
    /// </summary>
    class President : Approver
    {
        public override void ProcessRequest(Purchase purchase)
        {
            if (purchase.Amount < 100000.0)
            {
                Console.WriteLine("{0} approved request# {1}",
                  this.GetType().Name, purchase.Number);
            }
            else
            {
                Console.WriteLine("Request# {0} requires an executive meeting!", purchase.Number);
            }
        }
    }

    /// <summary>
    /// Class holding request details
    /// </summary>
    class Purchase
    {
        private int _number;
        private double _amount;
        private string _purpose;

        // Constructor
        public Purchase(int number, double amount, string purpose)
        {
            this._number = number;
            this._amount = amount;
            this._purpose = purpose;
        }

        // Gets or sets purchase number
        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        // Gets or sets purchase amount
        public double Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        // Gets or sets purchase purpose
        public string Purpose
        {
            get { return _purpose; }
            set { _purpose = value; }
        }
    }
}

