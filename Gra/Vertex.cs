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
    public class Vertex:ICloneable
    {
        public Vector2 Position;
        public Texture2D Tex;
        public void LoadTex(Texture2D _Tex)
        {
            Tex = _Tex;
        }

        Vertex (Vector2 Pos, Texture2D Tex)
        {
            this.Position = Pos;
            this.Tex = Tex;
        }

        public Vertex()
        { }

        public object Clone()
        {
            return new Vertex(this.Position, this.Tex);

        }

        public float GetLenghtFrom(Vertex Vertex)
        {
            return Vector2.Distance(this.Position, Vertex.Position);
        }

        public void Render(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Tex, (Position - new Vector2(Tex.Width / 2, Tex.Height / 2)) * new Vector2((float)(Renderer.Width - 100) / 500, (float)(Renderer.Height - 100) / 500) , Color.White);
        }

    }
}
