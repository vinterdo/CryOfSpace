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
    
    public class Ship : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Hull Hull;
        //List<Component> Components = new List<Component>();
        //List<Crew> Crew = new List<Crew>();
        //List<Distaster> Distasters = new List<Distaster>();


        EnergyConduit[][] EnergyConduits;
        PlasmaConduit[][] PlasmaConduits;
        CoolantConduit[][] CoolantConduits;
        DataConduit[][] DataConduits;
        OxygenConduit[][] OxygenConduits;
        AntimaterConduit[][] AntimaterConduits;

        public Animation OutsideView;
        public Animation InsideView;
        public Animation ConduitsView;
        public Animation Explosion;
        public Animation Wreck;

        float Angle;
        Vector2 Position;
        Vector2 Speed;
        

        public Ship(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            Hull.OutsideView.RegisterAnimation();
            OutsideView = Renderer.Animations["ship2"];
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (GeneralManager.Singleton.keyboardState.IsKeyDown(Keys.W))
            {
                Speed += GeneralManager.Singleton.GetVectorFromAngle(-1 * Angle + (float)Math.PI/2) / 1000;
            }
            if (GeneralManager.Singleton.keyboardState.IsKeyDown(Keys.D))
            {
                Angle += 0.002f;
            }
            if (GeneralManager.Singleton.keyboardState.IsKeyDown(Keys.A))
            {
                Angle -= 0.002f;
            }
            if (GeneralManager.Singleton.keyboardState.IsKeyDown(Keys.S))
            {
                Speed *= 0.95f;
            }

            Position += Speed;
            OutsideView.Position = Position;
            OutsideView.Update(gameTime);
            base.Update(gameTime);
        }

        public void DrawOutside(GameTime gameTime)
        {
            OutsideView.Draw(gameTime, Angle, Hull.Center);
        }
    }
}