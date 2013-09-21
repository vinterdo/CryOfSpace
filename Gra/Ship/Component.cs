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
    [Serializable]

    public abstract class Component : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Vector2 Position;
        public Vector2 Size = Vector2.One;
        public Texture2D Tex;

        public string Name;
        //public Animation Anim;

        public Component(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
        }

        public override void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, RenderTarget2D Render)
        {
        }
    }
}