using System;
using System.Collections.Generic;
using System.Text;

namespace Prom_IT
{
    class Editor
    {
        public Editor()
        {

        }
        public void RunCycle()
        {
            string line;
            while ((line = ReadLineWithCancel()) != "")
            {
                // Perform autocompletion here

            }
        }
        public string ReadLineWithCancel()
        {
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
