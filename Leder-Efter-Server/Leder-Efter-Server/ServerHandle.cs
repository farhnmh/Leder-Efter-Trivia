using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Numerics;

namespace Leder_Efter_Server
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string a = "TESTING";

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}");
            Server.total = _fromClient;
            //Console.WriteLine("Total Player Join = " + Server.total);
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            //Server.clients[_fromClient].SendtoGame(a);
        }

        public static void SignInReceived(int _fromClient, Packet _packet)
        {
            ClientData.Account _account = _packet.ReadObject<ClientData.Account>();

            string validation = AccountHandler.SignIn(_account.username, _account.password);
            if (validation == "login was successful")
                Server.readyDatabase.Add(new ReadyDatabase(_fromClient, false));

            ServerSend.SignInValidation(_fromClient, validation);
        }

        public static void SignUpReceived(int _fromClient, Packet _packet)
        {
            ClientData.Account _account = _packet.ReadObject<ClientData.Account>();
            string validation = AccountHandler.SignUp(_account.username, _account.password);
            ServerSend.SignUpValidation(_fromClient, validation);
        }

        public static void HostRoomReceived(int _fromClient, Packet _packet)
        {
            string code = _packet.ReadString();
            string notif = RoomHandler.HostRoomValidation(code);
            ServerSend.HostRoomValidation(_fromClient, notif);
        }

        public static void JoinRoomReceived(int _fromClient, Packet _packet)
        {
            string code = _packet.ReadString();
            int id = _packet.ReadInt();
            string uname = _packet.ReadString();
            string notif = RoomHandler.JoinRoomValidation(code, id, uname);
            ServerSend.JoinRoomValidation(_fromClient, notif);
        }

        public static void LeaveRoomReceived(int _fromClient, Packet _packet)
        {
            string code = _packet.ReadString();
            string uname = _packet.ReadString();
            RoomHandler.LeaveRoom(code, uname);
            ServerSend.BroadcastPlayerLeft(code, uname);
        }

        public static void DestroyRoomReceived(int _fromClient, Packet _packet)
        {
            string code = _packet.ReadString();
            RoomHandler.DestroyRoom(code);
            ServerSend.BroadcastDestroyRoom(code);
        }

        public static void StartMatchReceived(int _fromClient, Packet _packet)
        {
            string code = _packet.ReadString();
            string gameType = _packet.ReadString();
            RoomHandler.StartMatch(code, true, gameType);
            ServerSend.BroadcastStartMatch(code, gameType);
        }

        public static void PlayerPositionReceived(int _fromClient, Packet _packet)
        {
            string code = _packet.ReadString();
            int id = _packet.ReadInt();
            int controlHorizontal = _packet.ReadInt();
            int controlVertical = _packet.ReadInt();
            
            //Console.WriteLine($"{code} - {id} - {controlHorizontal} - {controlVertical}");
            ServerSend.BroadcastPlayerPosition(code, id, controlHorizontal, controlVertical);
        }

        public static void ChatboxReceived(int _fromClient, Packet _packet)
        {
            ClientData.Chatbox _chatbox = _packet.ReadObject<ClientData.Chatbox>();
            ServerSend.BroadcastChatbox(_chatbox.username, _chatbox.message);
        }

        public static void RandomizeReceived(int _fromClient, Packet _packet)
        {
            bool _ready = _packet.ReadBool();
            ReadyHandler.ReadySetter(_fromClient, _ready);

            if (_ready)
            {
                ReadyHandler.totalReady++;
                ServerSend.BroadcastReady(ReadyHandler.totalReady, Server.readyDatabase.Count, "(wait)", "(wait)");

                if (ReadyHandler.totalReady == Server.readyDatabase.Count)
                {
                    Console.WriteLine($"All player're ready: {RandomizeHandler.StuffRandomizer()} & {RandomizeHandler.ColorRandomizer()}");
                    ServerSend.BroadcastReady(ReadyHandler.totalReady, Server.readyDatabase.Count, RandomizeHandler.StuffRandomizer(), RandomizeHandler.ColorRandomizer());
                }
            }
        }

        public static void PlayerReady(int _fromClient, Packet _packet)
        {
            bool _ready = _packet.ReadBool();
            ReadyHandler.ReadySetter(_fromClient, _ready);

            if (_ready)
            {
                ReadyHandler.totalReady++;
                ServerSend.Siap(ReadyHandler.totalReady, Server.readyDatabase.Count, "(wait)");

                if (ReadyHandler.totalReady == Server.readyDatabase.Count)
                {
                    //Console.WriteLine($"All player're ready: {RandomizeHandler.StuffRandomizer()} & {RandomizeHandler.ColorRandomizer()}");
                    ServerSend.Siap(ReadyHandler.totalReady, Server.readyDatabase.Count, "ready");
                }
            }
        }

        public static void ColorReceived(int _fromClient, Packet _packet)
        {
            bool warna = _packet.ReadBool();
            ServerSend.SendColor();
        }

        public static void MintakPlayer(int _fromClient, Packet _packet)
        {
            string a = "TESTING";

            Server.clients[_fromClient].SendtoGame(a);
        }

        public static void TriviaQuestionReceived(int _fromClient, Packet _packet)
        {
            string codeRoom = _packet.ReadString();
            bool ready = _packet.ReadBool();
            int maxQuestion = _packet.ReadInt();

            TriviaHandler.SetQuestion(codeRoom, ready, maxQuestion);
        }

        public static void TriviaDatabaseReceived(int _fromClient, Packet _packet)
        {
            string codeRoom = _packet.ReadString();
            int maxCategory = _packet.ReadInt();
            int maxQuestion = _packet.ReadInt();

            TriviaHandler.SetDatabase(codeRoom, maxCategory, maxQuestion);
        }

        public static void ScorePlayReceived(int _fromClient, Packet _packet)
        {
            string uname = _packet.ReadString();
            int score = _packet.ReadInt();
            int play = _packet.ReadInt();

            AccountHandler.AddScorePlay(uname, score, play);
        }

        public static void ScorePlayerSent(int _fromClient, Packet _packet)
        {
            string uname = _packet.ReadString();

            AccountHandler.ShowScorePlay(_fromClient, uname);
        }

        public static void PlayerMovement(int _fromClient, Packet _packet)
        {
            bool[] _inputs = new bool[_packet.ReadInt()];
            for(int i = 0; i < _inputs.Length; i++)
            {
                _inputs[i] = _packet.ReadBool();
            }
            Quaternion rot = _packet.ReadQuaternion();
            Server.clients[_fromClient].player.SetInput(_inputs, rot);
        }
    }
}
