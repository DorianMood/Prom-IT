using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Prom_IT
{
    class FileParser
    {
        public static List<string> parse(string fileName)
        {
            List<string> words = new List<string>();

            if (File.Exists(fileName))
            {
                if (GetEncoding(fileName).BodyName != Encoding.UTF8.BodyName)
                {
                    throw new FileLoadException();
                }
                
                
                // Process file
                FileStream fs = File.OpenRead(fileName);
                StreamReader reader = new StreamReader(fs);

                while (!reader.EndOfStream)
                {
                    string line = System.Text.RegularExpressions.Regex.Replace(reader.ReadLine(), @"\s+", " ");
                    foreach (string word in line.Split(' '))
                    {
                        words.Add(word);
                    }
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
            return words;
        }
        /// <summary

        /// Get File's Encoding

        /// </summary>
        /// <param name="filename">The path to the file
        private static Encoding GetEncoding(string filename)
        {
            // This is a direct quote from MSDN:  
            // The CurrentEncoding value can be different after the first
            // call to any Read method of StreamReader, since encoding
            // autodetection is not done until the first call to a Read method.

            using (var reader = new StreamReader(filename, Encoding.Default, true))
            {
                if (reader.Peek() >= 0) // you need this!
                    reader.Read();

                return reader.CurrentEncoding;
            }
        }
    }
}
