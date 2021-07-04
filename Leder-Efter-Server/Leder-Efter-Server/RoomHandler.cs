using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Leder_Efter_Server
{
    class RoomDatabase
    {
        public string code { get; set; }
        public bool isPlay { get; set; }

        public List<PlayerJoinedDatabase> playerJoinedDatabase = new List<PlayerJoinedDatabase>();

        public RoomDatabase(string _code)
        {
            code = _code;
        }
    }

    class PlayerJoinedDatabase
    {
        public int id { get; set; }
        public string username { get; set; }
        public int type { get; set; }

        public PlayerJoinedDatabase(int _id, string _uname, int _type)
        {
            id = _id;
            username = _uname;
            type = _type;
        }
    }

    class RoomHandler
    {
        public static List<RoomDatabase> roomDatabase = new List<RoomDatabase>();

        public static string HostRoomValidation(string code)
        {
            foreach (RoomDatabase oroom in roomDatabase)
            {
                if (code == oroom.code)
                {
                    return "create failed! change your room code or join with that code";
                }
            }

            HostRoom(code);
            return "room created succesfully";
        }

        public static void HostRoom(string code)
        {
            roomDatabase.Add(new RoomDatabase(code));
            Console.WriteLine($"Room Created Succesfully w/ Code: {code}");
        }

        public static string JoinRoomValidation(string code, int id, string uname)
        {
            foreach (RoomDatabase oroom in roomDatabase)
            {
                if (code == oroom.code)
                {
                    JoinRoom(code, id, uname);
                    return "joined succesfully";
                }
            }

            return "join failed! change your room code or host a new room";
        }

        public static void JoinRoom(string _code, int id,string _uname)
        {
            for (int i = 0; i < roomDatabase.Count; i++)
            {
                if (_code == roomDatabase[i].code)
                {
                    bool playerJoined = false;
                    foreach (PlayerJoinedDatabase oplayer in roomDatabase[roomDatabase.Count - 1].playerJoinedDatabase)
                    {
                        if (_uname == oplayer.username)
                            playerJoined = true;
                    }

                    if (!playerJoined)
                    {
                        roomDatabase[i].playerJoinedDatabase.Add(new PlayerJoinedDatabase(id, _uname, 0));

                        for (int j = 0; j < roomDatabase[i].playerJoinedDatabase.Count; j++)
                        {
                            ServerSend.BroadcastPlayerJoined(_code, roomDatabase[i].playerJoinedDatabase[j].id, roomDatabase[i].playerJoinedDatabase[j].username);
                        }
                    }
                }
            }

            //PrintDetailRoom();
        }

        public static void LeaveRoom(string _code, string _uname)
        {
            for (int i = 0; i < roomDatabase.Count; i++)
            {
                if (_code == roomDatabase[i].code)
                {
                    for (int j = 0; j < roomDatabase[i].playerJoinedDatabase.Count; j++)
                    {
                        if (_uname == roomDatabase[i].playerJoinedDatabase[j].username)
                        {
                            roomDatabase[i].playerJoinedDatabase.RemoveAt(j);
                            Console.WriteLine($"Player-{_uname} Leave Room w/ Code: {_code}");
                        }
                    }
                }
            }
        }

        public static void DestroyRoom(string _code)
        {
            for (int i = 0; i < roomDatabase.Count; i++)
            {
                if (_code == roomDatabase[i].code)
                {
                    roomDatabase.RemoveAt(i);
                    Console.WriteLine($"Room Destroyed Succesfully w/ Code: {_code}");
                }
            }
        }

        public static void StartMatch(string _code, bool _isPlay, string _gameType)
        {
            for (int i = 0; i < roomDatabase.Count; i++)
            {
                if (_code == roomDatabase[i].code)
                {
                    roomDatabase[i].isPlay = _isPlay;
                    Console.WriteLine($"Room w/ Code: {_code} isPlay {_gameType}");
                }
            }
        }

        public static void PrintDetailRoom()
        {
            foreach (RoomDatabase oroom in roomDatabase)
            {
                Console.WriteLine($"Code Room: {oroom.code}");
                Console.WriteLine($"Player Joined:");

                foreach (PlayerJoinedDatabase oplayer in roomDatabase[roomDatabase.Count - 1].playerJoinedDatabase)
                {
                    Console.WriteLine($"Id: {oplayer.username}");
                }
            }
        }
    }
}