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
    public class GameSelectionScreen : GameScreen
    {
        MenuComponent Menu;

        public GameSelectionScreen(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {

        }

        public override void Initialize()
        {
            Menu = new MenuComponent(Game, spriteBatch, Renderer.Singleton.Content.Load<SpriteFont>("Kootenay"), new string[]{"New Game", "Load Game", "Back"});
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
                            GeneralManager.Singleton.CurrentLevel = new Level(Game, spriteBatch);
                            GeneralManager.Singleton.CurrentLevel.Generate();
                            GeneralManager.Singleton.CurrentLevel.Show();
                            this.Visible = false;
                            break;
                        case 1:

                            break;
                        case 2:

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