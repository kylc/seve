using System.Collections.Generic;
using Fleck;

namespace ILC.Seve.Web
{
    public class ClientBroadcaster
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
                connection.Send(data);//TODO: IOException because it couldn't write data to our JS file

            }
        }
    }
}
