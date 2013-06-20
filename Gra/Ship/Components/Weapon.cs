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


        public override void Initialize()
        {

            base.Initialize();
        }

        public virtual void Update(GameTime gameTime, Vector2 DrawPosition)
        {
            base.Update(gameTime);
        }

        public void DrawBullets(GameTime gameTime, Vector2 DrawPosition)
        {
            foreach (Bullet B in Bullets)
            {
                B.Draw(gameTime, DrawPosition);
            }
        }
    }
}