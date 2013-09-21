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

    public class Text : GuiElement
    {
        public SpriteFont Font;

        public string Name;

        public Text(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
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
            float ScaleX = 1f;
            float ScaleY = 1f;
            
            ScaleX = Rect.Width / Font.MeasureString(Name).X;
            ScaleY = Rect.Height / Font.MeasureString(Name).Y;

            if (ScaleX < ScaleY)
            {
                float Offset = (Rect.Height / 2f) - (Font.MeasureString(Name).Y * ScaleX) / 2; 
                Renderer.Singleton.batch.DrawString(Font, Name, new Vector2(Rect.X, Rect.Y + Offset), Color.White, 0.0f, Vector2.Zero, ScaleX, SpriteEffects.None, 0.0f);
            }
            else
            {
                float Offset = (Rect.Width / 2f) - (Font.MeasureString(Name).X * ScaleY) / 2;
                Renderer.Singleton.batch.DrawString(Font, Name, new Vector2(Rect.X + Offset, Rect.Y), Color.White, 0.0f, Vector2.Zero, ScaleY, SpriteEffects.None, 0.0f);
            }
            

            
            base.Draw(gameTime);
        }

        public override void CatchClick()
        {

        }
    }
}