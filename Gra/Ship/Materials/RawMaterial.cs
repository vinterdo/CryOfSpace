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
    public abstract class RawMaterial 
    {
        public Texture2D Tex;
        public string Name;
        public int AvgPrice;

        public int Count;


        public RawMaterial(int Count)
        {
            this.Count = Count;
            // TODO: Construct any child components here
        }


    }
}