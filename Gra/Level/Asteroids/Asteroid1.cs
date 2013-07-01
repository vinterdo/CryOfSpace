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
    public class Asteroid1 : Asteroid
    {
        public Asteroid1(Game game, Vector2 Position)
            : base(game)
        {
            Tex = Renderer.Singleton.Asteroid1;
            Size = new Vector2(100, 100);
            this.Position = Position;
            MiningChance = 200;
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
            Renderer.Singleton.batch.Draw(Tex, DrawPosition, null, Color.White, Angle, new Vector2(Tex.Width / 2, Tex.Height / 2), Vector2.One, SpriteEffects.None, 1.0f);

            base.Draw(gameTime);
        }
    }
}