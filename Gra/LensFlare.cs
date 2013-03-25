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
    public class LensFlare : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public List<Texture2D> Lenses;
        public Vector2 LightPosition;
        public float Distance;
        float Transparency;

        public LensFlare(Game game)
            : base(game)
        {
            Lenses = new List<Texture2D>();
        }

        public override void Initialize()
        {
            Lenses.Add(Renderer.Singleton.Content.Load<Texture2D>("lensflare1"));
            Lenses.Add(Renderer.Singleton.Content.Load<Texture2D>("lensflare2"));
            Lenses.Add(Renderer.Singleton.Content.Load<Texture2D>("lensflare3"));
            base.Initialize();
        }

        public void Draw(GameTime gameTime, Vector2 CameraPos, Vector2 Offset)
        {
            Vector2 Center = new Vector2(Renderer.Width / 2, Renderer.Height / 2);

            Vector2 Normalized = Center - (LightPosition-CameraPos);
            Normalized.Normalize();

            int i = 0;
            foreach (Texture2D Tex in Lenses)
            {
                Renderer.Singleton.batch.Draw(Tex, Normalized * (Distance / Lenses.Count) * i + (LightPosition - CameraPos), new Color(Color.White, 128));
                i++;
            }

        }
    }
}