﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
            //task.Wait();
        }
        public void Stop()
        {
            IsRunning = false;
            TcpListener.Stop();
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
                catch (Exception e)
                {
                    Console.WriteLine("ERROR");
                }
            }
            Console.WriteLine("Finishing");
        }
        private async Task StartReadingDataFromClient(TcpClient acceptedClient)
        {
            try
            {
                IPEndPoint ipEndPoint = (IPEndPoint)acceptedClient.Client.RemoteEndPoint;
                while (true)
                {
                    MemoryStream bufferStream = new MemoryStream();
                    // ******** Note 2 ********
                    byte[] buffer = new byte[1024];
                    int packetSize = await acceptedClient.GetStream().ReadAsync(buffer, 0, buffer.Length);

                    if (packetSize == 0)
                    {
                        break;
                    }

                    Console.WriteLine("Accepted new message from: IP: {0} Port: {1}\nMessage: {2}",
                        ipEndPoint.Address, ipEndPoint.Port, Encoding.Default.GetString(buffer));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR");
            }
            finally
            {
                acceptedClient.Close();
                Clients.Remove(acceptedClient);
            }
        }
    }
}