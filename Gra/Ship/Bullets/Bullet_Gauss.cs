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
    public class Bullet_Gauss : Bullet
    {
        

        public Bullet_Gauss(Game game, float Angle, Weapon Parent)
            : base(game, Parent)
        {
            BasicSpeed = 7.0f;
            Tex = Renderer.Singleton.GaussBullet;
            this.Angle = Angle;
            this.LifeTime = 1500;
            
        }

        
        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }
    }
}