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

        Texture2D Background;
        Texture2D Foreground;
        Texture2D BackgroundLight;
        Vector2 BackgroundPos = new Vector2(0, 0);

        public GameSelectionScreen(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {

        }

        public override void Initialize()
        {
            Menu = new MenuComponent(Game, spriteBatch, Renderer.Singleton.Content.Load<SpriteFont>("Font"), new string[] { "New Game", "Load Game", "Back" });
            Background = Renderer.Singleton.Content.Load<Texture2D>("MainMenuBackground");
            BackgroundLight = Renderer.Singleton.Content.Load<Texture2D>("MainMenuLight");
            Foreground = Renderer.Singleton.Content.Load<Texture2D>("MainMenuForeground");
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
                            GeneralManager.Players = new Dictionary<string, Player>();
                            GeneralManager.Players.Add("test", new Player());
                            GeneralManager.Players["test"].Ship.Hull = Hull.Hulls["Test"];
                            GeneralManager.Players["test"].Initalize();
                            GeneralManager.Singleton.CurrentLevel = new Level(Game, spriteBatch);
                            GeneralManager.Singleton.CurrentLevel.Generate();
                            GeneralManager.Singleton.CurrentLevel.Show();
                            GeneralManager.SoundManager.Initialize();

                            GeneralManager.SoundManager.LoadSound("WelcomeAboard");
                            GeneralManager.SoundManager.LoadSound("WarpJump");
                            GeneralManager.SoundManager.LoadSound("shipengine");
                            GeneralManager.SoundManager.LoadSound("DroppHSpace");

                            GeneralManager.SoundManager.PlaySound("WelcomeAboard");
                            
                            GeneralManager.SoundManager.PlayInLoop("shipengine");

                            
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
                float Transparency = (float)Math.Abs((Math.Sin(((gameTime.TotalRealTime.Milliseconds + gameTime.TotalRealTime.Seconds * 1000) * Math.PI) / 7625)));
                Vector2 RandomPos = new Vector2((float)Math.Sin(((gameTime.TotalRealTime.Milliseconds + gameTime.TotalRealTime.Seconds * 1000) * Math.PI) / 15250) * 15, (float)Math.Cos(((gameTime.TotalRealTime.Milliseconds + gameTime.TotalRealTime.Seconds * 1000) * Math.PI) / 30500) * 15);
                BackgroundPos = new Vector2(GeneralManager.Singleton.MousePos.X / Renderer.Width * -30 + 15, GeneralManager.Singleton.MousePos.Y / Renderer.Height * -30 + 15);
                BackgroundPos += RandomPos;

                spriteBatch.Draw(Background, new Rectangle((int)BackgroundPos.X - 30, (int)BackgroundPos.Y - 30, Renderer.Width, Renderer.Height), Color.White);
                spriteBatch.Draw(Foreground, new Rectangle(0, 0, Renderer.Width, Renderer.Height), Color.White);

                spriteBatch.Draw(BackgroundLight, new Rectangle(0, 0, Renderer.Width, Renderer.Height), new Color(Color.White, Transparency));


                Menu.Draw(gameTime);
            }
            base.Draw(gameTime);
        } 
    }
}