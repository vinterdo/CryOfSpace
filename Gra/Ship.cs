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
            OutsideView.Update(gameTime);
            base.Update(gameTime);
        }

        public void DrawOutside(GameTime gameTime)
        {
            OutsideView.Draw(gameTime);
        }
    }
}