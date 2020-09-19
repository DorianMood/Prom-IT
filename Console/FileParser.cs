using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Prom_IT
{
    class FileParser
    {
        public static HashSet<Completion> ParseCompletions(string fileName)
        {
            HashSet<Completion> completions = new HashSet<Completion>();

            if (File.Exists(fileName))
            {
                // Check encoding to be equal UTF-8
                if (GetEncoding(fileName).BodyName != Encoding.UTF8.BodyName)
                {
                    throw new FileLoadException();
                }

                // Process file
                // Here we have "words" extraction algorithm.
                // It's unclear what kind of input data is being considered as "word",
                // so author decided to keep all the data between spaces as "word",
                // another way id to process only words matching following expression "[a-zA-Z0-9_-]+".
                using (FileStream fs = File.OpenRead(fileName))
                using (StreamReader reader = new StreamReader(fs))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = System.Text.RegularExpressions.Regex.Replace(reader.ReadLine().Trim(), @"\s+", " ").ToLower();
                        foreach (string word in line.Split(' '))
                        {
                            if (word.Length < 3 || word.Length > 15)
                                continue;
                            Completion item;
                            if (completions.Any(item => item.Word == word))
                            {
                                item = completions.First(item => item.Word == word);
                                item.Frequency++;
                            }
                            else
                            {
                                item = new Completion() { Word = word, Frequency = 1 };
                            }
                            completions.Add(item);
                        }
                    }
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
            int minFrequency = int.Parse(ConfigurationManager.AppSettings.Get("MinFrequency"));
            return completions.Where(item => item.Frequency >= minFrequency).ToHashSet();
        }
        private static Encoding GetEncoding(string filename)
        {
            // This is a direct quote from MSDN:  
            // The CurrentEncoding value can be different after the first
            // call to any Read method of StreamReader, since encoding
            // autodetection is not done until the first call to a Read method.

            using var reader = new StreamReader(filename, Encoding.Default, true);
            if (reader.Peek() >= 0) // you need this!
                reader.Read();

            return reader.CurrentEncoding;
        }
    }
}
