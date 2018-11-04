using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.CreationalPatterns
{
    /// <summary>
    /// § Factory Method Pattern
    /// Define an interface for creating an object, but let subclasses decide which class to instantiate. 
    /// Factory Method lets a class defer instantiation to subclasses.
    /// 
    /// § Use
    /// It can be used to create object in any object creation case.
    /// For example: File reader interface which can have multiple implementation as per file types.
    /// 
    /// § Participants
    /// The classes and objects participating in this pattern are:
    ///     ► Product(Page)
    ///       1. Defines the interface of objects the factory method creates
    ///     ► ConcreteProduct(SkillsPage, EducationPage, ExperiencePage)
    ///       1. Implements the Product interface
    ///     ► Creator  (Document)
    ///       1. Declares the factory method, which returns an object of type Product.Creator may also define 
    ///          a default implementation of the factory method that returns a default ConcreteProduct object.
    ///       2. May call the factory method to create a Product object.
    ///     ► ConcreteCreator  (Report, Resume)
    ///       1. Overrides the factory method to return an instance of a ConcreteProduct.
    ///</summary>
    public static class FactoryMethodPattern
    {
        /// <summary>
        /// This real-world code demonstrates the Factory method offering flexibility in creating different 
        /// documents. The derived Document classes Report and Resume instantiate extended versions of the 
        /// Document class. Here, the Factory Method is called in the constructor of the Document base class.
        /// </summary>
        public static void Compute()
        {
            Console.WriteLine("\n\nFactory Method Pattern\n");

            // Note: constructors call Factory Method
            Document[] documents = new Document[2];

            documents[0] = new Resume();
            documents[1] = new Report();

            // Display document pages
            foreach (Document document in documents)
            {
                Console.WriteLine("\n" + document.GetType().Name + ":");
                foreach (Page page in document.Pages)
                {
                    Console.WriteLine("\t" + page.GetType().Name);
                }
            }
        }
    }

    /// <summary>
    /// The 'Product' abstract class
    /// </summary>
    abstract class Page
    {
    }

    /// <summary>
    /// A 'ConcreteProduct' class
    /// </summary>
    class SkillsPage : Page
    {
    }

    /// <summary>
    /// A 'ConcreteProduct' class
    /// </summary>
    class EducationPage : Page
    {
    }

    /// <summary>
    /// A 'ConcreteProduct' class
    /// </summary>
    class ExperiencePage : Page
    {
    }

    /// <summary>
    /// A 'ConcreteProduct' class
    /// </summary>
    class IntroductionPage : Page
    {
    }

    /// <summary>
    /// A 'ConcreteProduct' class
    /// </summary>
    class ResultsPage : Page
    {
    }

    /// <summary>
    /// A 'ConcreteProduct' class
    /// </summary>
    class ConclusionPage : Page
    {
    }

    /// <summary>
    /// A 'ConcreteProduct' class
    /// </summary>
    class SummaryPage : Page
    {
    }

    /// <summary>
    /// A 'ConcreteProduct' class
    /// </summary>
    class BibliographyPage : Page
    {
    }

    /// <summary>
    /// The 'Creator' abstract class
    /// </summary>
    abstract class Document
    {
        private List<Page> _pages = new List<Page>();

        // Constructor calls abstract Factory method
        public Document()
        {
            this.CreatePages();
        }

        public List<Page> Pages
        {
            get { return _pages; }
        }

        // Factory Method
        public abstract void CreatePages();
    }

    /// <summary>
    /// A 'ConcreteCreator' class
    /// </summary>
    class Resume : Document
    {
        // Factory Method implementation
        public override void CreatePages()
        {
            Pages.Add(new SkillsPage());
            Pages.Add(new EducationPage());
            Pages.Add(new ExperiencePage());
        }
    }

    /// <summary>
    /// A 'ConcreteCreator' class
    /// </summary>
    class Report : Document
    {
        // Factory Method implementation
        public override void CreatePages()
        {
            Pages.Add(new IntroductionPage());
            Pages.Add(new ResultsPage());
            Pages.Add(new ConclusionPage());
            Pages.Add(new SummaryPage());
            Pages.Add(new BibliographyPage());
        }
    }

}
