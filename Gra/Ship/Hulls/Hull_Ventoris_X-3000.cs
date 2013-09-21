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
    class Hull_Ventoris_X_3000:Hull
    {
        public Hull_Ventoris_X_3000()
            : base()
        {
            this.Name = "Test";

            this.BasicHull = 100.0f;
            this.Center = new Vector2(150, 150);

            this.Explosion = new RawAnimation();
            this.HullBreachs = new List<Vector2>();
            this.HullModifier = 1.0f;

            this.InsideView = new RawAnimation();
            this.InsideView.TextureName = "ship2-inside";
            this.InsideView.SetProperties(new Vector2(300, 300), 1000, 1);
            this.InsideView.CreateAnimation();

            this.OutsideView = new RawAnimation();
            this.OutsideView.TextureName = "ship2";
            this.OutsideView.SetProperties(new Vector2(300, 300), 1000, 1);
            this.OutsideView.CreateAnimation();

            

            this.SizeX = 300;
            this.SizeY = 300;
            this.SpeedModifier = 1.0f;
            this.Weight = 100.0f;

            this.SlotsNum = 9;


            this.Initialize();
            this.Slots[0].Position = new Vector2(82, 70);
            this.Slots[1].Position = new Vector2(82, 225);
            this.Slots[2].Position = new Vector2(71, 133);
            this.Slots[3].Position = new Vector2(101, 133);
            this.Slots[4].Position = new Vector2(71, 163);
            this.Slots[5].Position = new Vector2(101, 163);
            this.Slots[6].Position = new Vector2(184, 133);
            this.Slots[7].Position = new Vector2(184, 162);
            this.Slots[8].Position = new Vector2(227, 148);
        }
    }
}
