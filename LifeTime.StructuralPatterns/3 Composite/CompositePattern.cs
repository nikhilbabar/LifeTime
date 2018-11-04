using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.StructuralPatterns
{
    /// <summary>
    /// § Composite Pattern
    /// Compose objects into tree structures to represent part-whole hierarchies. Composite lets 
    /// clients treat individual objects and compositions of objects uniformly.
    /// 
    /// § Use
    /// It can be used to create object sub-hierarchies and integrate into main hierarchy by using 
    /// tree components.
    /// 
    /// § Participants
    /// The classes and objects participating in this pattern are:
    /// ► Component (DrawingElement)
    ///   → Declares the interface for objects in the composition.
    ///   → Implements default behavior for the interface common to all classes, as appropriate.
    ///   → Declares an interface for accessing and managing its child components.
    ///   → (optional) Defines an interface for accessing a component's parent in the recursive 
    ///     structure, and implements it if that's appropriate.
    /// ► Leaf (PrimitiveElement)
    ///   → Represents leaf objects in the composition. A leaf has no children.
    ///   → Defines behavior for primitive objects in the composition.
    /// ► Composite (CompositeElement)
    ///   → Defines behavior for components having children.
    ///   → Stores child components.
    ///   → Implements child-related operations in the Component interface.
    /// ► Client (CompositeApp)
    ///   → Manipulates objects in the composition through the Component interface.    
    /// </summary>
    public static class CompositePattern
    {
        /// <summary>
        /// This real-world code demonstrates the Composite pattern used in building a graphical 
        /// tree structure made up of primitive nodes (lines, circles, etc) and composite nodes 
        /// (groups of drawing elements that make up more complex elements).
        /// </summary>
        public static void Compute()
        {
            Console.WriteLine("\n\nComposite Pattern\n");

            // Create a tree structure 
            CompositeElement root = new CompositeElement("Picture");
            root.Add(new PrimitiveElement("Red Line"));
            root.Add(new PrimitiveElement("Blue Circle"));
            root.Add(new PrimitiveElement("Green Box"));

            // Create a branch
            CompositeElement comp = new CompositeElement("Two Circles");
            comp.Add(new PrimitiveElement("Black Circle"));
            comp.Add(new PrimitiveElement("White Circle"));
            root.Add(comp);

            // Add and remove a PrimitiveElement
            PrimitiveElement pe = new PrimitiveElement("Yellow Line");
            root.Add(pe);
            root.Remove(pe);

            // Recursively display nodes
            root.Display(1);
        }
    }

    /// <summary>
    /// The 'Component' Tree node
    /// </summary>
    abstract class DrawingElement
    {
        protected string _name;

        // Constructor
        public DrawingElement(string name)
        {
            this._name = name;
        }

        public abstract void Add(DrawingElement d);
        public abstract void Remove(DrawingElement d);
        public abstract void Display(int indent);
    }

    /// <summary>
    /// The 'Leaf' class
    /// </summary>
    class PrimitiveElement : DrawingElement
    {
        // Constructor
        public PrimitiveElement(string name)
          : base(name)
        {
        }

        public override void Add(DrawingElement c)
        {
            Console.WriteLine("Cannot add to a PrimitiveElement");
        }

        public override void Remove(DrawingElement c)
        {
            Console.WriteLine("Cannot remove from a PrimitiveElement");
        }

        public override void Display(int indent)
        {
            Console.WriteLine(new String('-', indent) + " " + _name);
        }
    }

    /// <summary>
    /// The 'Composite' class
    /// </summary>
    class CompositeElement : DrawingElement
    {
        private List<DrawingElement> elements = new List<DrawingElement>();

        // Constructor
        public CompositeElement(string name)
          : base(name)
        {
        }

        public override void Add(DrawingElement d)
        {
            elements.Add(d);
        }

        public override void Remove(DrawingElement d)
        {
            elements.Remove(d);
        }

        public override void Display(int indent)
        {
            Console.WriteLine(new String('-', indent) + "+ " + _name);

            // Display each child element on this node
            foreach (DrawingElement d in elements)
            {
                d.Display(indent + 2);
            }
        }
    }
}
