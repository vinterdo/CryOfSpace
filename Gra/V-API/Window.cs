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
    public abstract class Window : GuiElement
    {
        public Texture2D Tex;


        public Window(Game game, string Name, Rectangle Rect)
            : base(game)
        {
            Tex = Renderer.Singleton.SpaceStationMenuBG;
            this.Rect = Rect;
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
            if (Visible)
            {
                Renderer.Singleton.batch.Draw(Tex, Rect, Color.White);
                base.Draw(gameTime);
            }
        }

        public void Show()
        {
            Visible = true;
        }


        public void Hide()
        {
            Visible = false;
        }


    }
}