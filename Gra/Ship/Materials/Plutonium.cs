using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gra
{
    class Plutonium:RawMaterial
    {

        public Plutonium(int Count):base(Count)
        {
            Tex = Renderer.Textures["Plutonium"];
            AvgPrice = 50;
            Name = "Plutonium";

        }

        public Plutonium()
            : base()
        {
            Tex = Renderer.Textures["Plutonium"];
            AvgPrice = 50;
            Name = "Plutonium";
            // TODO: Construct any child components here
        }
    }
}
