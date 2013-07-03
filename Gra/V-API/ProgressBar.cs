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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ProgressBar : GuiElement
    {
        public ProgressBar(Game game, Rectangle Rect, Texture2D Background, Texture2D Foreground, Texture2D Overlay)
            : base(game)
        {
            this.Rect = Rect;
            this.Background = Background;
            this.Foreground = Foreground;
            this.Overlay = Overlay;

        }

        public float Progress = 0;
        public Texture2D Background;
        public Texture2D Foreground;
        public Texture2D Overlay;

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
            Renderer.Singleton.batch.Draw(Background, Rect, Color.White);
            Rectangle BarRec = new Rectangle(Rect.X, Rect.Y, (int)(Rect.Width * Progress), Rect.Height);
            Rectangle SourceRect = new Rectangle(0,0, (int)(Foreground.Width * Progress), Foreground.Height);

            Renderer.Singleton.batch.Draw(Foreground, BarRec, SourceRect, Color.White);

            Renderer.Singleton.batch.Draw(Overlay, Rect, Color.White);


            base.Draw(gameTime);
        }

        public override void CatchClick()
        {

        }
    }
}