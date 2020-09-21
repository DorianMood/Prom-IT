using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;

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
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Run)
                .WithNotParsed(Error);
        }
        static void Run(Options opt)
        {
            // Create completer
            var autocompleter = new Autocompleter();

            if (opt.Create != null && opt.Create.Length != 0)
            {
                // Create new completions database
                try
                {
                    autocompleter.Create(opt.Create);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("No such file or directory.");
                }
                catch (FileLoadException e)
                {
                    Console.WriteLine("Please make sure specified file encoding is UTF-8.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Creating failed.");
                    Console.WriteLine("Unknown error occured.");
                    Console.WriteLine();
                    Console.WriteLine(e.ToString());
                }
            }
            else if (opt.Update != null && opt.Update.Length != 0)
            {
                // Update existing completions database
                try
                {
                    autocompleter.Update(opt.Update);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("No such file or directory.");
                }
                catch (FileLoadException e)
                {
                    Console.WriteLine("Please make sure specified file encoding is UTF-8.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Updating failed.");
                    Console.WriteLine("Unknown error occured.");
                    Console.WriteLine();
                    Console.WriteLine(e.ToString());
                }
            }
            else if (opt.Remove)
            {
                // Remove existing completions database
                try
                {
                    autocompleter.Remove();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Removing failed.");
                    Console.WriteLine("Unknown error occured.");
                    Console.WriteLine();
                    Console.WriteLine(e.ToString());
                }
            }
            else
            {
                // Create and run editor
                try
                {
                    Editor editor = new Editor();
                    editor.RunCycle();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unknown error occured.");
                    Console.WriteLine();
                    Console.WriteLine(e.ToString());
                };
            }
        }
        static void Error(IEnumerable<Error> errors)
        {
            Console.WriteLine("Arguments parsing error occured.");
        }
    }
}
