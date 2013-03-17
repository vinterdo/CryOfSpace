using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

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

        bool IsInitalized = false;


        //UdpClient UdpClient;
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
            if (IsInitalized)
            {
                if (IsHost)
                {
                    foreach (TcpClient c in Clients)
                    {
                        if (c != null)
                        {
                            SendLevel(c);
                        }
                    }
                }
                else
                {
                    ReciveLevel(TcpClient);
                }
            }
            //TcpListener.
        }

        public void ConnectTo(string host)
        {
            try
            {
                TcpClient = new TcpClient(host, port);
                TcpClient.Connect(new IPEndPoint(IPAddress.Parse(host), port+1));
                IsInitalized = true;
            }
            catch(Exception e)
            {
                e = null;
                
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
                IsInitalized = true;
                IsHost = true;
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

        public void SendLevel(TcpClient Client)
        {
            //List<byte[]> Buffer = new List<byte[]>();
            StreamWriter SW = new StreamWriter(Client.GetStream());
            try
            {
                SW.Write("LevelPacket".ToCharArray());
                Client.GetStream().Flush();
            }
            catch(Exception e)
            {
                //Add Client Remove and disconnect log
            }

            
        }

        public void ReciveLevel(TcpClient Client)
        {
            char[] Buffer = new char[100];
            StreamReader SR = new StreamReader(Client.GetStream());
            try
            {
                SR.Read(Buffer, 0, 11);
            }
            catch (Exception e)
            {
                e = null;
            }
            //Client.GetStream().Read(Buffer, 0, 20);
            string PacketType = Buffer.ToString();
            if (PacketType == "LevelPacket")
            {
            }
        }

        
    }
}
