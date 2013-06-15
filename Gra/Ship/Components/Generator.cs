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
    public class Generator : Component
    {
        public Generator(Game game)
            : base(game)
        {
            Tex = Renderer.Singleton.Content.Load<Texture2D>("Generator");

            Name = "Generator";
            //Anim = Renderer.Animations["Generator"];
            // TODO: Construct any child components here
        }

        public override void Initialize()
        {
            
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Renderer.Singleton.batch.Draw(Tex, Position, Color.White);
           // Anim.Draw(gameTime, 0.0f, Position);
            base.Draw(gameTime);
        }
    }
}