using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.BehavioralPatterns
{
    /// <summary>
    /// § Memento Pattern
    /// Without violating encapsulation, capture and externalize an object's internal state so 
    /// that the object can be restored to this state later.
    /// 
    /// § Use
    /// Used to restore state of an object to a previous state. 
    /// 
    /// § Participants
    /// The classes and objects participating in this pattern are:
    /// ► Memento(Memento)
    ///   → Stores internal state of the Originator object. The memento may store as much or as little of the originator's internal state as necessary at its originator's discretion.
    ///   → Protect against access by objects of other than the originator.Mementos have effectively two interfaces.Caretaker sees a narrow interface to the Memento -- it can only pass the memento to the other objects.Originator, in contrast, sees a wide interface, one that lets it access all the data necessary to restore itself to its previous state.Ideally, only the originator that produces the memento would be permitted to access the memento's internal state.
    /// ► Originator  (SalesProspect)
    ///   → Creates a memento containing a snapshot of its current internal state.
    ///   → Uses the memento to restore its internal state
    /// ► Caretaker(Caretaker)
    ///   → Is responsible for the memento's safekeeping
    ///   → Never operates on or examines the contents of a memento.
    /// </summary>
    public static class MementoPattern
    {
        /// <summary>
        /// This real-world code demonstrates the Memento pattern which temporarily saves 
        /// and then restores the SalesProspect's internal state.
        /// </summary>
        public static void Compute()
        {
            Console.WriteLine("\n\nMemento Pattern\n");

            SalesProspect element = new SalesProspect();
            element.Name = "Noel van Halen";
            element.Phone = "(412) 256-0990";
            element.Budget = 25000.0;

            // Store internal state
            ProspectMemory memory = new ProspectMemory();
            memory.Memento = element.SaveMemento();

            // Continue changing originator
            element.Name = "Leo Welch";
            element.Phone = "(310) 209-7111";
            element.Budget = 1000000.0;

            // Restore saved state
            element.RestoreMemento(memory.Memento);
        }

        /// <summary>
        /// The 'Originator' class
        /// </summary>
        class SalesProspect
        {
            private string _name;
            private string _phone;
            private double _budget;

            // Gets or sets name
            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value;
                    Console.WriteLine("Name:  " + _name);
                }
            }

            // Gets or sets phone
            public string Phone
            {
                get { return _phone; }
                set
                {
                    _phone = value;
                    Console.WriteLine("Phone: " + _phone);
                }
            }

            // Gets or sets budget
            public double Budget
            {
                get { return _budget; }
                set
                {
                    _budget = value;
                    Console.WriteLine("Budget: " + _budget);
                }
            }

            // Stores memento
            public Memento SaveMemento()
            {
                Console.WriteLine("\nSaving state --\n");
                return new Memento(_name, _phone, _budget);
            }

            // Restores memento
            public void RestoreMemento(Memento memento)
            {
                Console.WriteLine("\nRestoring state --\n");
                this.Name = memento.Name;
                this.Phone = memento.Phone;
                this.Budget = memento.Budget;
            }
        }

        /// <summary>
        /// The 'Memento' class
        /// </summary>
        class Memento
        {
            private string _name;
            private string _phone;
            private double _budget;

            // Constructor
            public Memento(string name, string phone, double budget)
            {
                this._name = name;
                this._phone = phone;
                this._budget = budget;
            }

            // Gets or sets name
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            // Gets or set phone
            public string Phone
            {
                get { return _phone; }
                set { _phone = value; }
            }

            // Gets or sets budget
            public double Budget
            {
                get { return _budget; }
                set { _budget = value; }
            }
        }

        /// <summary>
        /// The 'Caretaker' class
        /// </summary>
        class ProspectMemory
        {
            private Memento _memento;

            // Property
            public Memento Memento
            {
                set { _memento = value; }
                get { return _memento; }
            }
        }
    }
}
