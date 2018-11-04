using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.BehavioralPatterns
{
    /// <summary>
    /// § Interpreter Pattern
    /// Given a language, define a representation for its grammar along with an interpreter 
    /// that uses the representation to interpret sentences in the language.
    /// 
    /// Interpreter pattern provides a way to evaluate language grammar or expression. This 
    /// pattern involves implementing an expression interface which tells to interpret a 
    /// particular context.
    /// 
    /// § Use
    /// This pattern is used in SQL parsing, symbol processing engine etc..
    /// 
    /// § Participants
    /// The classes and objects participating in this pattern are:
    /// ► AbstractExpression (Expression)
    ///   → Declares an interface for executing an operation
    /// ► TerminalExpression (ThousandExpression, HundredExpression, TenExpression, OneExpression )
    ///   → Implements an Interpret operation associated with terminal symbols in the grammar.
    ///   → An instance is required for every terminal symbol in the sentence.
    /// ► NonterminalExpression (not used)
    ///   → One such class is required for every rule R::= R1R2...Rn in the grammar
    ///   → Maintains instance variables of type AbstractExpression for each of the symbols R1 through Rn.
    ///   → Implements an Interpret operation for nonterminal symbols in the grammar.Interpret typically 
    ///     calls itself recursively on the variables representing R1 through Rn.
    /// ► Context (Context)
    ///   → Contains information that is global to the interpreter
    /// ► Client (InterpreterApp)
    ///   → Builds (or is given) an abstract syntax tree representing a particular sentence in the language 
    ///     that the grammar defines.The abstract syntax tree is assembled from instances of the 
    ///     NonterminalExpression and TerminalExpression classes
    ///   → Invokes the Interpret operation
    /// </summary>
    public static class InterpreterPattern
    {
        /// <summary>
        /// This real-world code demonstrates the Interpreter pattern which is used to convert a Roman 
        /// numeral to a decimal.
        /// </summary>
        public static void Compute()
        {
            Console.WriteLine("\n\nInterpreter Pattern\n");

            string roman = "MCMXXVIII";
            Context context = new Context(roman);

            // Build the 'parse tree'
            List<Expression> tree = new List<Expression>();
            tree.Add(new ThousandExpression());
            tree.Add(new HundredExpression());
            tree.Add(new TenExpression());
            tree.Add(new OneExpression());

            // Interpret
            foreach (Expression exppression in tree)
            {
                exppression.Interpret(context);
            }

            Console.WriteLine("{0} = {1}", roman, context.Output);
        }
    }

    /// <summary>
    /// The 'Context' class
    /// </summary>
    class Context
    {
        private string _input;
        private int _output;

        // Constructor
        public Context(string input)
        {
            this._input = input;
        }

        // Gets or sets input
        public string Input
        {
            get { return _input; }
            set { _input = value; }
        }

        // Gets or sets output
        public int Output
        {
            get { return _output; }
            set { _output = value; }
        }
    }

    /// <summary>
    /// The 'AbstractExpression' class
    /// </summary>
    abstract class Expression
    {
        public void Interpret(Context context)
        {
            if (context.Input.Length == 0)
                return;

            if (context.Input.StartsWith(Nine()))
            {
                context.Output += (9 * Multiplier());
                context.Input = context.Input.Substring(2);
            }
            else if (context.Input.StartsWith(Four()))
            {
                context.Output += (4 * Multiplier());
                context.Input = context.Input.Substring(2);
            }
            else if (context.Input.StartsWith(Five()))
            {
                context.Output += (5 * Multiplier());
                context.Input = context.Input.Substring(1);
            }

            while (context.Input.StartsWith(One()))
            {
                context.Output += (1 * Multiplier());
                context.Input = context.Input.Substring(1);
            }
        }

        public abstract string One();
        public abstract string Four();
        public abstract string Five();
        public abstract string Nine();
        public abstract int Multiplier();
    }

    /// <summary>
    /// A 'TerminalExpression' class
    /// <remarks>
    /// Thousand checks for the Roman Numeral M 
    /// </remarks>
    /// </summary>
    class ThousandExpression : Expression
    {
        public override string One() { return "M"; }
        public override string Four() { return " "; }
        public override string Five() { return " "; }
        public override string Nine() { return " "; }
        public override int Multiplier() { return 1000; }
    }

    /// <summary>
    /// A 'TerminalExpression' class
    /// <remarks>
    /// Hundred checks C, CD, D or CM
    /// </remarks>
    /// </summary>
    class HundredExpression : Expression
    {
        public override string One() { return "C"; }
        public override string Four() { return "CD"; }
        public override string Five() { return "D"; }
        public override string Nine() { return "CM"; }
        public override int Multiplier() { return 100; }
    }

    /// <summary>
    /// A 'TerminalExpression' class
    /// <remarks>
    /// Ten checks for X, XL, L and XC
    /// </remarks>
    /// </summary>
    class TenExpression : Expression
    {
        public override string One() { return "X"; }
        public override string Four() { return "XL"; }
        public override string Five() { return "L"; }
        public override string Nine() { return "XC"; }
        public override int Multiplier() { return 10; }
    }

    /// <summary>
    /// A 'TerminalExpression' class
    /// <remarks>
    /// One checks for I, II, III, IV, V, VI, VI, VII, VIII, IX
    /// </remarks>
    /// </summary>
    class OneExpression : Expression
    {
        public override string One() { return "I"; }
        public override string Four() { return "IV"; }
        public override string Five() { return "V"; }
        public override string Nine() { return "IX"; }
        public override int Multiplier() { return 1; }
    }
}
