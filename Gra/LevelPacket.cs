﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gra
{
    class LevelPacket:Packet
    {
        LevelPacket()
        {
            PacketType = "LevelData";
        }

        public override byte[] GetBufferedData()
        {
            return null;
        }
    }
}
