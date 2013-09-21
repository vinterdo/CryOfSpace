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
    public class Wreck : VertexComponent
    {
        public Wreck(Game game)
            : base(game)
        {
            
        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Renderer.Singleton.batch.Draw(Tex, DrawPosition, null, Color.White, Angle, new Vector2(Tex.Width / 2, Tex.Height / 2), Vector2.One, SpriteEffects.None, 1.0f);
            base.Draw(gameTime);
        }
    }
}