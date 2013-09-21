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
    public class ParticleEmitter : Microsoft.Xna.Framework.GameComponent
    {
        public List<Particle> Particles;

        public Texture2D PartTex;
        public Vector2 Position;
        public float PositionOffset;
        public float Direction; 
        public float DirectionOffset;
        public float Speed;
        public float SpeedOffset;
        public float AngularSpeed;
        public float AngleOffset;
        public float Opacity;
        public float OpacityChange;
        public float OpacityOffset;

        public float ParticleMaxLife;

        public float GenerationChance;

        public float Scale;
        public float ScaleChange;
        public float ScaleOffset;



        public ParticleEmitter(Game game)
            : base(game)
        {
            ParticleWorld.Register(this);
            Particles = new List<Particle>();
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public int ParticleCount
        {
            get
            {
                return Particles.Count;
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Particle P in Particles)
            {
                P.Draw(gameTime, this.Position);
            }
        }

        public void Draw(GameTime gameTime, Vector2 Offset)
        {
            foreach (Particle P in Particles)
            {
                P.DrawOnPosition(gameTime, Offset);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                for(int i =0; i< Particles.Count; i++)
                {
                    Particles[i].Update(gameTime);
                    if (Particles[i].CurrentLife > Particles[i].ParticleMaxLife)
                    {
                        Particles.Remove(Particles[i]);
                    }

                }

                if (GenerationChance > (GeneralManager.Singleton.GetRandom() % 1000f) / 1000f)
                {
                    Particle Tmp = new Particle(Game, PartTex, /*Position +*/ new Vector2((float)((GeneralManager.Singleton.GetRandom() % (PositionOffset * 1000f))) / 1000f, (float)((GeneralManager.Singleton.GetRandom() % (PositionOffset * 1000f))) / 1000f));
                    Tmp.Angle = (float)((GeneralManager.Singleton.GetRandom() % (AngleOffset * 1000f))) / 1000f;
                    Tmp.Opacity = Opacity - ((GeneralManager.Singleton.GetRandom() * 1000f) % (OpacityOffset * 1000f)) / 1000f;
                    Tmp.Speed = GeneralManager.Singleton.GetVectorFromAngle(Direction + (float)(GeneralManager.Singleton.GetRandom() % (DirectionOffset * 1000f)) / 1000f) * (Speed + ((GeneralManager.Singleton.GetRandom() * 1000f) % (SpeedOffset * 1000f)) / 1000f);
                    Tmp.OpacityChange = OpacityChange;
                    Tmp.AngularSpeed = AngularSpeed;
                    Tmp.Scale = Scale + (float)((GeneralManager.Singleton.GetRandom() % (ScaleOffset * 1000f))) / 1000f;
                    Tmp.ScaleChange = ScaleChange;
                    Tmp.ParticleMaxLife = ParticleMaxLife;


                    int Index;
                    if (Particles.Count != 0)
                    {
                        Index = GeneralManager.Singleton.GetRandom() % Particles.Count;
                        Particle Switch = Particles[Index];
                        Particles[Index] = Tmp;
                        Particles.Add(Switch);
                    }
                    else
                    {
                        Particles.Add(Tmp);
                    }

                }


                base.Update(gameTime);
            }
        }

    }
}