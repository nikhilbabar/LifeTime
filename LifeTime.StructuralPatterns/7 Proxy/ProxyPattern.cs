using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.StructuralPatterns
{
    /// <summary>
    /// § Proxy Pattern
    /// Provide a surrogate or placeholder for another object to control access to it.
    /// 
    /// § Participants
    /// The classes and objects participating in this pattern are:
    /// ► Proxy(MathProxy)
    ///   → Maintains a reference that lets the proxy access the real subject. Proxy may 
    ///     refer to a Subject if the RealSubject and Subject interfaces are the same.
    ///   → Provides an interface identical to Subject's so that a proxy can be substituted 
    ///     for for the real subject.
    ///   → Controls access to the real subject and may be responsible for creating and deleting it.
    ///   → Other responsibilities depend on the kind of proxy:
    ///     → remote proxies are responsible for encoding a request and its arguments and for 
    ///       sending the encoded request to the real subject in a different address space.
    ///     → virtual proxies may cache additional information about the real subject so that 
    ///       they can postpone accessing it.For example, the ImageProxy from the Motivation 
    ///       caches the real images extent.
    ///     → protection proxies check that the caller has the access permissions required to 
    ///       perform a request.
    /// ► Subject (IMath)
    ///   → Defines the common interface for RealSubject and Proxy so that a Proxy can 
    ///     be used anywhere a RealSubject is expected.
    /// ► RealSubject (Math)
    ///   → Defines the real object that the proxy represents.
    /// </summary>
    public static class ProxyPattern
    {
        /// <summary>
        /// This real-world code demonstrates the Proxy pattern for a Math object represented 
        /// by a MathProxy object.
        /// </summary>
        public static void Compute()
        {
            Console.WriteLine("\n\nProxy Pattern\n");

            // Create math proxy
            MathProxy proxy = new MathProxy();

            // Do the math
            Console.WriteLine("\t4 + 2 = " + proxy.Add(4, 2));
            Console.WriteLine("\t4 - 2 = " + proxy.Sub(4, 2));
            Console.WriteLine("\t4 * 2 = " + proxy.Mul(4, 2));
            Console.WriteLine("\t4 / 2 = " + proxy.Div(4, 2));
        }

        /// <summary>
        /// The 'Subject interface
        /// </summary>
        public interface IMath
        {
            double Add(double x, double y);
            double Sub(double x, double y);
            double Mul(double x, double y);
            double Div(double x, double y);
        }

        /// <summary>
        /// The 'RealSubject' class
        /// </summary>
        class Math : IMath
        {
            public double Add(double x, double y) { return x + y; }
            public double Sub(double x, double y) { return x - y; }
            public double Mul(double x, double y) { return x * y; }
            public double Div(double x, double y) { return x / y; }
        }

        /// <summary>
        /// The 'Proxy Object' class
        /// </summary>
        class MathProxy : IMath
        {
            private Math _math = new Math();

            public double Add(double x, double y)
            {
                return _math.Add(x, y);
            }
            public double Sub(double x, double y)
            {
                return _math.Sub(x, y);
            }
            public double Mul(double x, double y)
            {
                return _math.Mul(x, y);
            }
            public double Div(double x, double y)
            {
                return _math.Div(x, y);
            }
        }
    }
}
