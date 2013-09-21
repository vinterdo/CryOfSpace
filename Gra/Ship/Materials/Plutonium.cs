using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryOfSpace
{
    class Plutonium:RawMaterial
    {

        public Plutonium(int Count):base(Count)
        {
            SetParameters();

        }

        public Plutonium()
            : base()
        {
            SetParameters();
            // TODO: Construct any child components here
        }

        public override void SetParameters()
        {
            Tex = Renderer.Textures["Plutonium"];
            AvgPrice = 50;
            Name = "Plutonium";
            this.GenerationChance = 0.4f;
            this.MaximalCount = 10;
            this.MinimalCount = 2;
            this.NumberOfOres = 4;
        }
    }
}
