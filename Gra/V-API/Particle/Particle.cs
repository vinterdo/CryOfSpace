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
    public class Particle : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Texture2D Tex;
        public Vector2 Position;
        public Vector2 Speed;
        public float Angle;
        public float AngularSpeed;
        public float Opacity;
        public float OpacityChange;
        public float Scale;
        public float ScaleChange;

        public float CurrentLife;
        public float ParticleMaxLife;


        public Particle(Game game, Texture2D Tex, Vector2 Position):base(game)
        {
            this.Tex = Tex;
            this.Position = Position;
        }

        public override void Draw(GameTime gameTime)
        {
            Renderer.Singleton.batch.Draw(Tex, this.Position, null, new Color(Color.White, Opacity), Angle, new Vector2(Tex.Width / 2, Tex.Height / 2), Scale, SpriteEffects.None, 0.1f);
        
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            Position += Speed;
            Angle += AngularSpeed;
            Opacity -= OpacityChange;
            CurrentLife += gameTime.ElapsedGameTime.Milliseconds;
            
        }

        public void DrawOnPosition(GameTime gameTime, Vector2 Offset)
        {
            Renderer.Singleton.batch.Draw(Tex, this.Position - Offset , null, new Color(Color.White, Opacity), Angle, new Vector2(Tex.Width/2, Tex.Height/2), Scale, SpriteEffects.None, 0.1f);
        }
    }
}