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
    public abstract class VertexComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Vector2 Position;
        public Vector2 DrawPosition;
        public Vector2 Size;
        public Texture2D Tex;

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
    }
}