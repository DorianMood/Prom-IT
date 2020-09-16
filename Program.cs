using System;
using System.Collections.Generic;
using CommandLine;

namespace Prom_IT
{
    class Program
    {
        public class Options
        {
            [Option('c', "create", Required = false, HelpText = "<path> Create new dictionary.", SetName = "create")]
            public string Create { get; set; }
            [Option('u', "update", Required = false, HelpText = "<path> Update existing dictionary.", SetName = "update")]
            public string Update { get; set; }
            [Option('r', "remove", Required = false, HelpText = "Remove existing dictionary.", SetName = "remove")]
            public bool Remove { get; set; }
        }
        static void Main(string[] args)
        {
            // Parse arguments here
            /*
             * --create, -c - Create new dictionary
             * --update, -u - Update existing dictionary
             * --remove, -r - Remove existing dictionary
             * --help, -h - Show help.
             */
            CommandLine.Parser.Default.ParseArguments<Options>(args)
                .WithParsed(run)
                .WithNotParsed(error);
        }
        static void run(Options opt)
        {
            if (opt.Create.Length != 0)
            {
                FileParser.parse(opt.Create);
            }
            else if (opt.Update.Length != 0)
            {
                FileParser.parse(opt.Create);
            }
            else if (opt.Remove)
            {
                // Remove data from db here
            }
        }
        static void error(IEnumerable<Error> errors)
        {

        }
    }
}
