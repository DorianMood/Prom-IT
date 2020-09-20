using Prom_IT;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

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
        public List<string> GetCompletions(string word)
        {
            List<string> completions = new List<string>();

            // Encode request to byte array
            Byte[] data = Encoding.ASCII.GetBytes($"get {word}\0");

            // Request completions here and put them
            NetworkStream stream = TcpClient.GetStream();
            stream.Write(data, 0, data.Length);

            // Buffer variable to store recieved bytes
            data = new byte[256];
            string response = string.Empty;

            // Read response and deserialize json
            int bytes = stream.Read(data, 0, data.Length);
            response = Encoding.ASCII.GetString(data, 0, bytes);
            completions = (List<string>)JsonSerializer.Deserialize(response, typeof(List<string>));

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
