﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.StructuralPatterns
{
    /// <summary>
    /// § Adapter Pattern
    /// Convert the interface of a class into another interface clients expect. Adapter lets 
    /// classes work together that couldn't otherwise because of incompatible interfaces.
    /// i.e. "Adapter" as the name suggests is the object which lets two mutually incompatible 
    /// interfaces communicate with each other.
    /// For example:
    /// Your laptop charger which you bought in US has flattish pins which easily gets hooked 
    /// into electrical sockets in US. But when you travel to European countries you may have 
    /// round holes in the electrical sockets. What do you do then?-Simple buy socket 
    /// adapters/converters for that.
    /// 
    /// § Synonym
    /// Adapter design pattern is also called Wrapper pattern, Translator pattern.
    /// 
    /// § Use
    /// It can be used to covert entity object to domain object or vice-versa.
    /// For example: AutoMapper
    /// 
    /// § Participants
    /// ► Target(ChemicalCompound)
    ///   1. Defines the domain-specific interface that Client uses.
    /// ► Adapter(Compound)
    ///   1. Adapts the interface Adaptee to the Target interface.
    /// ► Adaptee(ChemicalDatabank)
    ///   1. Defines an existing interface that needs adapting.
    /// ► Client(AdapterApp)
    ///   1. Collaborates with objects conforming to the Target interface.
    /// </summary>
    public static class AdapterPattern
    {
        /// <summary>
        /// This real-world code demonstrates the use of a legacy chemical 
        /// databank. Chemical compound objects access the databank through 
        /// an Adapter interface.
        /// </summary>
        public static void Compute()
        {
            Console.WriteLine("\n\nAdapter Pattern\n");
            // Non-adapted chemical compound
            Compound unknown = new Compound("Unknown");
            unknown.Display();

            // Adapted chemical compounds
            Compound water = new RichCompound("Water");
            water.Display();

            Compound benzene = new RichCompound("Benzene");
            benzene.Display();

            Compound ethanol = new RichCompound("Ethanol");
            ethanol.Display();
        }

        /// <summary>
        /// The 'Target' class
        /// </summary>
        class Compound
        {
            protected string _chemical;
            protected float _boilingPoint;
            protected float _meltingPoint;
            protected double _molecularWeight;
            protected string _molecularFormula;

            // Constructor
            public Compound(string chemical)
            {
                this._chemical = chemical;
            }

            public virtual void Display()
            {
                Console.WriteLine("\nCompound: {0} ", _chemical);
            }
        }

        /// <summary>
        /// The 'Adapter' class
        /// </summary>
        class RichCompound : Compound
        {
            private ChemicalDatabank _bank;

            // Constructor
            public RichCompound(string name)
              : base(name)
            {
            }

            public override void Display()
            {
                // The Adaptee
                _bank = new ChemicalDatabank();

                _boilingPoint = _bank.GetCriticalPoint(_chemical, "B");
                _meltingPoint = _bank.GetCriticalPoint(_chemical, "M");
                _molecularWeight = _bank.GetMolecularWeight(_chemical);
                _molecularFormula = _bank.GetMolecularStructure(_chemical);

                base.Display();
                Console.WriteLine("\tFormula: {0}", _molecularFormula);
                Console.WriteLine("\tWeight : {0}", _molecularWeight);
                Console.WriteLine("\tMelting Pt: {0}", _meltingPoint);
                Console.WriteLine("\tBoiling Pt: {0}", _boilingPoint);
            }
        }

        /// <summary>
        /// The 'Adaptee' class
        /// </summary>
        class ChemicalDatabank
        {
            // The databank 'legacy API'
            public float GetCriticalPoint(string compound, string point)
            {
                // Melting Point
                if (point == "M")
                {
                    switch (compound.ToLower())
                    {
                        case "water": return 0.0f;
                        case "benzene": return 5.5f;
                        case "ethanol": return -114.1f;
                        default: return 0f;
                    }
                }
                // Boiling Point
                else
                {
                    switch (compound.ToLower())
                    {
                        case "water": return 100.0f;
                        case "benzene": return 80.1f;
                        case "ethanol": return 78.3f;
                        default: return 0f;
                    }
                }
            }

            public string GetMolecularStructure(string compound)
            {
                switch (compound.ToLower())
                {
                    case "water": return "H20";
                    case "benzene": return "C6H6";
                    case "ethanol": return "C2H5OH";
                    default: return "";
                }
            }

            public double GetMolecularWeight(string compound)
            {
                switch (compound.ToLower())
                {
                    case "water": return 18.015;
                    case "benzene": return 78.1134;
                    case "ethanol": return 46.0688;
                    default: return 0d;
                }
            }
        }
    }
}
