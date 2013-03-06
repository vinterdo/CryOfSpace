using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    [Serializable]
    public class Vertex:DrawableGameComponent,ICloneable
    {
        public Vector2 Position;
        Texture2D Tex;
        SpriteBatch spriteBatch;

        

        public Vertex (Game game, Vector2 Pos, Texture2D Tex):base(game)
        {
            this.Position = Pos;
            this.Tex = Tex;
            spriteBatch = Renderer.Singleton.batch;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(Tex, (Position - new Vector2(Tex.Width / 2, Tex.Height / 2)) * new Vector2((float)(Renderer.Width - 100) / 500, (float)(Renderer.Height - 100) / 500), Color.White);
            base.Draw(gameTime);
        }

        public object Clone()
        {
            return new Vertex(Game, this.Position, this.Tex);

        }

        public float GetLenghtFrom(Vertex Vertex)
        {
            return Vector2.Distance(this.Position, Vertex.Position);
        }

    }
}
