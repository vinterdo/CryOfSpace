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
            Menu = new MenuComponent(Game, spriteBatch, Renderer.Singleton.Content.Load<SpriteFont>("Font"), new string[] { "New Game", "Load Game", "Back" });
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
                            GeneralManager.Players.Add("test", new Player());
                            GeneralManager.Players["test"].Ship.Hull = Hull.Hulls["Test"];
                            GeneralManager.Players["test"].Initalize();
                            GeneralManager.Singleton.CurrentLevel = new Level(Game, spriteBatch);
                            GeneralManager.Singleton.CurrentLevel.Generate();
                            GeneralManager.Singleton.CurrentLevel.Show();
                            
                            this.Visible = false;
                            break;
                        case 1:
                            SaveGameManager.Load(Game, spriteBatch);
                            this.Visible = false;
                            GeneralManager.Singleton.CurrentLevel.Visible = true;
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