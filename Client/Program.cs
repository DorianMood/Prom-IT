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
            Console.WriteLine("Starting client");

            Console.WriteLine($"{opt.Host}:{opt.Port}");

            Client client = new Client(opt.Host, opt.Port);
            List<Prom_IT.Completion> completions = client.GetCompletions("h");

        }
        static void DisplayHelp()
        {
            Console.WriteLine("USAGE");
        }
    }
}
