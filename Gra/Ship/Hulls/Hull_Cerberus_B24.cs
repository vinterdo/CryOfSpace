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
    public class Hull_Cerberus_B24 : Hull
    {
        public Hull_Cerberus_B24():base()
        {
            this.Name = "Cerberus B24";

            this.BasicHull = 70.0f;
            this.Center = new Vector2(70, 87);

            this.Explosion = new RawAnimation();
            this.HullBreachs = new List<Vector2>();
            this.HullModifier = 1.1f;

            this.InsideView = new RawAnimation();
            this.InsideView.TextureName = "Cerberus_B24_Base";
            this.InsideView.SetProperties(new Vector2(175, 175), 1000, 1);
            this.InsideView.CreateAnimation();

            this.OutsideView = new RawAnimation();
            this.OutsideView.TextureName = "Cerberus_B24_Base";
            this.OutsideView.SetProperties(new Vector2(175, 175), 1000, 1);
            this.OutsideView.CreateAnimation();

            this.OutsideColor = new RawAnimation();
            this.OutsideColor.TextureName = "Cerberus_B24_Color";
            this.OutsideColor.SetProperties(new Vector2(175, 175), 1000, 1);
            this.OutsideColor.CreateAnimation();

            this.Mask = new Mask(Renderer.Singleton.Content.Load<Texture2D>("Cerberus_B24_Mask"), new Vector2(175, 175));

            this.SizeX = 175;
            this.SizeY = 175;
            this.SpeedModifier = 1.0f;
            this.Weight = 100.0f;
            this.BasicHull = 400;

            this.SlotsNum = 7;

            this.Wreck = new Wreck_Cerberus_B24(Renderer.Singleton.Game);


            this.Initialize();
            this.Slots[0].Position = new Vector2(82, 70);
            this.Slots[1].Position = new Vector2(82, 225);
            this.Slots[2].Position = new Vector2(71, 133);
            this.Slots[3].Position = new Vector2(101, 133);
            this.Slots[4].Position = new Vector2(71, 163);
            this.Slots[5].Position = new Vector2(101, 163);
            this.Slots[6].Position = new Vector2(184, 133);

            this.AccelerationEngines.Add(new EngineSmokeEmmiter(Renderer.Singleton.Game, Vector2.Zero, 0f));
            this.AccelerationEngines.Add(new EngineEmmiter(Renderer.Singleton.Game, Vector2.Zero, 0f));
            this.AccelerationOffset.Add(new Vector2(-50, 20));
            this.AccelerationOffset.Add(new Vector2(-50, 20));

            this.AccelerationEngines.Add(new EngineSmokeEmmiter(Renderer.Singleton.Game, Vector2.Zero, 0f));
            this.AccelerationEngines.Add(new EngineEmmiter(Renderer.Singleton.Game, Vector2.Zero, 0f));
            this.AccelerationOffset.Add(new Vector2(-50, -18));
            this.AccelerationOffset.Add(new Vector2(-50, -18));

            this.WeaponPositions.Add(new Vector2(-70, 25));
            this.WeaponPositions.Add(new Vector2(-70, -53));
        }
    }
}