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
    class Renderer
    {

        SpriteBatch batch;
        Dictionary<string, Texture2D> TextureStore;

        public Renderer(SpriteBatch spriteBatch)
        {
            batch = spriteBatch;
        }

        public void Line(float width, Vector2 from, Vector2 to, Color color)
        {
            float angle = (float)Math.Atan2(to.Y - from.Y, to.X - from.X);
            float length = Vector2.Distance(from, to);

            batch.Draw(TextureStore.Get("blank"), from, null, color,
                       angle, Vector2.Zero, new Vector2(length, width),
                       SpriteEffects.None, 0);
        }
    }
}
