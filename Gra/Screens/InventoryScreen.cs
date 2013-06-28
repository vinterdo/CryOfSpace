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
    public class InventoryScreen : GameScreen
    {
        Texture2D Background;

        public InventoryScreen(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            Background = Renderer.Singleton.Content.Load<Texture2D>("InventoryBackground");
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                if (GeneralManager.Singleton.CheckLMB() && Renderer.GetPartialRect(0.9f, 0f, 0.1f, 0.05f).Contains((int)GeneralManager.Singleton.MousePos.X, (int)GeneralManager.Singleton.MousePos.Y))
                {
                    this.Visible = false;
                    GeneralManager.Singleton.CurrentLevel.Show();
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                Renderer.Singleton.batch.Draw(Background, new Rectangle(0, 0, Renderer.Width, Renderer.Height), Color.Gray);

                for (int i = 0; i < 16; i++)
                {
                    DrawMaterialSlot(i, i);
                }

                Renderer.Singleton.batch.Draw(Renderer.Textures["BackButton"], Renderer.GetPartialRect(0.9f, 0f, 0.1f, 0.05f), Color.White);

                base.Draw(gameTime);
            }
        }

        public void DrawMaterialSlot(int Index, int Position)
        {
            Rectangle Rect = Renderer.GetPartialRect(0.1f , 0.1f + 0.05f * Position, 0.05f, 0.05f);
            Renderer.Singleton.batch.Draw(Renderer.Singleton.SlotBackground, Rect, Color.White);
            if (GeneralManager.Singleton.CurrentPlayer.MaterialsInventory.Count > Index && GeneralManager.Singleton.CurrentPlayer.MaterialsInventory[Index] != null)
            {
                Renderer.Singleton.batch.Draw(GeneralManager.Singleton.CurrentPlayer.MaterialsInventory[Index].Tex, Rect, Color.White);
                Rectangle SmallRect = Renderer.GetPartialRect(0.13f, 0.13f + 0.05f * Position, 0.02f, 0.02f);
                Renderer.Singleton.batch.Draw(Renderer.Singleton.SlotBackground, SmallRect, Color.White);
                Text Label = new Text(Game);
                Label.Font = Renderer.Singleton.Content.Load<SpriteFont>("Font");
                Label.Rect = SmallRect;
                Label.Name = GeneralManager.Singleton.CurrentPlayer.MaterialsInventory[Index].Count.ToString();
                Label.Draw(null);
            }

        }

        
    }
}
