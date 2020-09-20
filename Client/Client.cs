using Prom_IT;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Client
    {
        public string Host { get; }
        public int Port { get; }
        private TcpClient TcpClient;
        public Client(string host, int port)
        {
            Host = host;
            Port = port;
            TcpClient = new TcpClient(host, port);
        }
        public List<Completion> GetCompletions(string word)
        {
            List<Completion> completions = new List<Completion>();

            // Encode request to byte array
            Byte[] data = Encoding.ASCII.GetBytes($"get {word}\0");

            // Request completions here and put them
            NetworkStream stream = TcpClient.GetStream();
            stream.Write(data, 0, data.Length);

            // Buffer variable to store recieved bytes
            data = new byte[256];
            string response = string.Empty;

            // Read response
            int bytes = stream.Read(data, 0, data.Length);
            response = Encoding.ASCII.GetString(data, 0, bytes);

            Console.WriteLine(response);

            // Dispose stream
            stream.Close();

            return completions;
        }
        ~Client()
        {
            TcpClient.Close();
        }
    }
}
