using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fleck;

namespace ILC.Seve
{
    class ClientBroadcaster
    {
        public List<IWebSocketConnection> Connections { get; private set;  }

        public ClientBroadcaster()
        {
            Connections = new List<IWebSocketConnection>();
        }

        public void StartServer()
        {
            var server = new WebSocketServer("ws://localhost:8080");
            server.Start(socket =>
            {
                socket.OnOpen = () => Connections.Add(socket);
                socket.OnClose = () => Connections.Remove(socket);
            });
        }

        public void Broadcast(string data)
        {
            foreach (var connection in Connections)
            {
                connection.Send(data);
            }
        }
    }
}
