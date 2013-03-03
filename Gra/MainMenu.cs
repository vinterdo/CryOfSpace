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
    public class MainMenu : GameScreen
    {
        public MainMenu(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
        }

        MenuComponent Menu;

        public override void Initialize()
        {
            Menu = new MenuComponent(Game, spriteBatch, Renderer.Singleton.Content.Load<SpriteFont>("Kootenay"), new string[] { "SinglePlayer", "MultiPlayer", "Quit Game" });
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                if (Menu.SelectedIndex == 2 && Menu.keyboardState.IsKeyDown(Keys.Enter)) Game.Exit();
                if (Menu.SelectedIndex == 0 && Menu.keyboardState.IsKeyDown(Keys.Enter))
                {
                    this.Visible = false;
                    GeneralManager.Singleton.CurrentLevel.Show();


                }
                Menu.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if(Visible)
                Menu.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}