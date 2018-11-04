using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.BehavioralPatterns
{
    /// <summary>
    /// § Strategy Pattern
    /// Define a family of algorithms, encapsulate each one, and make them interchangeable. Strategy lets 
    /// the algorithm vary independently from clients that use it.
    /// 
    /// Class behavior or its algorithm can be changed at run time.
    /// 
    /// In Strategy pattern, we create objects which represent various strategies and a context object whose 
    /// behavior varies as per its strategy object. The strategy object changes the executing algorithm of 
    /// the context object.
    /// 
    /// § Participants
    /// The classes and objects participating in this pattern are:
    /// ► Strategy(SortStrategy)
    ///   → Declares an interface common to all supported algorithms. Context uses this interface to call the 
    ///     algorithm defined by a ConcreteStrategy
    /// ► ConcreteStrategy (QuickSort, ShellSort, MergeSort)
    ///   → Implements the algorithm using the Strategy interface
    /// ► Context  (SortedList)
    ///   → Is configured with a ConcreteStrategy object
    ///   → Maintains a reference to a Strategy object
    ///   → May define an interface that lets Strategy access its data.
    /// </summary>
    public static class StrategyPattern
    {
        /// <summary>
        /// This real-world code demonstrates the Strategy pattern which encapsulates sorting algorithms in 
        /// the form of sorting objects. This allows clients to dynamically change sorting strategies 
        /// including Quick-sort, Shell-sort, and Merge-sort.
        /// </summary>
        public static void Compute()
        {
            Console.WriteLine("\n\nStrategy Pattern\n");

            // Two contexts following different strategies
            SortedList students = new SortedList();

            students.Add("Samuel");
            students.Add("Jimmy");
            students.Add("Sandra");
            students.Add("Vivek");
            students.Add("Anna");

            students.SetSortStrategy(new QuickSort());
            students.Sort();

            students.SetSortStrategy(new ShellSort());
            students.Sort();

            students.SetSortStrategy(new MergeSort());
            students.Sort();

        }
    }


    /// <summary>
    /// The 'Strategy' abstract class
    /// </summary>
    abstract class SortStrategy
    {
        public abstract void Sort(List<string> list);
    }

    /// <summary>
    /// A 'ConcreteStrategy' class
    /// </summary>
    class QuickSort : SortStrategy
    {
        public override void Sort(List<string> list)
        {
            list.Sort(); // Default is Quicksort
            Console.WriteLine("Quick sorted list ");
        }
    }

    /// <summary>
    /// A 'ConcreteStrategy' class
    /// </summary>
    class ShellSort : SortStrategy
    {
        public override void Sort(List<string> list)
        {
            //list.ShellSort(); not-implemented
            Console.WriteLine("Shell sorted list ");
        }
    }

    /// <summary>
    /// A 'ConcreteStrategy' class
    /// </summary>
    class MergeSort : SortStrategy
    {
        public override void Sort(List<string> list)
        {
            //list.MergeSort(); not-implemented
            Console.WriteLine("Merge sorted list ");
        }
    }

    /// <summary>
    /// The 'Context' class
    /// </summary>
    class SortedList
    {
        private List<string> _list = new List<string>();
        private SortStrategy _sortstrategy;

        public void SetSortStrategy(SortStrategy sortstrategy)
        {
            this._sortstrategy = sortstrategy;
        }

        public void Add(string name)
        {
            _list.Add(name);
        }

        public void Sort()
        {
            _sortstrategy.Sort(_list);

            // Iterate over list and display results
            foreach (string name in _list)
            {
                Console.WriteLine("\t" + name);
            }
            Console.WriteLine();
        }
    }
}
