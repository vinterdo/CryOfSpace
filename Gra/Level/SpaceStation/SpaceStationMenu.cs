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
    class SpaceStationMenu : Window
    {
        public SpaceStationMenu(Game game)
            : base(game, "SpaceStationMenuBG", new Rectangle(Renderer.Width/10, Renderer.Height/10, (Renderer.Width*8)/10,(Renderer.Height*8)/10))
        {
            Visible = false;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public void DrawTrade(TradeOptions Options)
        {
            foreach (BuyOption B in Options.Buy)
            {
                Rectangle Slot = new Rectangle((int)(Renderer.Width * 1.5 / 10), (int)(Renderer.Height * 1.5 / 10), (int)(Renderer.Width/ 10), (int)(Renderer.Height /10));
                Renderer.Singleton.batch.Draw(Renderer.Singleton.SlotBackground, Slot, Color.White);
            }

            foreach (SellOption B in Options.Sell)
            {
                Rectangle Slot = new Rectangle((int)(Renderer.Width * 1.5 / 10), (int)(Renderer.Height * 2.5 / 10), (int)(Renderer.Width / 10), (int)(Renderer.Height / 10));
                Renderer.Singleton.batch.Draw(Renderer.Singleton.SlotBackground, Slot, Color.White);
            }
        }
    }
}