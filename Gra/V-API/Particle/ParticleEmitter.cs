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
    public class ParticleEmitter : Microsoft.Xna.Framework.GameComponent
    {
        public List<Particle> Particles;

        public Texture2D PartTex;
        public Vector2 Position;
        public float Direction; 
        public float DirectionOffset;
        public float Speed;
        public float SpeedOffset;
        public float AngularSpeed;
        public float Opacity;
        public float OpacityChange;
        public float OpacityOffset;

        public float GenerationChance;

        public float Scale;
        public float ScaleChange;
        public float ScaleOffset;



        public ParticleEmitter(Game game)
            : base(game)
        {
            Particles = new List<Particle>();
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Particle P in Particles)
            {
                P.Update(gameTime);
                
            }

            if (GenerationChance > (GeneralManager.Singleton.GetRandom() % 1000f) / 1000f)
            {
                Particle Tmp = new Particle(Game, PartTex, Position);
                Tmp.Angle = 0.0f;
                Tmp.Opacity = Opacity - ((GeneralManager.Singleton.GetRandom() * 1000f) % (OpacityOffset * 1000f)) / 1000f;
                Tmp.Speed = GeneralManager.Singleton.GetVectorFromAngle(Direction + (float)(GeneralManager.Singleton.GetRandom() % (DirectionOffset * 1000f)) / 1000f) * (Speed + ((GeneralManager.Singleton.GetRandom() * 1000f) % (SpeedOffset * 1000f)) / 1000f);
                Tmp.OpacityChange = OpacityChange;
                Tmp.AngularSpeed = AngularSpeed;
                Tmp.Scale = Scale + (float)((GeneralManager.Singleton.GetRandom() % (ScaleOffset * 1000f))) / 1000f;
                Tmp.ScaleChange = ScaleChange;

                Particles.Add(Tmp);
            }


            base.Update(gameTime);
        }

        public void CrateParticle()
        {
            Particle Tmp = new Particle(Game, PartTex, Position);
        }
    }
}