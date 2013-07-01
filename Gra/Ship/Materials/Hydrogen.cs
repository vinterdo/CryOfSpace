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
    public class Hydrogen : RawMaterial
    {


        public Hydrogen(int Count)
            : base(Count)
        {
            Tex = Renderer.Textures["Hydrogen"];
            AvgPrice = 10;
            Name = "Hydrogen";
        }
        

        public Hydrogen():base()
        {
            Tex = Renderer.Textures["Hydrogen"];
            AvgPrice = 10;
            Name = "Hydrogen";
        }

    }
}