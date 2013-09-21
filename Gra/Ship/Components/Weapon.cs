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
    public class Weapon : Component
    {
        public Weapon(Game game)
            : base(game)
        {
            List<Bullet> Bullets = new List<Bullet>();
        }

        

        public enum Mode
        {
            Auto,
            Select,
            Cursor,
            Off
        }
        public enum State
        {
            Normal,
            Overheat,
            Damaged
        }

        public Mode WeaponMode;
        public State WeaponState = State.Normal;

        public List<Bullet> Bullets;

        public float CurrentAngle;
        public float Heat;
        public int MaxHeat;
        public int CoolingPerSecond;
        public int HeatPerShoot;
        public int ShootColddown;
        public int CurrentColddown;

        public int Damage;

        public Animation ShootAnim;



        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            
            CurrentColddown -= gameTime.ElapsedGameTime.Milliseconds;
            if (CurrentColddown < 0)
            {
                CurrentColddown = 0;
            }
            base.Update(gameTime);
        }

        public void DrawBullets(GameTime gameTime, Vector2 DrawPosition)
        {
            foreach (Bullet B in Bullets)
            {
                B.Draw(gameTime, DrawPosition);
            }
        }

        public virtual void Shoot(Vector2 Target)
        {
        }

        public void DetectCollisions(VertexScreen V, Ship Owner)
        {
            foreach (Bullet B in Bullets)
            {
                foreach (Ship S in V.Ships)
                {
                    if (S.Hull.Mask.CheckCollision(B.Position + Owner.Position - S.Position + S.OutsideView.FrameSize/2) && Owner != S)
                    {
                        B.CurrentLife = B.LifeTime;
                        S.HitPoints -= Damage;
                        S.CurrentVertex.Effect.Parameters["BloomIntensity"].SetValue(1.5f);
                    }
                }
            }
        }
    }
}