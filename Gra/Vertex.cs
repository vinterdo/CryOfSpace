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
    class Vertex
    {
        public Vector2 Position;
        public Texture2D Tex;
        public void LoadTex(Texture2D _Tex)
        {
            Tex = _Tex;
        }

        public void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Tex, Position - new Vector2(Tex.Width/2, Tex.Height/2), Color.White);
        }

    }
}
