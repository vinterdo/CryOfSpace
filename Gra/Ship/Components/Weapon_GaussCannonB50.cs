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
    public class Weapon_GaussCannonB50 : Weapon
    {
        public Weapon_GaussCannonB50(Game game)
            : base(game)
        {
            WeaponMode = Mode.Cursor;
            Tex = Renderer.Singleton.GaussCannonB50;
            Bullets = new List<Bullet>();
            Name = "Gauss B50 Cannon";
        } 

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime, Vector2 DrawPosition)
        {
            if (GeneralManager.Singleton.IsLMBDown && WeaponMode == Weapon.Mode.Cursor)
            {
                Bullet Tmp = new Bullet_Gauss(Game, GeneralManager.Singleton.GetAngleFromVector(GeneralManager.Singleton.MousePos - DrawPosition));
                Tmp.CurrentLife = 0;
                Tmp.Position = Position;

                Bullets.Add(Tmp);

            }

            foreach (Bullet B in Bullets)
            {
                B.Update(gameTime);
            }
            base.Update(gameTime);
        }

        

    }
}