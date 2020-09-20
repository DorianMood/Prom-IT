using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {

            Server server = new Server(1337);
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
                    // create new database from given file
                    string fileName = command[1];
                    server.Create(fileName);
                }
                else if (command.Length == 2 && (command[0] == "update" || command[0] == "u"))
                {
                    // update existing database from given file
                    string fileName = command[1];
                    server.Update(fileName);
                }
                else if (command.Length == 1 && (command[0] == "remove" || command[0] == "r"))
                {
                    // remove existing database
                    server.Remove();
                }
            }
            /**
             * 1. Multiple clients. +
             * 2. TCP/IP server. +
             * 3. server.exe db_path port
             * 4. Change db content on the fly from console:
             * 4.1. create filename
             * 4.2. update filename
             * 4.3. remove
             */
        }
    }
}
