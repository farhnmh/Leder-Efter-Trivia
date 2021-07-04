using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Leder_Efter_Server
{
    class Server
    {
        public static int MaxPlayers { get; private set; }
        public static int Port { get; private set; }
        public static int total;

        public delegate void PacketHandler(int _fromClient, Packet _packet);
        public static Dictionary<int, Client> clients = new Dictionary<int, Client>();
        public static Dictionary<int, PacketHandler> packetHandlers;
        public static List<AccountDatabase> accountDatabase = new List<AccountDatabase>();
        public static List<ReadyDatabase> readyDatabase = new List<ReadyDatabase>();

        private static TcpListener tcpListener;
        private static UdpClient udpListener;

        public static void Start(int _maxPlayer, int _port)
        {
            MaxPlayers = _maxPlayer;
            Port = _port;

            Console.WriteLine("Server is starting...");
            InitializeServerData();

            tcpListener = new TcpListener(IPAddress.Any, Port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            udpListener = new UdpClient(Port);
            udpListener.BeginReceive(UDPReceiveCallback, null);

            Console.WriteLine($"Server started on {Port}\n");
            Console.WriteLine("[CLIENT ACTIVITY]");
        }

        private static void TCPConnectCallback(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            Console.WriteLine($"Incoming connection from {_client.Client.RemoteEndPoint}...");

            for (int i = 1; i <= MaxPlayers; i++)
            {
                if (clients[i].tcp.socket == null)
                {
                    clients[i].tcp.Connect(_client);
                    return;
                }
            }

            Console.WriteLine($"{_client.Client.RemoteEndPoint} failed to connect: server full");
        }

        private static void UDPReceiveCallback(IAsyncResult _result)
        {
            try
            {
                IPEndPoint _clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] _data = udpListener.EndReceive(_result, ref _clientEndPoint);
                udpListener.BeginReceive(UDPReceiveCallback, null);

                if (_data.Length < 4)
                {
                    return;
                }

                using (Packet _packet = new Packet(_data))
                {
                    int _clientId = _packet.ReadInt();

                    if (_clientId == 0)
                    {
                        return;
                    }

                    if (clients[_clientId].udp.endPoint == null)
                    {
                        clients[_clientId].udp.Connect(_clientEndPoint);
                        return;
                    }

                    if (clients[_clientId].udp.endPoint.ToString() == _clientEndPoint.ToString())
                    {
                        clients[_clientId].udp.HandleData(_packet);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error receiving UDP data: {e}");
            }
        }

        public static void SendUDPData(IPEndPoint _clientEndPoint, Packet _packet)
        {
            try
            {
                if (_clientEndPoint != null)
                {
                    udpListener.BeginSend(_packet.ToArray(), _packet.Length(), _clientEndPoint, null, null);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error sending data to {_clientEndPoint} via UDP: {e}");
            }
        }

        private static void InitializeServerData()
        {
            for (int i = 1; i <= MaxPlayers; i++)
            {
                clients.Add(i, new Client(i));
            }

            packetHandlers = new Dictionary<int, PacketHandler>()
            {
                { (int)ClientPackets.welcomeRequest, ServerHandle.WelcomeReceived },
                { (int)ClientPackets.signInRequest, ServerHandle.SignInReceived },
                { (int)ClientPackets.signUpRequest, ServerHandle.SignUpReceived },
                { (int)ClientPackets.hostRoomRequest, ServerHandle.HostRoomReceived },
                { (int)ClientPackets.joinRoomRequest, ServerHandle.JoinRoomReceived },
                { (int)ClientPackets.leaveRoomRequest, ServerHandle.LeaveRoomReceived },
                { (int)ClientPackets.destroyRoomRequest, ServerHandle.DestroyRoomReceived },
                { (int)ClientPackets.startMatchRequest, ServerHandle.StartMatchReceived },
                { (int)ClientPackets.playerPositionRequest, ServerHandle.PlayerPositionReceived },

                { (int)ClientPackets.chatboxRequest, ServerHandle.ChatboxReceived },
                { (int)ClientPackets.randomizeRequest, ServerHandle.RandomizeReceived },
                { (int)ClientPackets.colorRequest, ServerHandle.ColorReceived },
                { (int)ClientPackets.mintakSpawnDong, ServerHandle.MintakPlayer },
                { (int)ClientPackets.playerMovement, ServerHandle.PlayerMovement },
                { (int)ClientPackets.readyGan, ServerHandle.PlayerReady },

                { (int)ClientPackets.triviaQuestionRequest, ServerHandle.TriviaQuestionReceived },
                { (int)ClientPackets.triviaDatabaseRequest, ServerHandle.TriviaDatabaseReceived },

                { (int)ClientPackets.storeScorePlay, ServerHandle.ScorePlayReceived },
                { (int)ClientPackets.scorePlayRequest, ServerHandle.ScorePlayerSent }
            };

            Console.WriteLine("Initialized packets");
        }
    }
}
