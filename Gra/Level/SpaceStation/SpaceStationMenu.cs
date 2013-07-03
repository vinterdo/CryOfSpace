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

        public enum Mode
        {
            Main,
            TradeMaterials,
            TradeComponents
        }

        public Mode MenuMode = Mode.Main;

        public static SpriteFont Font = Renderer.Fonts["Coalition"];

        public List<RawMaterial> SellMaterials;
        public List<RawMaterial> BuyMaterials;


        public SpaceStationMenu(Game game)
            : base(game, "SpaceStationMenuBG", new Rectangle(Renderer.Width/10, Renderer.Height/10, (Renderer.Width*8)/10,(Renderer.Height*8)/10))
        {
            Visible = false;
            SellMaterials = new List<RawMaterial>();
            BuyMaterials = new List<RawMaterial>();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void Update(GameTime gameTime, TradeOptions Options)
        {
            if (MenuMode == Mode.TradeComponents)
            {
                int i = 0;
                foreach (BuyOption B in Options.Buy)
                {
                    i++;
                    Rectangle Slot = new Rectangle((int)(Renderer.Width * 1.5 / 10), (int)(10 + Renderer.Height * i / 10), (int)(Renderer.Width / 10), (int)(Renderer.Height / 10));
                    if (GeneralManager.Singleton.CheckLMB() && GeneralManager.Singleton.CheckCollision(GeneralManager.Singleton.MousePos, Slot))
                    {
                        B.OnClick();
                        Options.Buy.Remove(B);
                        break;
                    }
                }

                i = 0;

                foreach (SellOption B in Options.Sell)
                {
                    i++;
                    Rectangle Slot = new Rectangle((int)(Renderer.Width * 2.5 / 10), (int)(10 + Renderer.Height * i / 10), (int)(Renderer.Width / 10), (int)(Renderer.Height / 10));
                    if (GeneralManager.Singleton.CheckLMB() && GeneralManager.Singleton.CheckCollision(GeneralManager.Singleton.MousePos, Slot))
                    {
                        B.OnClick();
                        Options.Sell.Remove(B);
                        break;
                    }
                }
            }
            if (MenuMode == Mode.TradeMaterials)
            {
                for (int i = 0; i < SellMaterials.Count; i++)
                {
                    Rectangle Slot = Renderer.GetPartialRect(0.2f, 0.2f + 0.05f * i, 0.05f, 0.05f);

                    if (GeneralManager.Singleton.CheckLMB())
                    {
                        if (Slot.Contains((int)GeneralManager.Singleton.MousePos.X, (int)GeneralManager.Singleton.MousePos.Y))
                        {
                            SellMaterials[i].Count -= 1;
                            GeneralManager.Singleton.CurrentPlayer.Money += SellMaterials[i].AvgPrice;

                            if (SellMaterials[i].Count <= 0)
                            {
                                SellMaterials.Remove(SellMaterials[i]);
                            }
                        }
                    }
                }

                for (int i = 0; i < BuyMaterials.Count; i++)
                {
                    Rectangle Slot = Renderer.GetPartialRect(0.3f, 0.2f + 0.05f * i, 0.05f, 0.05f);

                    if (GeneralManager.Singleton.CheckLMB())
                    {
                        if (Slot.Contains((int)GeneralManager.Singleton.MousePos.X, (int)GeneralManager.Singleton.MousePos.Y))
                        {
                            BuyMaterials[i].Count -= 1;
                            GeneralManager.Singleton.CurrentPlayer.Money -= SellMaterials[i].AvgPrice;

                            if (BuyMaterials[i].Count <= 0)
                            {
                                BuyMaterials.Remove(BuyMaterials[i]);
                            }
                        }
                    }
                }
            }


            base.Update(gameTime);
        }


        public void DrawComponentsTrade(TradeOptions Options)
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

        public void DrawMaterialsTrade()
        {
            //TODO : Drawing avalible buy and sell materials
            for (int i = 0; i < SellMaterials.Count; i++ )
            {
                Rectangle Slot = Renderer.GetPartialRect(0.2f, 0.2f + 0.05f * i, 0.05f, 0.05f);
                Rectangle PriceRect = Renderer.GetPartialRect(0.23f, 0.23f + 0.05f * i, 0.02f, 0.02f);

                Renderer.Singleton.batch.Draw(Renderer.Singleton.SlotBackground, Slot, Color.White);
                Renderer.Singleton.batch.Draw(SellMaterials[i].Tex, Slot, Color.White);

                Renderer.Singleton.batch.Draw(Renderer.Singleton.SlotBackground, PriceRect, Color.White);
                Text PriceText = new Text(Game);
                PriceText.Name = SellMaterials[i].AvgPrice.ToString();
                PriceText.Rect = PriceRect;
                PriceText.Font = Font;
                PriceText.Draw(null);
            }

            for (int i = 0; i < BuyMaterials.Count; i++)
            {
                Rectangle Slot = Renderer.GetPartialRect(0.3f, 0.2f + 0.05f * i, 0.05f, 0.05f);
                Rectangle PriceRect = Renderer.GetPartialRect(0.33f, 0.23f + 0.05f * i, 0.02f, 0.02f);

                Renderer.Singleton.batch.Draw(Renderer.Singleton.SlotBackground, Slot, Color.White);
                Renderer.Singleton.batch.Draw(BuyMaterials[i].Tex, Slot, Color.White);

                Renderer.Singleton.batch.Draw(Renderer.Singleton.SlotBackground, PriceRect, Color.White);
                Text PriceText = new Text(Game);
                PriceText.Name = BuyMaterials[i].AvgPrice.ToString();
                PriceText.Rect = PriceRect;
                PriceText.Font = Font;
                PriceText.Draw(null);
            }

            Renderer.Singleton.batch.Draw(Renderer.Textures["BuyButton"], Renderer.GetPartialRect(0.18f, 0.75f, 0.15f, 0.1f), Color.White);
            Renderer.Singleton.batch.Draw(Renderer.Textures["SellButton"], Renderer.GetPartialRect(0.37f, 0.75f, 0.15f, 0.1f), Color.White);
        }


        public override void CatchClick()
        {
            
        }

    }
}