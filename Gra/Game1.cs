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
        }

        protected override void Initialize()
        {
            base.Initialize();
            
            Renderer.Singleton.InitRenderer(spriteBatch, Content);
            Renderer.Singleton.LoadContent();

            GeneralManager.Singleton.CurrentLevel = new Level();
            GeneralManager.Singleton.CurrentLevel.Generate();
            GeneralManager.Singleton.CurrentLevel.AddConnection(1, 2);

            /*CurrentLevel.CreateVertex(new Vector2(100, 100), Indicator);
            CurrentLevel.CreateVertex(new Vector2(100, 200), Indicator);
            CurrentLevel.CreateVertex(new Vector2(300, 150), Indicator);*/

            
            
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
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            
            spriteBatch.Begin();
            GeneralManager.Singleton.CurrentLevel.Render(spriteBatch);
            GeneralManager.Singleton.CurrentLevel.RenderConnections(spriteBatch);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
