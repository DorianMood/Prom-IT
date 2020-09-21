using Prom_IT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        public string Address { get; }
        public int Port { get; }
        private TcpListener TcpListener;
        private List<TcpClient> Clients;
        private bool IsRunning;
        public Server(int port)
        {
            Port = port;
            TcpListener = new TcpListener(new IPEndPoint(IPAddress.Any, Port));
            Clients = new List<TcpClient>();
            IsRunning = false;
        }
        public void Start()
        {
            IsRunning = true;
            TcpListener.Start();
            Task task = Task.Run(() => StartAcceptingClientsAsync());
        }
        public void Stop()
        {
            IsRunning = false;
            TcpListener.Stop();
        }
        public void Create(string fileName)
        {
            Autocompleter completer = new Autocompleter();
            completer.Create(fileName);
        }
        public void Update(string fileName)
        {
            Autocompleter completer = new Autocompleter();
            completer.Update(fileName);
        }
        public void Remove()
        {
            Autocompleter completer = new Autocompleter();
            completer.Remove();
        }
        private async Task StartAcceptingClientsAsync()
        {
            while (IsRunning)
            {
                try
                {
                    TcpClient acceptedClient = await TcpListener.AcceptTcpClientAsync();

                    Clients.Add(acceptedClient);
                    IPEndPoint clientIPEndPoint = (IPEndPoint)acceptedClient.Client.RemoteEndPoint;

                    _ = Task.Run(() => StartReadingDataFromClient(acceptedClient));
                }
                catch (Exception e) { }
            }
        }
        private async Task StartReadingDataFromClient(TcpClient acceptedClient)
        {
            try
            {
                IPEndPoint ipEndPoint = (IPEndPoint)acceptedClient.Client.RemoteEndPoint;
                while (true)
                {
                    MemoryStream bufferStream = new MemoryStream();

                    byte[] buffer = new byte[1024];
                    int packetSize = await acceptedClient.GetStream().ReadAsync(buffer, 0, buffer.Length);

                    if (packetSize == 0)
                    {
                        break;
                    }

                    Autocompleter completer = new Autocompleter();

                    // TODO : check correct command here
                    string word = Encoding.ASCII.GetString(buffer, 0, packetSize).Split(' ')[1];
                    List<Completion> completions = completer.GetCompletions(word);
                    string json = JsonSerializer.Serialize(from item in completions select item.Word);
                    acceptedClient.GetStream().Write(Encoding.Default.GetBytes(json));
                }
            }
            catch (Exception) { }
            finally
            {
                acceptedClient.Close();
                Clients.Remove(acceptedClient);
            }
        }
    }
}
