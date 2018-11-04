using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.BehavioralPatterns
{
    /// <summary>
    /// § Command Pattern
    /// Encapsulate a request as an object, thereby letting you parameterize clients 
    /// with different requests, queue or log requests, and support undoable operations.
    /// 
    /// A request is wrapped under an object as command and passed to invoker object. 
    /// Invoker object looks for the appropriate object which can handle this command 
    /// and passes the command to the corresponding object which executes the command.
    /// 
    /// § Use
    /// This clearly decouples the request from the invoker. This is more suited for 
    /// scenarios where we implement Redo, Copy, Paste and Undo operations where the 
    /// action is stored as an object. We generally use Menu or Shortcut key gestures 
    /// for any of the preceding actions to be executed.
    /// 
    /// § Participants
    /// The classes and objects participating in this pattern are:
    /// ► Command (Command)
    ///   → Declares an interface for executing an operation
    /// ► ConcreteCommand (CalculatorCommand)
    ///   → Defines a binding between a Receiver object and an action
    ///   → Implements Execute by invoking the corresponding operation(s) on Receiver
    /// ► Client (CommandApp)
    ///   → Creates a ConcreteCommand object and sets its receiver
    /// ► Invoker (User)
    ///   → Asks the command to carry out the request
    /// ► Receiver(Calculator)
    ///   → Knows how to perform the operations associated with carrying out the request.
    /// </summary>
    public static class CommandPattern
    {
        /// <summary>
        /// This real-world code demonstrates the Command pattern used in a simple calculator 
        /// with unlimited number of undo's and redo's. Note that in C#  the word 'operator' 
        /// is a keyword. Prefixing it with '@' allows using it as an identifier.
        /// </summary>
        public static void Compute()
        {
            Console.WriteLine("\n\nCommand Pattern\n");

            // Create user and let her compute
            User user = new User();

            // User presses calculator buttons
            user.Compute('+', 100);
            user.Compute('-', 50);
            user.Compute('*', 10);
            user.Compute('/', 2);

            // Undo 4 commands
            user.Undo(4);

            // Redo 3 commands
            user.Redo(3);
        }


        /// <summary>
        /// The 'Command' abstract class
        /// </summary>
        abstract class Command
        {
            public abstract void Execute();
            public abstract void UnExecute();
        }

        /// <summary>
        /// The 'ConcreteCommand' class
        /// </summary>
        class CalculatorCommand : Command
        {
            private char _operator;
            private int _operand;
            private Calculator _calculator;

            // Constructor
            public CalculatorCommand(Calculator calculator, char @operator, int operand)
            {
                this._calculator = calculator;
                this._operator = @operator;
                this._operand = operand;
            }

            // Gets operator
            public char Operator
            {
                set { _operator = value; }
            }

            // Get operand
            public int Operand
            {
                set { _operand = value; }
            }

            // Execute new command
            public override void Execute()
            {
                _calculator.Operation(_operator, _operand);
            }

            // Un-execute last command
            public override void UnExecute()
            {
                _calculator.Operation(Undo(_operator), _operand);
            }

            // Returns opposite operator for given operator
            private char Undo(char @operator)
            {
                switch (@operator)
                {
                    case '+': return '-';
                    case '-': return '+';
                    case '*': return '/';
                    case '/': return '*';
                    default:
                        throw new ArgumentException("@operator");
                }
            }
        }

        /// <summary>
        /// The 'Receiver' class
        /// </summary>
        class Calculator
        {
            private int _current = 0;

            public void Operation(char @operator, int operand)
            {
                switch (@operator)
                {
                    case '+': _current += operand; break;
                    case '-': _current -= operand; break;
                    case '*': _current *= operand; break;
                    case '/': _current /= operand; break;
                }

                Console.WriteLine("Current value = {0,3} (following {1} {2})", _current, @operator, operand);
            }
        }

        /// <summary>
        /// The 'Invoker' class
        /// </summary>
        class User
        {
            // Initializers
            private Calculator _calculator = new Calculator();
            private List<Command> _commands = new List<Command>();
            private int _current = 0;

            public void Redo(int levels)
            {
                Console.WriteLine("\n---- Redo {0} levels ", levels);
                // Perform redo operations
                for (int i = 0; i < levels; i++)
                {
                    if (_current < _commands.Count - 1)
                    {
                        Command command = _commands[_current++];
                        command.Execute();
                    }
                }
            }

            public void Undo(int levels)
            {
                Console.WriteLine("\n---- Undo {0} levels ", levels);
                // Perform undo operations
                for (int i = 0; i < levels; i++)
                {
                    if (_current > 0)
                    {
                        Command command = _commands[--_current] as Command;
                        command.UnExecute();
                    }
                }
            }

            public void Compute(char @operator, int operand)
            {
                // Create command operation and execute it
                Command command = new CalculatorCommand(_calculator, @operator, operand);
                command.Execute();

                // Add command to undo list
                _commands.Add(command);
                _current++;
            }
        }
    }
}
