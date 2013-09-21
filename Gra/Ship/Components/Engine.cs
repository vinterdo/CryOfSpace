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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Engine : Component
    {
        
        public Engine(Game game)
            : base(game)
        {
            Tex = Renderer.Singleton.Content.Load<Texture2D>("Engine");
            Name = "Engine";
            //Anim = Renderer.Animations["engine"];
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
            //Anim.Draw(gameTime, 0.0f, Position);
            base.Draw(gameTime);
        }
    }
}