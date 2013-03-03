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

        MenuComponent Menu1;
        
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
            
            Renderer.Singleton.InitRenderer(spriteBatch, Content);
            Renderer.Singleton.LoadContent();

            GeneralManager.Singleton.CurrentLevel = new Level(this, spriteBatch);
            GeneralManager.Singleton.CurrentLevel.Generate();
            Menu1 = new MenuComponent(this, spriteBatch, Content.Load<SpriteFont>("Kootenay"), new string[] { "hah", "ok" });


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
            Menu1.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            
            spriteBatch.Begin();
            Renderer.Singleton.RenderBackground(gameTime);
            
            GeneralManager.Singleton.CurrentLevel.Draw(gameTime);;
            Menu1.Draw(gameTime);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
