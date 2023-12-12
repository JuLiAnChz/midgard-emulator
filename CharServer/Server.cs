using System;
using Core.Network;
using Shared;
using System.Net.Sockets;
using SimpleTCP;

namespace CharServer
{
    public class Server
    {
        public SimpleTcpServer _server { get; set; }

        public Server(int port) {
            _server = new SimpleTcpServer();
            _server.ClientConnected += NewClientConnected;
            _server.ClientDisconnected += ClientDisconnected;
            _server.DataReceived += PacketReceived;

            _server.Start(port);
        }

        public void NewClientConnected(object? sender, TcpClient client)
        {
            if (client.Client.RemoteEndPoint != null)
            {
                string? remoteAddr = client.Client.RemoteEndPoint.ToString();
                Display.Info($"Client connected {client.Client.RemoteEndPoint}");
            }
        }

        public void ClientDisconnected(object? sender, TcpClient client)
        {
        }

        public void PacketReceived(object? sender, Message e)
        {
        }
    }
}
