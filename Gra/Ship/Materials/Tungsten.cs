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


namespace Gra
{
    public class Tungsten : RawMaterial
    {
        public Tungsten(int Count)
            : base(Count)
        {
            SetParameters();
        }

        public Tungsten()
            : base()
        {
            SetParameters();
            // TODO: Construct any child components here
        }

        public override void SetParameters()
        {
            Tex = Renderer.Textures["Tungsten"];
            AvgPrice = 20;
            Name = "Tungsten";
            this.GenerationChance = 0.4f;
            this.MaximalCount = 10;
            this.MinimalCount = 2;
            this.NumberOfOres = 4;
        }

    }
}