using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.StructuralPatterns
{
    /// <summary>
    /// § Flyweight Pattern
    /// Use sharing to support large numbers of fine-grained objects efficiently.
    /// 
    /// Flyweight pattern is primarily used to reduce the number of objects created 
    /// and to decrease memory footprint and increase performance. This pattern provides 
    /// ways to decrease object count thus improving the object structure of application.
    /// 
    /// Flyweight pattern tries to reuse already existing similar kind objects by 
    /// storing them and creates new object when no matching object is found. 
    /// 
    /// § Participants
    /// The classes and objects participating in this pattern are:
    /// ► Flyweight (Character)
    ///   → Declares an interface through which flyweights can receive and act on extrinsic state.
    /// ► ConcreteFlyweight (CharacterA, CharacterB, ..., CharacterZ)
    ///   → Implements the Flyweight interface and adds storage for intrinsic state, if any.
    ///     A ConcreteFlyweight object must be sharable.Any state it stores must be intrinsic, that is, 
    ///     it must be independent of the ConcreteFlyweight object's context.
    /// ► UnsharedConcreteFlyweight (not used)
    ///   → not all Flyweight subclasses need to be shared.The Flyweight interface enables sharing, 
    ///     but it doesn't enforce it. It is common for UnsharedConcreteFlyweight objects to have 
    ///     ConcreteFlyweight objects as children at some level in the flyweight object structure 
    ///     (as the Row and Column classes have).
    /// ► FlyweightFactory (CharacterFactory)
    ///   → creates and manages flyweight objects
    ///   → ensures that flyweight are shared properly.When a client requests a flyweight, the 
    ///     FlyweightFactory objects assets an existing instance or creates one, if none exists.
    /// ► Client (FlyweightApp)
    ///   → maintains a reference to flyweight(s).
    ///   → computes or stores the extrinsic state of flyweight(s).
    /// </summary>
    public static class Flyweight
    {
        /// <summary>
        /// This real-world code demonstrates the Flyweight pattern in which a 
        /// relatively small number of Character objects is shared many times 
        /// by a document that has potentially many characters.
        /// </summary>
        public static void Compute()
        {
            Console.WriteLine("\n\nFlyweight Pattern\n");

            // Build a document with text
            string document = "AAZZBBZB";
            char[] chars = document.ToCharArray();

            CharacterFactory factory = new CharacterFactory();

            // extrinsic state
            int pointSize = 10;

            // For each character use a flyweight object
            foreach (char c in chars)
            {
                pointSize++;
                Character character = factory.GetCharacter(c);
                character.Display(pointSize);
            }
        }

        /// <summary>
        /// The 'FlyweightFactory' class
        /// </summary>
        class CharacterFactory
        {
            private Dictionary<char, Character> _characters = new Dictionary<char, Character>();

            public Character GetCharacter(char key)
            {
                // Uses "lazy initialization"
                Character character = null;
                if (_characters.ContainsKey(key))
                {
                    character = _characters[key];
                }
                else
                {
                    switch (key)
                    {
                        case 'A': character = new CharacterA(); break;
                        case 'B': character = new CharacterB(); break;
                        //...
                        case 'Z': character = new CharacterZ(); break;
                    }
                    _characters.Add(key, character);
                }
                return character;
            }
        }

        /// <summary>
        /// The 'Flyweight' abstract class
        /// </summary>
        abstract class Character
        {
            protected char symbol;
            protected int width;
            protected int height;
            protected int ascent;
            protected int descent;
            protected int pointSize;

            public abstract void Display(int pointSize);
        }

        /// <summary>
        /// A 'ConcreteFlyweight' class
        /// </summary>
        class CharacterA : Character
        {
            // Constructor
            public CharacterA()
            {
                this.symbol = 'A';
                this.height = 100;
                this.width = 120;
                this.ascent = 70;
                this.descent = 0;
            }

            public override void Display(int pointSize)
            {
                this.pointSize = pointSize;
                Console.WriteLine("\t" + this.symbol + " (point size " + this.pointSize + ")");
            }
        }

        /// <summary>
        /// A 'ConcreteFlyweight' class
        /// </summary>
        class CharacterB : Character
        {
            // Constructor
            public CharacterB()
            {
                this.symbol = 'B';
                this.height = 100;
                this.width = 140;
                this.ascent = 72;
                this.descent = 0;
            }

            public override void Display(int pointSize)
            {
                this.pointSize = pointSize;
                Console.WriteLine("\t" + this.symbol + " (point size " + this.pointSize + ")");
            }

        }

        // ... C, D, E, etc..

        /// <summary>
        /// A 'ConcreteFlyweight' class
        /// </summary>
        class CharacterZ : Character
        {
            // Constructor
            public CharacterZ()
            {
                this.symbol = 'Z';
                this.height = 100;
                this.width = 100;
                this.ascent = 68;
                this.descent = 0;
            }

            public override void Display(int pointSize)
            {
                this.pointSize = pointSize;
                Console.WriteLine("\t" + this.symbol + " (point size " + this.pointSize + ")");
            }
        }
    }
}
