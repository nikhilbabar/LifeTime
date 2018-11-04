using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.CreationalPatterns
{
    /// <summary>
    /// § Builder Pattern
    /// Separate the construction of a complex object from its representation so that the same 
    /// construction process can create different representations.
    /// 
    /// § Use
    /// It can be used in CAD software domain.
    /// 
    /// § Participants
    /// The classes and objects participating in this pattern are:
    ///     • Builder(VehicleBuilder)
    ///         o Specifies an abstract interface for creating parts of a Product object
    ///     • ConcreteBuilder(MotorCycleBuilder, CarBuilder, ScooterBuilder)
    ///         o Constructs and assembles parts of the product by implementing the Builder interface
    ///         o Defines and keeps track of the representation it creates
    ///         o Provides an interface for retrieving the product
    ///     • Director (Shop)
    ///         o Constructs an object using the Builder interface
    ///     • Product (Vehicle)
    ///         o Represents the complex object under construction. ConcreteBuilder builds the 
    ///           product's internal representation and defines the process by which it's assembled
    ///         o Includes classes that define the constituent parts, including interfaces for 
    ///           assembling the parts into the result
    /// </summary>
    public static class BuilderPattern
    {
        /// <summary>
        /// This real-world code demonstrates the Builder pattern in which different vehicles are 
        /// assembled in a step-by-step fashion. The Shop uses VehicleBuilders to construct a variety 
        /// of Vehicles in a series of sequential steps.
        /// </summary>
        public static void Compute()
        {
            Console.WriteLine("\n\nBuilder Pattern\n");

            VehicleBuilder builder;

            // Create shop with vehicle builders
            Shop shop = new Shop();

            // Construct and display vehicles
            builder = new ScooterBuilder();
            shop.Construct(builder);
            builder.Vehicle.Show();

            builder = new CarBuilder();
            shop.Construct(builder);
            builder.Vehicle.Show();

            builder = new MotorCycleBuilder();
            shop.Construct(builder);
            builder.Vehicle.Show();
        }
    }

    /// <summary>
    /// The 'Director' class
    /// </summary>
    class Shop
    {
        // Builder uses a complex series of steps
        public void Construct(VehicleBuilder vehicleBuilder)
        {
            vehicleBuilder.BuildFrame();
            vehicleBuilder.BuildEngine();
            vehicleBuilder.BuildWheels();
            vehicleBuilder.BuildDoors();
        }
    }

    /// <summary>
    /// The 'Builder' abstract class
    /// </summary>
    abstract class VehicleBuilder
    {
        protected Vehicle vehicle;

        // Gets vehicle instance
        public Vehicle Vehicle
        {
            get { return vehicle; }
        }

        // Abstract build methods
        public abstract void BuildFrame();
        public abstract void BuildEngine();
        public abstract void BuildWheels();
        public abstract void BuildDoors();
    }

    /// <summary>
    /// The 'ConcreteBuilder1' class
    /// </summary>
    class MotorCycleBuilder : VehicleBuilder
    {
        public MotorCycleBuilder()
        {
            vehicle = new Vehicle("MotorCycle");
        }

        public override void BuildFrame()
        {
            vehicle["frame"] = "MotorCycle Frame";
        }

        public override void BuildEngine()
        {
            vehicle["engine"] = "500 cc";
        }

        public override void BuildWheels()
        {
            vehicle["wheels"] = "2";
        }

        public override void BuildDoors()
        {
            vehicle["doors"] = "0";
        }
    }


    /// <summary>
    /// The 'ConcreteBuilder2' class
    /// </summary>
    class CarBuilder : VehicleBuilder
    {
        public CarBuilder()
        {
            vehicle = new Vehicle("Car");
        }

        public override void BuildFrame()
        {
            vehicle["frame"] = "Car Frame";
        }

        public override void BuildEngine()
        {
            vehicle["engine"] = "2500 cc";
        }

        public override void BuildWheels()
        {
            vehicle["wheels"] = "4";
        }

        public override void BuildDoors()
        {
            vehicle["doors"] = "4";
        }
    }

    /// <summary>
    /// The 'ConcreteBuilder3' class
    /// </summary>
    class ScooterBuilder : VehicleBuilder
    {
        public ScooterBuilder()
        {
            vehicle = new Vehicle("Scooter");
        }

        public override void BuildFrame()
        {
            vehicle["frame"] = "Scooter Frame";
        }

        public override void BuildEngine()
        {
            vehicle["engine"] = "50 cc";
        }

        public override void BuildWheels()
        {
            vehicle["wheels"] = "2";
        }

        public override void BuildDoors()
        {
            vehicle["doors"] = "0";
        }
    }

    /// <summary>
    /// The 'Product' class
    /// </summary>
    class Vehicle
    {
        private string _vehicleType;
        private Dictionary<string, string> _parts = new Dictionary<string, string>();

        // Constructor
        public Vehicle(string vehicleType)
        {
            _vehicleType = vehicleType;
        }

        // Indexer
        public string this[string key]
        {
            get { return _parts[key]; }
            set { _parts[key] = value; }
        }

        public void Show()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Vehicle Type: {0}", _vehicleType);
            Console.WriteLine("\tFrame : {0}", _parts["frame"]);
            Console.WriteLine("\tEngine : {0}", _parts["engine"]);
            Console.WriteLine("\t#Wheels: {0}", _parts["wheels"]);
            Console.WriteLine("\t#Doors : {0}", _parts["doors"]);
        }
    }
}
