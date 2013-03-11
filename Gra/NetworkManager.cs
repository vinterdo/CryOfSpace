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

        UdpClient UdpClient;
        TcpClient TcpClient;
        TcpListener TcpListener;
        int port = 25565;
        public List<TcpClient> Clients;

        public bool IsHost = false ;

        private NetworkManager()
        {
            //UdpClient = new UdpClient(port);
            //TcpListener = new TcpListener(port);
            Clients = new List<TcpClient>();
        }
        

        private static NetworkManager Instance = new NetworkManager();

        public static NetworkManager Singleton
        {
            get
            { return Instance; }
            set
            {}
        }

        public void Update()
        {
            //TcpListener.
        }

        public void ConnectTo(string host)
        {
            try
            {
                TcpClient = new TcpClient(host, port);
            }
            catch
            {
            }
        }

        public void InitalizeServer(string address)
        {
            IPAddress Address;
            try
            {
                Address = IPAddress.Parse(address);
                TcpListener = new TcpListener(Address, port);
                TcpListener.Start();
                TcpListener.BeginAcceptTcpClient(new AsyncCallback(AcceptTcpClientCallback), TcpListener);
            }
            catch 
            { 
            }
        }

        public void AcceptTcpClientCallback(IAsyncResult asyncResult)
        {
            TcpListener l = asyncResult.AsyncState as TcpListener;
            Clients.Add(l.EndAcceptTcpClient(asyncResult));
        }

        
    }
}
