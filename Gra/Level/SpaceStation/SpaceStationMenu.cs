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
        public static SpriteFont Font = Renderer.Singleton.Content.Load<SpriteFont>("Font");

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
            int i = 0;
            foreach (BuyOption B in Options.Buy)
            {
                i++;
                Rectangle Slot = new Rectangle((int)(Renderer.Width * 1.5 / 10), (int)(10 + Renderer.Height * i / 10), (int)(Renderer.Width/ 10), (int)(Renderer.Height /10));
                Renderer.Singleton.batch.Draw(Renderer.Singleton.SlotBackground, Slot, Color.White);
                Renderer.Singleton.batch.Draw(B.Item.Tex, Slot, Color.White);

                Rectangle PriceRect = new Rectangle(Slot.X + Slot.Width * 3 / 4, Slot.Y + Slot.Height * 3 / 4, Slot.Width * 1 / 4, Slot.Height * 1 / 4);

                Renderer.Singleton.batch.Draw(Renderer.Singleton.SlotBackground, PriceRect, Color.White);
                Text PriceText = new Text(Game);
                PriceText.Name = B.Price.ToString();
                PriceText.Rect = PriceRect;
                PriceText.Font = Font;
                PriceText.Draw(null);
                //Renderer.Singleton.batch.DrawString(Font, B.Price.ToString(), new Vector2(Slot.X + Slot.Width * 3 / 4, Slot.Y + Slot.Height * 3 / 4), Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.0f);
                
            }

            i = 0;

            foreach (SellOption B in Options.Sell)
            {
                i++;
                Rectangle Slot = new Rectangle((int)(Renderer.Width * 2.5 / 10), (int)(10 + Renderer.Height * i  / 10), (int)(Renderer.Width / 10), (int)(Renderer.Height / 10));
                Renderer.Singleton.batch.Draw(Renderer.Singleton.SlotBackground, Slot, Color.White);
                Renderer.Singleton.batch.Draw(B.Item.Tex, Slot, Color.White);

                Rectangle PriceRect = new Rectangle(Slot.X + Slot.Width * 3 / 4, Slot.Y + Slot.Height * 3 / 4, Slot.Width * 1 / 4, Slot.Height * 1 / 4);

                Renderer.Singleton.batch.Draw(Renderer.Singleton.SlotBackground, PriceRect, Color.White);
                Text PriceText = new Text(Game);
                PriceText.Name = B.Price.ToString();
                PriceText.Rect = PriceRect;
                PriceText.Font = Font;
                PriceText.Draw(null);
               // Renderer.Singleton.batch.DrawString(Font, B.Price.ToString(), new Vector2(Slot.X + Slot.Width * 3 / 4, Slot.Y + Slot.Height * 3 / 4), Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.0f);
            }
        }
    }
}