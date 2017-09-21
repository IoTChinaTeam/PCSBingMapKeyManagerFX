using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PCSBingMapKeyManager
{
    class Program
    {
        private const string NewKeySwitch = "/newkey:";

        static void Main(string[] args)
        {
            if (args.Length > 1 || args.Any() && !args[0].StartsWith(NewKeySwitch))
            {
                Usage();
                return;
            }

            var manager = new BingMapKeyManager();

            if (!args.Any())
            {
                var key = manager.GetAsync().Result;
                if (string.IsNullOrEmpty(key))
                {
                    Console.WriteLine("No bing map key set");
                }
                else
                {
                    Console.WriteLine($"Bing map key = {key}");
                }

                Console.WriteLine($"\nHint: Use {NewKeySwitch}<key> to set or update the key");
            }
            else
            {
                var key = args[0].Substring(NewKeySwitch.Length);
                if (manager.SetAsync(key).Result)
                {
                    Console.WriteLine($"Bing map key set as '{key}'");
                }
            }
        }

        private static void Usage()
        {
            var entry = Path.GetFileName(Assembly.GetEntryAssembly().CodeBase);
            Console.WriteLine($"Usage: {entry} [{NewKeySwitch}<new key>]");
        }
    }
}
