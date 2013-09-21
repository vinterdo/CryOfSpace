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
    public class Weapon_GaussCannonB50 : Weapon
    {
        public Weapon_GaussCannonB50(Game game)
            : base(game)
        {
            WeaponMode = Mode.Cursor;
            Tex = Renderer.Singleton.GaussCannonB50;
            Bullets = new List<Bullet>();
            Name = "Gauss B50 Cannon";
            MaxHeat = 4000;
            CoolingPerSecond = 200;
            HeatPerShoot = 20;
            ShootAnim = new Animation(game);
            ShootAnim.Frames = Renderer.Singleton.Content.Load<Texture2D>("GaussCannonB50Animation");
            ShootAnim.SetProperties(new Vector2(135, 30), 0.04f, 7);
            ShootColddown = 200;
            Damage = 2;
        } 

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            for (int i = 0; i < Bullets.Count; i++ )
            {
                Bullets[i].Update(gameTime);
            }

            Heat -= CoolingPerSecond * (gameTime.ElapsedGameTime.Milliseconds/1000.0f);
            if (Heat < 0)
            {
                Heat = 0;
                WeaponState = State.Normal;
            }

            if (Heat > MaxHeat)
            {
                WeaponState = State.Overheat;
            } 

            

            base.Update(gameTime);
        }

        public override void Shoot(Vector2 Target)
        {
            if (Heat < MaxHeat && WeaponState == State.Normal && CurrentColddown == 0)
            {
                Bullet Tmp = new Bullet_Gauss(Game, GeneralManager.Singleton.GetAngleFromVector(Target), this);
                Tmp.CurrentLife = 0;
                Tmp.Position = Position;

                Bullets.Add(Tmp);

                Heat += HeatPerShoot;

                CurrentColddown = ShootColddown;

                GeneralManager.SoundManager.PlaySound("Gauss_Cannon", 0.2f);
            }
        }

    }
}