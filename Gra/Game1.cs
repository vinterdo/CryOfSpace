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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D Indicator;
        

        
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
        }

        protected override void Initialize()
        {
            base.Initialize();
            
            Renderer.Singleton.InitRenderer(this, spriteBatch, Content);
            Renderer.Singleton.LoadContent();


            ScreenManager.Singleton.Initalize(this);
            //GeneralManager.Singleton.CurrentLevel.Hide();
            ScreenManager.Singleton.MainMenu.Visible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Indicator = Content.Load<Texture2D>("indicator");
            
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            GeneralManager.Singleton.Update(gameTime);
            ScreenManager.Singleton.Update(gameTime);
            Renderer.Singleton.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            
            spriteBatch.Begin();


            ScreenManager.Singleton.MainMenu.Draw(gameTime);
            ScreenManager.Singleton.SelectionScreen.Draw(gameTime);
            ScreenManager.Singleton.InGameMenu.Draw(gameTime);
            if(GeneralManager.Singleton.IsLevelInitalized) 
                GeneralManager.Singleton.CurrentLevel.Draw(gameTime);

            Renderer.Animations["test"].Draw(gameTime);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
