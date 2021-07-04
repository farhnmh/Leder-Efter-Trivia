using System;
using System.Collections.Generic;
using System.Text;

namespace Leder_Efter_Server
{
    class ReadyDatabase
    {
        public int playerId { get; set; }
        public bool isReady { get; set; }

        public ReadyDatabase(int id, bool ready)
        {
            playerId = id;
            isReady = ready;
        }
    }

    class ReadyHandler
    {
        public static int totalReady;

        public static void ReadySetter(int id, bool ready)
        {
            foreach (ReadyDatabase oready in Server.readyDatabase)
            {
                if (id == oready.playerId)
                {
                    oready.isReady = ready;
                }
            }
        }
    }
}
