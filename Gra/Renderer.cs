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

        public SpriteBatch batch;
        Texture2D Blank;
        Texture2D Background1;
        Texture2D Background2;
        public ContentManager Content;
        public static int Height, Width;
        public static Dictionary<string, Animation> Animations;
        public Game Game;

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

        public void InitRenderer(Game game, SpriteBatch spriteBatch, ContentManager Content)
        {
            this.Game = game;
            batch = spriteBatch;
            Height = batch.GraphicsDevice.Viewport.Height;
            Width = batch.GraphicsDevice.Viewport.Width;
            Animations = new Dictionary<string, Animation>();
            this.Content = Content;
        }

        public void LoadContent()
        {
            this.Blank = Content.Load<Texture2D>("Blank");
            this.Background1 = Content.Load<Texture2D>("bg1");
            this.Background2 = Content.Load<Texture2D>("bg2");
            CreateAnimation("test", new Vector2(100, 100), 0.5f, 10);
            Animations["test"].LoadTex(Content.Load<Texture2D>("test"));
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
            int Time = gameTime.TotalGameTime.Milliseconds + gameTime.TotalGameTime.Seconds * 1000 + gameTime.TotalGameTime.Minutes * 60000;

            batch.Draw(Background2, new Rectangle(Time / 1000 % Width, 0, Width, Height), Color.Gray);
            batch.Draw(Background2, new Rectangle(Time / 1000 % Width - Width, 0, Width, Height), Color.Gray);
            
            batch.Draw(Background1, new Rectangle(Time/100%Width, 0 ,Width, Height), Color.White);
            batch.Draw(Background1, new Rectangle(Time / 100 % Width - Width, 0, Width, Height), Color.White);

        }

        public void CreateAnimation(string name, Vector2 FrameSize, float TimePerFrame, int NoFrames)
        {
            Animation a = new Animation(Game);
            a.SetProperties(FrameSize, TimePerFrame, NoFrames);
            Animations.Add(name, a);
        }

        public void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<string, Animation> a in Animations)
            {
                a.Value.Update(gameTime);
            }
        }
    }
}
