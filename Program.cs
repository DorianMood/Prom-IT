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
                autocompleter.Create(opt.Create);
            }
            else if (opt.Update != null && opt.Update.Length != 0)
            {
                autocompleter.Update(opt.Update);
            }
            else if (opt.Remove)
            {
                autocompleter.Remove();
            }
        }
        static void Error(IEnumerable<Error> errors)
        {
            
        }
    }
}
