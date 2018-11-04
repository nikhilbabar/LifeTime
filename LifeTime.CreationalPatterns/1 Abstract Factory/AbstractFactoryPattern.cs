using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.CreationalPatterns
{
    /// <summary>
    /// § Abstract Factory Pattern
    /// Provide an interface for creating families of related or dependent objects without specifying 
    /// their concrete classes.
    /// 
    /// § Use
    /// It can be used in game development
    /// 
    /// § Participants
    /// The classes and objects participating in this pattern are:
    ///     • AbstractFactory (ContinentFactory)
    ///         o declares an interface for operations that create abstract products
    ///     • ConcreteFactory(AfricaFactory, AmericaFactory)
    ///         o implements the operations to create concrete product objects
    ///     • AbstractProduct(Herbivore, Carnivore)
    ///         o declares an interface for a type of product object
    ///     • Product(Wildebeest, Lion, Bison, Wolf)
    ///         o defines a product object to be created by the corresponding concrete factory
    ///         o implements the AbstractProduct interface
    ///     • Client(AnimalWorld)
    ///         o uses interfaces declared by AbstractFactory and AbstractProduct classes
    /// </summary>
    public static class AbstractFactoryPattern
    {
        /// <summary>
        /// This real-world code demonstrates the creation of different animal worlds for a computer 
        /// game using different factories. Although the animals created by the Continent factories 
        /// are different, the interactions among the animals remain the same.
        /// </summary>
        public static void Compute()
        {
            Console.WriteLine("\n\nAbstract Factory Pattern\n");

            // Create and run the African animal world
            ContinentFactory africa = new AfricaFactory();
            AnimalWorld world = new AnimalWorld(africa);
            world.RunFoodChain();

            // Create and run the American animal world
            ContinentFactory america = new AmericaFactory();
            world = new AnimalWorld(america);
            world.RunFoodChain();
        }
    }

    /// <summary>
    /// The 'AbstractFactory' abstract class
    /// </summary>
    abstract class ContinentFactory
    {
        public abstract Herbivore CreateHerbivore();
        public abstract Carnivore CreateCarnivore();
    }

    /// <summary>
    /// The 'ConcreteFactory1' class
    /// </summary>
    class AfricaFactory : ContinentFactory
    {
        public override Herbivore CreateHerbivore()
        {
            return new Wildebeest();
        }
        public override Carnivore CreateCarnivore()
        {
            return new Lion();
        }
    }

    /// <summary>
    /// The 'ConcreteFactory2' class
    /// </summary>
    class AmericaFactory : ContinentFactory
    {
        public override Herbivore CreateHerbivore()
        {
            return new Bison();
        }
        public override Carnivore CreateCarnivore()
        {
            return new Wolf();
        }
    }

    /// <summary>
    /// The 'AbstractProductA' abstract class
    /// </summary>
    abstract class Herbivore
    {
    }

    /// <summary>
    /// The 'AbstractProductB' abstract class
    /// </summary>
    abstract class Carnivore
    {
        public abstract void Eat(Herbivore h);
    }

    /// <summary>
    /// The 'ProductA1' class
    /// </summary>
    class Wildebeest : Herbivore
    {
    }

    /// <summary>
    /// The 'ProductB1' class
    /// </summary>
    class Lion : Carnivore
    {
        public override void Eat(Herbivore h)
        {
            // Eat Wildebeest
            Console.WriteLine(this.GetType().Name + " eats " + h.GetType().Name);
        }
    }

    /// <summary>
    /// The 'ProductA2' class
    /// </summary>
    class Bison : Herbivore
    {
    }

    /// <summary>
    /// The 'ProductB2' class
    /// </summary>
    class Wolf : Carnivore
    {
        public override void Eat(Herbivore h)
        {
            // Eat Bison
            Console.WriteLine(this.GetType().Name + " eats " + h.GetType().Name);
        }
    }

    /// <summary>
    /// The 'Client' class 
    /// </summary>
    class AnimalWorld
    {
        private Herbivore _herbivore;
        private Carnivore _carnivore;

        // Constructor
        public AnimalWorld(ContinentFactory factory)
        {
            _carnivore = factory.CreateCarnivore();
            _herbivore = factory.CreateHerbivore();
        }

        public void RunFoodChain()
        {
            _carnivore.Eat(_herbivore);
        }
    }
}

