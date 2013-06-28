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
    public class InGameMenu : GameScreen
    {
        MenuComponent Menu;

        public InGameMenu(Game game)
            : base(game)
        {
            
        }

        

        public override void Initialize()
        {
            Menu = new MenuComponent(Game, Renderer.Singleton.batch, Renderer.Singleton.Content.Load<SpriteFont>("Font"), new string[] { "Back to Game", "Options", "Save Game", "Quit Game" });
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
                            //this.Visible = false;
                            Target = GeneralManager.Singleton.CurrentLevel;
                            ScreenState = State.FadeOut;

                            break;
                        case 1:

                            break;
                        case 2:
                            SaveGameManager.Save();
                            GeneralManager.SoundManager.PlaySound("beep");
                            break;
                        case 3:
                            GeneralManager.Singleton.CurrentLevel = null;
                            GeneralManager.Singleton.IsLevelInitalized = false;
                            this.Visible = false;
                            ScreenManager.Singleton.MainMenu.Visible = true;
                            ScreenManager.Singleton.MainMenu.Fade = 0;
                            GeneralManager.SoundManager.UnLoop("shipengine");
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