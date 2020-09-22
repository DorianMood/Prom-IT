using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;

namespace Server
{
    class Program
    {
        public class Options
        {
            [Value(0, Required = true, HelpText = "Host port.")]
            public int Port { get; set; }
        }

        static void Main(string[] args)
        {
            // TODO : mvove to .dbs file instead of SQL server.

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(Run)
                .WithNotParsed(Error);
        }


        static void Run(Options opt)
        {
            Console.WriteLine($"Server started. Avaliable commands:\n" +
                              $"\texit - cancel execution and exit.\n" +
                              $"\tcreate, c - <fileName> create new completion database.\n" +
                              $"\tupdate, u - <fileName> update existing completion database.\n" +
                              $"\tremove, r - <fileName> remove existing completion database.\n" +
                              $"\nConfigure your database in Server.dll.config file" +
                              $"\n");

            Server server = new Server(opt.Port);
            server.Start();
            while (true)
            {
                Console.Write("> ");
                string[] command = Console.ReadLine().Split(' ');
                if (command.Length >= 1 && command[0] == "exit")
                {
                    server.Stop();
                    break;
                }
                else if (command.Length == 2 && (command[0] == "create" || command[0] == "c"))
                {
                    try
                    {
                        // create new database from given file
                        string fileName = command[1];
                        server.Create(fileName);
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine("\nNo such file or directory.");
                    }
                    catch (FileLoadException e)
                    {
                        Console.WriteLine("\nPlease make sure specified file encoding is UTF-8.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("\nCreating failed.");
                        Console.WriteLine("Unknown error occured.");
                        Console.WriteLine();
                        Console.WriteLine(e.ToString());
                    }
                }
                else if (command.Length == 2 && (command[0] == "update" || command[0] == "u"))
                {
                    try
                    {
                        // update existing database from given file
                        string fileName = command[1];
                        server.Update(fileName);
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine("\nNo such file or directory.");
                    }
                    catch (FileLoadException e)
                    {
                        Console.WriteLine("\nPlease make sure specified file encoding is UTF-8.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("\nUpdating failed.");
                        Console.WriteLine("Unknown error occured.");
                        Console.WriteLine();
                        Console.WriteLine(e.ToString());
                    }
                }
                else if (command.Length == 1 && (command[0] == "remove" || command[0] == "r"))
                {
                    try
                    {
                        // remove existing database
                        server.Remove();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Removing failed.");
                        Console.WriteLine("Unknown error occured.");
                        Console.WriteLine();
                        Console.WriteLine(e.ToString());
                    }
                }
            }
        }
        static void Error(IEnumerable<Error> obj)
        {

        }
    }
}
