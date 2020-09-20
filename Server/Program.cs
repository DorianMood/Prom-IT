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
                string command = Console.ReadLine();
                if (command == "exit")
                {
                    server.Stop();
                    break;
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
