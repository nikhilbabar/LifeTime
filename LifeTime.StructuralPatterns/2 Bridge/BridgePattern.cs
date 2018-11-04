using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.StructuralPatterns
{
    /// <summary>
    /// § Adapter Pattern
    /// Decouple an abstraction from its implementation so that the two can vary independently.
    /// i.e. This pattern involves an interface which acts as a bridge between the abstraction 
    /// class and implementer classes and also makes the functionality of implementer class 
    /// independent from the abstraction class. Both types of classes can be modified without 
    /// affecting to each other.
    /// For example:
    /// System that is capable of sending Email's and SMS's using a web-service, so what will 
    /// we have in the code? 
    /// An interface that will have a method declaration, to send the Email and SMS, implemented 
    /// by the Email and the SMS concrete classes.
    /// So in this example, sending the SMS and Email notifications to users is our business logic 
    /// and sending it through a Web service, is one of the many different ways to do it.
    /// 
    /// § Use
    /// It can be used to provide pipe implementation 
    /// i.e. Receive from one end and pass it to second end.
    /// 
    /// § Participants
    /// ► Abstraction (BusinessObject)
    ///   1. Defines the abstraction's interface.
    ///   2. Maintains a reference to an object of type Implementer.
    /// ► RefinedAbstraction(CustomersBusinessObject)
    ///   1. Extends the interface defined by Abstraction.
    /// ► Implementer(DataObject)
    ///   1. Defines the interface for implementation classes.This interface doesn't have to correspond 
    ///      exactly to Abstraction's interface; in fact the two interfaces can be quite different. 
    ///      Typically the Implementation interface provides only primitive operations, and Abstraction 
    ///      defines higher-level operations based on these primitives.
    /// ► ConcreteImplementer(CustomersDataObject)
    ///   1. Implements the Implementer interface and defines its concrete implementation.
    /// </summary>
    public static class BridgePattern
    {
        public static void Compute()
        {
            Console.WriteLine("\n\nBridge Pattern\n");

            var messages = new List<SystemMessage>
                                {
                                    new SystemMessage
                                    {
                                        Body = "System Defined Message",
                                        Importance = 1,
                                        Title = "This is system generated message, do not reply it"
                                    },
                                    new UserMessage(new SmsSender())
                                    {
                                        Body = "User Defined Message",
                                        Importance = 3,
                                        UserComments = "Welcome to forum",
                                        Title = "Thanks for joining us. please let me know if any help needed"
                                    }
                                };
            foreach (var message in messages)
            {
                message.Send();
            }

            // Create RefinedAbstraction
            Customers customers = new Customers("Chicago");

            // Set ConcreteImplementer
            customers.Data = new CustomerRegister();

            // Exercise the bridge
            customers.Show();
            customers.Next();
            customers.Show();
            customers.Next();
            customers.Show();
            customers.Insert("Henry Velasquez");

            customers.ShowAll();

        }

        /// <summary>
        /// The 'Abstraction' class
        /// </summary>
        class CustomerColletion
        {
            private DataRegister _dataRegister;
            protected string group;

            public CustomerColletion(string group)
            {
                this.group = group;
            }

            // Property
            public DataRegister Data
            {
                set { _dataRegister = value; }
                get { return _dataRegister; }
            }

            public virtual void Next()
            {
                _dataRegister.NextRecord();
            }

            public virtual void Prev()
            {
                _dataRegister.PriorRecord();
            }

            public virtual void Insert(string customer)
            {
                _dataRegister.InsertRecord(customer);
            }

            public virtual void Delete(string customer)
            {
                _dataRegister.DeleteRecord(customer);
            }

            public virtual void Show()
            {
                _dataRegister.ShowRecord();
            }

            public virtual void ShowAll()
            {
                Console.WriteLine("Customer Group: " + group);
                _dataRegister.ShowRecords();
            }
        }

        /// <summary>
        /// The 'RefinedAbstraction' class
        /// </summary>
        class Customers : CustomerColletion
        {
            // Constructor
            public Customers(string group)
              : base(group)
            {
            }

            public override void ShowAll()
            {
                // Add separator lines
                Console.WriteLine();
                Console.WriteLine("------------------------");
                base.ShowAll();
                Console.WriteLine("------------------------");
            }
        }

        /// <summary>
        /// The 'Implementer' abstract class
        /// </summary>
        abstract class DataRegister
        {
            public abstract void NextRecord();
            public abstract void PriorRecord();
            public abstract void InsertRecord(string name);
            public abstract void DeleteRecord(string name);
            public abstract void ShowRecord();
            public abstract void ShowRecords();
        }

        /// <summary>
        /// The 'ConcreteImplementer' class i.e. Bridge
        /// </summary>
        class CustomerRegister : DataRegister
        {
            private List<string> _customers = new List<string>();
            private int _current = 0;

            public CustomerRegister()
            {
                // Loaded from a database 
                _customers.Add("Jim Jones");
                _customers.Add("Samual Jackson");
                _customers.Add("Allen Good");
                _customers.Add("Ann Stills");
                _customers.Add("Lisa Giolani");
            }

            public override void NextRecord()
            {
                if (_current <= _customers.Count - 1)
                {
                    _current++;
                }
            }

            public override void PriorRecord()
            {
                if (_current > 0)
                {
                    _current--;
                }
            }

            public override void InsertRecord(string customer)
            {
                _customers.Add(customer);
            }

            public override void DeleteRecord(string customer)
            {
                _customers.Remove(customer);
            }

            public override void ShowRecord()
            {
                Console.WriteLine(_customers[_current]);
            }

            public override void ShowRecords()
            {
                foreach (string customer in _customers)
                {
                    Console.WriteLine(" " + customer);
                }
            }
        }

        /// <summary>
        /// The 'Abstraction' class
        /// Abstraction
        /// This will be the abstract class that will have the abstract logic to be implemented.
        /// Most importantly, this will hold a reference to the bridge (that will internally have 
        /// a reference to the system through which notification is to be sent). The rest is just 
        /// like any other interface-based definition of the functions to be implemented.
        /// </summary>
        public class SystemMessage
        {
            protected MessageSenderBase messageSender { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
            public int Importance { get; set; }


            public SystemMessage()
            {
                messageSender = new EmailSender();
            }

            public virtual void Send()
            {
                messageSender.SendMessage(Title, Body, Importance);
            }
        }

        /// <summary>
        /// The 'RefinedAbstraction' class
        /// Abstraction Details
        /// These will be concrete implementations of the abstraction.
        /// </summary>
        public class EmailSender : MessageSenderBase
        {
            public override void SendMessage(string title, string body, int importance)
            {
                Console.WriteLine("Email\n\tTitle: {0}\n\tBody: {1}\n\tPriority: {2}\n", title, body, importance);
            }
        }

        /// <summary>
        /// The 'RefinedAbstraction' class
        /// </summary>
        public class SmsSender : MessageSenderBase
        {
            public override void SendMessage(string title, string body, int importance)
            {
                Console.WriteLine("SMS\n\tTitle: {0}\n\tBody: {1}\n\tPriority: {2}\n", title, body, importance);
            }
        }

        /// <summary>
        /// The 'RefinedAbstraction' class
        /// </summary>
        public class WebServiceSender : MessageSenderBase
        {
            public override void SendMessage(string title, string body, int importance)
            {
                Console.WriteLine("Web Service\n\tTitle: {0}\n\tBody: {1}\n\tPriority: {2}\n", title, body, importance);
            }
        }

        /// <summary>
        /// The 'Implementer' abstract class
        /// Bridge Abstraction
        /// This is the abstract component that will act as a bridge between the two components.
        /// </summary>
        public abstract class MessageSenderBase
        {
            public abstract void SendMessage(string title, string details, int importance);
        }

        /// <summary>
        /// The 'ConcreteImplementer' class
        /// Bridge Implementations
        /// These are the implementations for the bridge and will provide various ways in which 
        /// we can call the required logic implementations
        /// </summary>
        public class UserMessage : SystemMessage
        {
            public string UserComments { get; set; }

            public UserMessage(MessageSenderBase messageSender)
            {
                this.messageSender = messageSender;
            }

            public override void Send()
            {
                string fullBody = string.Format("\n\tBody: {0}\n\tComments: {1}", Body, UserComments);
                messageSender.SendMessage(Title, fullBody, Importance);
            }
        }
    }
}
