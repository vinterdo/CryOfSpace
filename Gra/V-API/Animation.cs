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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Animation : Microsoft.Xna.Framework.DrawableGameComponent
    {
        int NoFrames = 1;
        public Vector2 FrameSize = Vector2.One;
        float TimePerFrame = 1;
        public Vector2 Position = Vector2.Zero;
        public Texture2D Frames;
        float CurrentTime = 0;
        int CurrentFrame = 0;

        public Animation(Game game)
            : base(game)
        {
        }
        

        public override void Initialize()
        {
            Frames = Renderer.Singleton.Content.Load<Texture2D>("indicator");
            Position = Vector2.Zero;

            base.Initialize();
        }
        public void SetProperties(Vector2 FrameSize, float TimePerFrame, int NoFrames)
        {
            this.FrameSize = FrameSize;
            this.TimePerFrame = TimePerFrame;
            this.NoFrames = NoFrames;
        }

        public void LoadTex(Texture2D Tex)
        {
            this.Frames = Tex;
        }

        public void SetPosition(Vector2 Position)
        {
            this.Position = Position;
        }

        public override void Update(GameTime gameTime)
        {
            CurrentTime += (gameTime.ElapsedGameTime.Milliseconds);
            if (CurrentTime > TimePerFrame*1000)
            {
                CurrentTime -= TimePerFrame*1000;
                CurrentFrame += 1;
            }

            if (CurrentFrame >= NoFrames)
            {
                CurrentFrame = 0;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                Renderer.Singleton.batch.Draw(Frames, new Rectangle((int)Position.X, (int)Position.Y, (int)FrameSize.X, (int)FrameSize.Y), new Rectangle((int)FrameSize.X * CurrentFrame, 0, (int)FrameSize.X, (int)FrameSize.Y), Color.White);
                base.Draw(gameTime);
            }
        }



        public void Draw(GameTime gameTime, float Angle, Vector2 Center)
        {
            if (Visible)
            {
                Renderer.Singleton.batch.Draw(Frames, new Rectangle((int)Position.X, (int)Position.Y, (int)FrameSize.X, (int)FrameSize.Y), new Rectangle((int)FrameSize.X * CurrentFrame, 0, (int)FrameSize.X, (int)FrameSize.Y), Color.White, Angle, Center, SpriteEffects.None, 0);
                base.Draw(gameTime);
            }
        }

    }
}