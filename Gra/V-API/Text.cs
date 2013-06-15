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
    
    public class Text : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Rectangle Rect;
        public SpriteFont Font;

        public string Name;

        public Text(Game game)
            : base(game)
        {
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
            float Scale = 1f;
            if (Font.MeasureString(Name).X > Rect.Width)
            {
                Scale = Rect.Width / Font.MeasureString(Name).X;
            }

            Renderer.Singleton.batch.DrawString(Font, Name, new Vector2(Rect.X, Rect.Y), Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0.0f);
            base.Draw(gameTime);
        }
    }
}