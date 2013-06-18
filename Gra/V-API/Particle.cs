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
    public class ParticleState
    {
        public Color Color;
        public float Angle;
        public float Size;
        public float Opacity;
    }

    public class Particle : Microsoft.Xna.Framework.DrawableGameComponent, IDisposable
    {
        public ParticleState CurrentState;
        public ParticleState ChangeState;

        public Vector2 Position;
        public Vector2 Acceleration;
        public Vector2 Speed;

        public Texture2D Tex;
        
        public Vector2 Scale;
        public float TimeToDie;
        public float AliveTime;



        public Particle(Game game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            Speed *= Acceleration;
            Position += Speed;

            TimeToDie -= gameTime.ElapsedGameTime.Milliseconds;

            if (TimeToDie < 0)
            {
                this.Dispose();
            }


            base.Update(gameTime);
        }

        public void CalculateChange()
        {
            ChangeState.Color = new Color((ChangeState.Color.ToVector4() - CurrentState.Color.ToVector4()) / AliveTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Renderer.Singleton.batch.Draw(Tex, Position, new Color(CurrentState.Color, CurrentState.Opacity));
            base.Draw(gameTime);
        }
    }
}