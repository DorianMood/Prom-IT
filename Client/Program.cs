using System;
using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace Client
{
    class Program
    {
        public class Options
        {
            [Value(0, Required = true, HelpText = "Host name or IP addres.")]
            public string Host { get; set; }
            [Value(1, Required = true, HelpText = "Host port.")]
            public int Port { get; set; }
        }
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Run)
                .WithNotParsed(err => DisplayHelp());
        }
        static void Run(Options opt)
        {
            Client client = new Client(opt.Host, opt.Port);
            List<string> completions;
            while (true)
            {
                Console.Write(">");
                string command = Console.ReadLine();
                if (command == "exit")
                {
                    break;
                }

                // TODO : Check if command is correct
                if (command.Split(' ')[0] == "get" && command.Split(' ').Length == 2)
                {
                    completions = client.GetCompletions(command.Split(' ')[1]);
                    foreach(string completion in completions)
                    {
                        Console.WriteLine($"- {completion}");
                    }
                }
            }

        }
        static void DisplayHelp()
        {
            Console.WriteLine("USAGE");
        }
    }
}
