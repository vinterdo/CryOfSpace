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
    public sealed class Renderer
    {

        SpriteBatch batch;
        Texture2D Blank;
        Texture2D Background1;
        public ContentManager Content;

        private Renderer()
        {
        }

        private static Renderer Instance = new Renderer();

        public static Renderer Singleton
        {
            get
            {
                return Instance;
            }
            set
            {
            }
        }

        public void InitRenderer(SpriteBatch spriteBatch, ContentManager Content)
        {
            batch = spriteBatch;
            this.Content = Content;
        }

        public void LoadContent()
        {
            this.Blank = Content.Load<Texture2D>("Blank");
            this.Background1 = Content.Load<Texture2D>("bg1");
        }

        public void Line(float width, Vector2 from, Vector2 to, Color color)
        {
            float angle = (float)Math.Atan2(to.Y - from.Y, to.X - from.X);
            float length = Vector2.Distance(from, to);

            batch.Draw(Blank, from, null, color,
                       angle, Vector2.Zero, new Vector2(length/2, width),
                       SpriteEffects.None, 0);
        }

        public void RenderBackground(GameTime gameTime)
        {

            batch.Draw(Background1, new Rectangle(((gameTime.TotalGameTime.Milliseconds)/100 + gameTime.TotalGameTime.Seconds*10)%640, 0 ,640, 480), Color.White);
            batch.Draw(Background1, new Rectangle(((gameTime.TotalGameTime.Milliseconds) / 100 + gameTime.TotalGameTime.Seconds * 10) % 640 -640, 0, 640, 480), Color.White);
        }
    }
}
