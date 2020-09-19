using System;
using System.Collections.Generic;
using System.Text;

namespace Prom_IT
{
    class Editor
    {
        private IAutocompleter completer;
        public Editor()
        {
            completer = new Autocompleter();
        }
        public void RunCycle()
        {
            string line;
            while ((line = ReadLineWithCancel()) != "")
            {
                // Perform autocompletion
                // Get completions for current word
                List<Completion> completions = completer.GetCompletions(line);
                // Display completions
                foreach (Completion completion in completions)
                {
                    Console.WriteLine($"- {completion.Word}");
                }
            }
        }
        public string ReadLineWithCancel()
        {
            Console.Write("> ");

            string line = "";
            StringBuilder buffer = new StringBuilder();

            ConsoleKeyInfo info = Console.ReadKey(true);
            Console.Write(info.KeyChar);
            while (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Escape)
            {
                buffer.Append(info.KeyChar);
                info = Console.ReadKey();
            }

            Console.WriteLine();

            if (info.Key == ConsoleKey.Enter)
            {
                line = buffer.ToString();
            }

            return line;
        }
    }
}
