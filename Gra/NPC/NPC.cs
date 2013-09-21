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
    public abstract class NPC : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Ship Ship;
        public AI AI;

        public NPC(Game game, Ship Ship, VertexScreen Vertex)
            : base(game)
        {
            this.Ship = Ship;
            this.Ship.CurrentVertex = Vertex;
            Ship.CurrentVertex.Ships.Add(Ship);
            
            GeneralManager.NPCs.Add(this);
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            AI.Update(gameTime);
            //Ship.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Ship.DrawOutside(gameTime);
            base.Draw(gameTime);
        }
    }
}