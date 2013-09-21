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
    public class IpSelectionScreen : GameScreen
    {
        TextBox IpTextBox;
        MenuComponent Menu;

        public IpSelectionScreen(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            IpTextBox = new TextBox(Game, Renderer.Singleton.batch);
            Menu = new MenuComponent(Game, Renderer.Singleton.batch, Renderer.Singleton.Content.Load<SpriteFont>("Font"), new string[] { "Server Ip:", "Back" });
            IpTextBox.Initialize();
            IpTextBox.Rect.X = (int)Menu.position.X;
            IpTextBox.Rect.Y = (int)Menu.position.Y + (int)Menu.Spacing + Menu.spriteFont.LineSpacing;
            IpTextBox.Rect.Width = 100;
            IpTextBox.Rect.Height = 100;
            Menu.Spacing = Menu.spriteFont.LineSpacing + 2 * Menu.Spacing;
            
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                Menu.Update(gameTime);
                if (Menu.SelectedIndex == 0)
                {
                    IpTextBox.IsFocused = true;
                    IpTextBox.Update(gameTime);

                    if (GeneralManager.Singleton.CheckKey(Keys.Enter))
                    {
                        NetworkManager.Singleton.IsHost = false;
                        NetworkManager.Singleton.ConnectTo(IpTextBox.Text);

                    }
                }
                else if(Menu.SelectedIndex == 1)
                {
                    if (GeneralManager.Singleton.CheckKey(Keys.Enter))
                    {
                        this.Visible = false;
                        IpTextBox.IsFocused = true;
                        ScreenManager.Singleton.MultiplayerChooseScreen.Visible = true;
                    }
                }
                
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                Menu.Draw(gameTime);
                IpTextBox.Draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }
}