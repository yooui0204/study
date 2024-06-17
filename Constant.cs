using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetLib
{
    public class Constant
    {
        public enum HEADER
        {
            //init head
            CONNECT = 1636,
            DISCONNECT,

            //scene change head
            BACK_TO_LOBBY,
            GOTO_LEADERBOARD,
            GOTO_OPTION,
            START_MATCHING,
            STOP_MATCHING,
            ESTABLISH_MATCH,

            //in game head
            START_GAME,
            CUT,
            PULL_START,
            PULL_END,
            MAKE_ANCHOR,
            SYNC_POS,
            COLLISION,
            SPIKE,
            ADD_EDGE,
            REMOVE_EDGE,
            BOOST,
            SLOW,
            CHASE,
            BOOM,
            BLIND,
            SUPER,
            SURRENDER,
            FINISH_GAME,

            HEART_BEAT
        }

        public struct NAME_SCORE_STRUCT
        {
            public sbyte[] name;
            public int score;

            public NAME_SCORE_STRUCT(sbyte[] name, int score)
            {
                this.name = name;
                this.score = score;
            }
        }

        public const int FIXED_PACKET_SIZE = 64;

        public const int MAP_LENGTH = 28;

        public const int LEADERBOARD_PAGE_SIZE = 3;
    }
}
