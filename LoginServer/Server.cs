using Shared;
using SimpleTCP;
using Core.Network;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace LoginServer
{
    public class Server
    {
        public SimpleTcpServer _server {  get; set; }
        public static ConcurrentDictionary<string, ConnectionData> clients = new ConcurrentDictionary<string, ConnectionData>();
        private CommandProcessor _processor = new CommandProcessor();

        public Server(int port)
        {
            _server = new SimpleTcpServer();
            _server.ClientConnected += NewClientConnected;
            _server.ClientDisconnected += ClientDisconnected;
            _server.DataReceived += PacketReceived;

            _server.Start(port);
        }

        public void NewClientConnected(object? sender, TcpClient client)
        {
            if(client.Client.RemoteEndPoint != null)
            {
                string? remoteAddr = client.Client.RemoteEndPoint.ToString();
                clients.TryAdd(remoteAddr!, new ConnectionData { client = client });
                Display.Info($"Client connected {client.Client.RemoteEndPoint}");
            }
        }

        public void ClientDisconnected(object? sender, TcpClient client)
        {
            if( client.Client.RemoteEndPoint != null)
            {
                string? clientIpAddres = client.Client.RemoteEndPoint.ToString();
                if (clients.ContainsKey(clientIpAddres!))
                {
                    clients.TryRemove(clientIpAddres!, out var c);

                    /*if (fd.AccountId > 0 && PlayersSession.ContainsKey(fd.AccountId))
                        PlayersSession.TryRemove(fd.AccountId, out var ps);*/
                }
                Display.Info($"Client disconnected {client.Client.RemoteEndPoint}");
            }
        }

        public void PacketReceived(object? sender, Message e)
        {
            if (e.TcpClient.Client.RemoteEndPoint == null) return;

            string ?clientIpAddres = e.TcpClient.Client.RemoteEndPoint.ToString();

            if (!clients.ContainsKey(clientIpAddres!))
            {
                Display.Error($"Client not found");
                e.TcpClient.Close();
                return;
            }

            _processor.ProcessCommand(e.Data, clientIpAddres!);
        }
    }
}
