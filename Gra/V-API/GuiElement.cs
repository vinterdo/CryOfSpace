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
    public abstract class GuiElement : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Rectangle Rect;

        public GuiElement(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if(GeneralManager.Singleton.CheckLMB())
            {
                CatchClick();
            }
            base.Update(gameTime);
        }

        public abstract void CatchClick();


        public void Kill()
        {
        }
    }
}