using System.Collections.Generic;
using Fleck;

namespace ILC.Seve.Web
{
    public class ClientBroadcaster
    {
        private List<IWebSocketConnection> Connections;
        private WebSerializer Serializer;

        public ClientBroadcaster(WebSerializer serializer)
        {
            Connections = new List<IWebSocketConnection>();
            Serializer = serializer;
        }

        public void StartServer()
        {
            var server = new WebSocketServer("ws://localhost:8080");
            server.Start(socket =>
            {
                socket.OnOpen = () => { Connections.Add(socket); socket.Send(Serializer.Rewind()); };
                socket.OnClose = () => Connections.Remove(socket);
                socket.OnMessage = (string m) => OnMessage(socket, m);
            });
        }

        public void Broadcast(string data)
        {
            foreach (var connection in Connections)
            {
                connection.Send(data);
            }
        }

        private void OnMessage(IWebSocketConnection socket, string message)
        {
            
        }
    }
}
