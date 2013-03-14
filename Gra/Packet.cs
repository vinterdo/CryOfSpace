using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gra
{
    public abstract class Packet
    {
        protected string PacketType;

        public abstract byte[] GetBufferedData();
    }
}
