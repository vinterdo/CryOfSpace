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
    public class MultiplayerChooseScreen : GameScreen
    {
        public MultiplayerChooseScreen(Game game,SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {

        }
        MenuComponent Menu;
        
        public override void Initialize()
        {
            Menu = new MenuComponent(Game, spriteBatch, Renderer.Singleton.Content.Load<SpriteFont>("Font"), new string[] { "Host", "Join", "Back" });
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                if (Menu.IsEnterPressed)
                {
                    switch (Menu.SelectedIndex)
                    {
                        case 0:
                            break;
                        case 1:
                            this.Visible = false;
                            ScreenManager.Singleton.IpSelectionScreen.Visible = true;
                            break;
                        case 2:
                            this.Visible = false;
                            ScreenManager.Singleton.MainMenu.Visible = true;
                            break;
                    }
                }

                Menu.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                Menu.Draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }
}