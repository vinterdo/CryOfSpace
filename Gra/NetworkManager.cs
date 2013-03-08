using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Gra
{
    public sealed class NetworkManager
    {
        public enum NetworkState
        {
            NotConnected,
            TryingToConnect,
            SendingLevel,
            ReceivingLevel,
            InGame
        }

        UdpClient Client;

        private NetworkManager()
        {
            Client = new UdpClient(25565);
        }
        

        private static NetworkManager Instance = new NetworkManager();

        public static NetworkManager Singleton
        {
            get
            { return Instance; }
            set
            {}
        }
    }
}
