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
    public abstract class Bullet : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Weapon Parent;
        public Bullet(Game game, Weapon Parent)
            : base(game)
        {
            this.Parent = Parent;
        }

        public static Texture2D Tex;
        public Vector2 Position;
        public float Angle;
        public float BasicSpeed;

        public float LifeTime;
        public float CurrentLife;



        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            Position += BasicSpeed * GeneralManager.Singleton.GetVectorFromAngle(Angle);
            CurrentLife += gameTime.ElapsedGameTime.Milliseconds;
            if (CurrentLife > LifeTime)
            {
                Parent.Bullets.Remove(this);
            }
            base.Update(gameTime);
        }

        public void Draw(GameTime gameTime, Vector2 DrawPosition)
        {
            Renderer.Singleton.batch.Draw(Tex, DrawPosition + Position, null, Color.White, -1 *Angle, Vector2.Zero, 1f, SpriteEffects.None, 0);
            base.Draw(gameTime);
        }
    }
}