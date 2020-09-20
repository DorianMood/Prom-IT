using Prom_IT;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client
{
    class Client
    {
        public string Host { get; }
        public int Port { get; }
        public Client(string host, int port)
        {
            Host = host;
            Port = port;
        }
        public List<Completion> GetCompletions(string word)
        {
            List<Completion> completions = new List<Completion>();

            // Request completions here and put them

            return completions;
        }
    }
}
