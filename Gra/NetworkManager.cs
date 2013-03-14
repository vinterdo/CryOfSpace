﻿using System;
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

        bool IsInitalized = false;


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
            System.Text.ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] Buffer = encoding.GetBytes("LevelPacket".ToCharArray());
            Client.GetStream().Write(Buffer, 0, 0);

            Client.GetStream().Flush();
        }

        public void ReciveLevel(TcpClient Client)
        {
            byte[] Buffer = new byte[]{};
            Client.GetStream().Read(Buffer, 0, 0);
            string PacketType = Buffer.ToString();
            if (PacketType == "LevelPacket")
            {
                var i = 0;
            }
        }

        
    }
}
