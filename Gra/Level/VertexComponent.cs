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
    public abstract class VertexComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Vector2 Position;
        public Vector2 DrawPosition;
        public Vector2 Size;
        public Texture2D Tex;
        public float Angle = 0.0f;
        public float AngularSpeed = 0.0f;

        public VertexComponent(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            Angle += AngularSpeed;
            base.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);

        }

        public void SetDrawPosition(Vector2 Offset)
        {
            DrawPosition = Position - Offset;
        }

        public Rectangle GetRect()
        {
            return new Rectangle((int)DrawPosition.X - Tex.Width / 2, (int)DrawPosition.Y - Tex.Height/2, (int)Tex.Width, (int)Tex.Height);
        }
    }
}