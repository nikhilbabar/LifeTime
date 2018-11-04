using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTime.CreationalPatterns
{

    /// <summary>
    /// § Singleton Pattern
    /// Ensure a class has only one instance and provide a global point of access to it.
    /// 
    /// § Use
    /// It can be used to create global object which handles resources.
    /// For example: Class which start any application.
    /// 
    /// § Participants
    /// The classes and objects participating in this pattern are:
    ///     ► Singleton (LoadBalancer)
    ///       1. Defines an Instance operation that lets clients access its unique instance. Instance is a class operation.
    ///       2. Responsible for creating and maintaining its own unique instance.
    /// </summary>
    public static class SingletonPattern
    {
        /// <summary>
        /// This structural code demonstrates the Singleton pattern which assures only a single instance 
        /// (the singleton) of the class can be created.
        /// </summary>
        public static void Compute()
        {
            Console.WriteLine("\n\nSingleton Pattern\n");

            LoadBalancer b1 = LoadBalancer.GetLoadBalancer();
            LoadBalancer b2 = LoadBalancer.GetLoadBalancer();
            LoadBalancer b3 = LoadBalancer.GetLoadBalancer();
            LoadBalancer b4 = LoadBalancer.GetLoadBalancer();

            // Same instance?
            if (b1 == b2 && b2 == b3 && b3 == b4)
            {
                Console.WriteLine("Same instance\n");
            }

            // Load balance 15 server requests
            LoadBalancer balancer = LoadBalancer.GetLoadBalancer();
            for (int i = 0; i < 15; i++)
            {
                string server = balancer.Server;
                Console.WriteLine("Dispatch request to: " + server);
            }
        }
    }

    /// <summary>
    /// The 'Singleton' class
    /// </summary>
    class LoadBalancer
    {
        private static LoadBalancer _instance;
        private List<string> _servers = new List<string>();
        private Random _random = new Random();

        // Lock synchronization object
        private static object syncLock = new object();

        // Constructor (protected)
        protected LoadBalancer()
        {
            // List of available servers
            _servers.Add("Server.1");
            _servers.Add("Server.2");
            _servers.Add("Server.3");
            _servers.Add("Server.4");
            _servers.Add("Server.5");
        }

        public static LoadBalancer GetLoadBalancer()
        {
            /// Support multithreaded applications through 'Double checked locking' 
            /// pattern which (once the instance exists) avoids locking each time 
            /// the method is invoked
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new LoadBalancer();
                    }
                }
            }

            return _instance;
        }

        // Simple, but effective random load balancer
        public string Server
        {
            get
            {
                int index = _random.Next(_servers.Count);
                return _servers[index].ToString();
            }
        }
    }
}
