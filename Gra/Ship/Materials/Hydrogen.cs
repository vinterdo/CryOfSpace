using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace CryOfSpace
{
    public class Hydrogen : RawMaterial
    {


        public Hydrogen(int Count)
            : base(Count)
        {
            SetParameters();
        }

        public Hydrogen():base()
        {
            SetParameters();
        }

        public override void SetParameters()
        {
            Tex = Renderer.Textures["Hydrogen"];
            AvgPrice = 10;
            Name = "Hydrogen";
            this.GenerationChance = 0.4f;
            this.MaximalCount = 10;
            this.MinimalCount = 2;
            this.NumberOfOres = 4;
        }

    }
}