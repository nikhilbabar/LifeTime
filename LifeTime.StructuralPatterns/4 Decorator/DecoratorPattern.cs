using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.StructuralPatterns
{
    /// <summary>
    /// § Decorator Pattern
    /// Attach additional responsibilities to an object dynamically. Decorators provide 
    /// a flexible alternative to sub classing for extending functionality.
    /// 
    /// Decorator pattern allows a user to add new functionality to an existing object 
    /// without altering its structure. This type of design pattern comes under structural 
    /// pattern as this pattern acts as a wrapper to existing class.
    /// 
    /// This pattern creates a decorator class which wraps the original class and provides 
    /// additional functionality keeping class methods signature intact.
    /// 
    /// We are demonstrating the use of decorator pattern via following example in which 
    /// we will decorate a shape with some color without alter shape class.
    /// 
    /// § Participants
    /// The classes and objects participating in this pattern are:
    /// ► Component (LibraryItem)
    ///   → Defines the interface for objects that can have responsibilities added to 
    ///     them dynamically.
    /// ► ConcreteComponent (Book, Video)
    ///   → Defines an object to which additional responsibilities can be attached.
    /// ► Decorator (Decorator)
    ///   → Maintains a reference to a Component object and defines an interface that 
    ///     conforms to Component's interface.
    /// ► ConcreteDecorator (Borrowable)
    ///   → Adds responsibilities to the component.
    /// </summary>
    public static class DecoratorPattern
    {
        /// <summary>
        /// This real-world code demonstrates the Decorator pattern in which 'borrowable' 
        /// functionality is added to existing library items (books and videos).
        /// </summary>
        public static void Compute()
        {
            Console.WriteLine("\n\nDecorator Pattern\n");

            // Create book
            Book book = new Book("Worley", "Inside ASP.NET", 10);
            book.Display();

            // Create video
            Video video = new Video("Spielberg", "Jaws", 23, 92);
            video.Display();

            // Make video borrowable, then borrow and display
            Console.WriteLine("\n► Making video borrowable:");

            Borrowable borrowvideo = new Borrowable(video);
            borrowvideo.BorrowItem("Nikhil");
            borrowvideo.BorrowItem("Johnny");

            borrowvideo.Display();
        }
    }


    /// <summary>
    /// The 'Component' abstract class
    /// </summary>
    abstract class LibraryItem
    {
        private int _numCopies;

        // Property
        public int NumCopies
        {
            get { return _numCopies; }
            set { _numCopies = value; }
        }

        public abstract void Display();
    }

    /// <summary>
    /// The 'ConcreteComponent' class
    /// </summary>
    class Book : LibraryItem
    {
        private string _author;
        private string _title;

        // Constructor
        public Book(string author, string title, int numCopies)
        {
            this._author = author;
            this._title = title;
            this.NumCopies = numCopies;
        }

        public override void Display()
        {
            Console.WriteLine("\n► Book");
            Console.WriteLine("\tAuthor: {0}", _author);
            Console.WriteLine("\tTitle: {0}", _title);
            Console.WriteLine("\t# Copies: {0}", NumCopies);
        }
    }

    /// <summary>
    /// The 'ConcreteComponent' class
    /// </summary>
    class Video : LibraryItem
    {
        private string _director;
        private string _title;
        private int _playTime;

        // Constructor
        public Video(string director, string title,
          int numCopies, int playTime)
        {
            this._director = director;
            this._title = title;
            this.NumCopies = numCopies;
            this._playTime = playTime;
        }

        public override void Display()
        {
            Console.WriteLine("\n► Video");
            Console.WriteLine("\tDirector: {0}", _director);
            Console.WriteLine("\tTitle: {0}", _title);
            Console.WriteLine("\t# Copies: {0}", NumCopies);
            Console.WriteLine("\tPlaytime: {0}\n", _playTime);
        }
    }

    /// <summary>
    /// The 'Decorator' abstract class
    /// </summary>
    abstract class Decorator : LibraryItem
    {
        protected LibraryItem libraryItem;

        // Constructor
        public Decorator(LibraryItem libraryItem)
        {
            this.libraryItem = libraryItem;
        }

        public override void Display()
        {
            libraryItem.Display();
        }
    }

    /// <summary>
    /// The 'ConcreteDecorator' class
    /// </summary>
    class Borrowable : Decorator
    {
        protected List<string> borrowers = new List<string>();

        // Constructor
        public Borrowable(LibraryItem libraryItem)
          : base(libraryItem)
        {
        }

        public void BorrowItem(string name)
        {
            borrowers.Add(name);
            libraryItem.NumCopies--;
        }

        public void ReturnItem(string name)
        {
            borrowers.Remove(name);
            libraryItem.NumCopies++;
        }

        public override void Display()
        {
            base.Display();

            foreach (string borrower in borrowers)
            {
                Console.WriteLine("\tBorrower: " + borrower);
            }
        }
    }

}
