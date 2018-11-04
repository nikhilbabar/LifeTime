using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.BehavioralPatterns
{
    /// <summary>
    /// § Template Method Pattern
    /// Define the skeleton of an algorithm in an operation, deferring some steps to subclasses. Template Method 
    /// lets subclasses redefine certain steps of an algorithm without changing the algorithm's structure.
    /// 
    /// § Use
    /// This can be use to import different types of files as per algorithm in ETL.
    /// 
    /// § Participants
    /// The classes and objects participating in this pattern are:
    /// ► AbstractClass (DataObject)
    ///   → Defines abstract primitive operations that concrete subclasses define to implement steps of an algorithm
    ///   → Implements a template method defining the skeleton of an algorithm. The template method calls primitive 
    ///     operations as well as operations defined in AbstractClass or those of other objects.
    /// ► ConcreteClass (CustomerDataObject)
    ///   → Implements the primitive operations and carry out subclass-specific steps of the algorithm
    /// </summary>
    public static class TemplateMethodPattern
    {
        /// <summary>
        /// This real-world code demonstrates a Template method named Run() which provides a skeleton calling 
        /// sequence of methods. Implementation of these steps are deferred to the CustomerDataObject subclass 
        /// which implements the Connect, Select, Process, and Disconnect methods.
        /// </summary>
        public static void Compute()
        {
            Console.WriteLine("\n\nTemplate Method Pattern\n");

            DataAccessObject accessCategories = new Categories();
            accessCategories.Run();

            DataAccessObject accessProducts = new Products();
            accessProducts.Run();
        }
    }

    /// <summary>
    /// The 'AbstractClass' abstract class
    /// </summary>
    abstract class DataAccessObject
    {
        protected string connectionString;
        protected DataSet dataSet;

        public virtual void Connect()
        {
            connectionString = @"Provider=Microsoft.JET.OLEDB.4.0;Server=.\SQLExpress;AttachDbFilename=D:\Projects\Live\Proof\LifeTime\LifeTime.BehavioralPatterns\Data\Local_GAHP.mdf;Database=Local_GAHP;Trusted_Connection=Yes;";
        }

        public abstract void Select();
        public abstract void Process();

        public virtual void Disconnect()
        {
            connectionString = "";
        }

        // The 'Template Method' 
        public void Run()
        {
            Connect();
            Select();
            Process();
            Disconnect();
        }
    }

    /// <summary>
    /// A 'ConcreteClass' class
    /// </summary>
    class Categories : DataAccessObject
    {
        public override void Select()
        {
            string sql = "SELECT TOP 10 * FROM MDM.DimCategory";
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sql, connectionString);

            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "Categories");
        }

        public override void Process()
        {
            Console.WriteLine("Categories");

            DataTable dataTable = dataSet.Tables["Categories"];
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine(row["CategoryName"]);
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// A 'ConcreteClass' class
    /// </summary>
    class Products : DataAccessObject
    {
        public override void Select()
        {
            string sql = "SELECT TOP 10 * FROM MDM.DimProduct";
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sql, connectionString);

            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "Products");
        }

        public override void Process()
        {
            Console.WriteLine("Products");
            DataTable dataTable = dataSet.Tables["Products"];
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine(row["ProductName"]);
            }
            Console.WriteLine();
        }
    }
}
