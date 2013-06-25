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

    public class MiningLaser : Component
    {
        SmokeEmmiter Emiter;
        Vector2 CurrentOffset;

        public MiningLaser(Game game)
            : base(game)
        {
            Tex = Renderer.Singleton.MiningLaser;
            Name = "Mining Laser";
            Emiter = new SmokeEmmiter(Game, Vector2.Zero);
            CurrentOffset = Vector2.Zero;
        }

        public enum State
        {
            Enabled,
            Disabled,
            Damaged,
            Colddown
        }

        public State CurrentState = State.Disabled;
        public Vector2 ActualPosition;
 

        public Asteroid Target;


        public override void Initialize()
        {

            base.Initialize();
        }

        public void StartMining(Asteroid Target)
        {
            this.Target = Target;
            CurrentState = State.Enabled;
        }

        public void StopMining()
        {
            Target = null;
            CurrentState = State.Disabled;
        }

        public void Update(GameTime gameTime, Vector2 Position)
        {
            ActualPosition = Position;

            if (CurrentState == State.Enabled)
            {
                
            }

            base.Update(gameTime);
        }

        

        public void DrawBeam(GameTime gameTime)
        {
            if (CurrentState == State.Enabled)
            {
                Vector2 TargetPos = Target.DrawPosition + CurrentOffset;

                float angle = (float)Math.Atan2(TargetPos.Y - ActualPosition.Y, TargetPos.X - ActualPosition.X);
                float length = Vector2.Distance(TargetPos, ActualPosition);

                Renderer.Singleton.batch.Draw(Renderer.Singleton.LaserBeam, ActualPosition, null, Color.White,
                           angle, Vector2.Zero, new Vector2(length / 10, 1f + (float)((GeneralManager.Singleton.GetRandom()%1000) / 5000f)),
                           SpriteEffects.None, 1);

                CurrentOffset.X += GeneralManager.Singleton.GetRandom() % 3 - 1;
                CurrentOffset.Y += GeneralManager.Singleton.GetRandom() % 3 - 1;

                Emiter.Position = TargetPos;
                Emiter.Draw(gameTime);
            }
        }
    }
}