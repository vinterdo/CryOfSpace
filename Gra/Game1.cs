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
        Rectangle TmpRect;
        Level CurrentLevel;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            TmpRect = new Rectangle(0, 0, 100, 100);
            base.Initialize();
            CurrentLevel = new Level();

            CurrentLevel.CreateVertex(new Vector2(100, 100), Indicator);
            CurrentLevel.CreateVertex(new Vector2(100, 200), Indicator);
            CurrentLevel.CreateVertex(new Vector2(300, 150), Indicator);
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Indicator = Content.Load<Texture2D>("indicator");

            
            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                TmpRect.Y += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                TmpRect.Y -= 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                TmpRect.X += 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                TmpRect.X -= 1;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            
            spriteBatch.Begin();
            //spriteBatch.Draw(Tmp, TmpRect, Color.Red);
            CurrentLevel.Render(spriteBatch);

            CurrentLevel.DrawConnection(0, spriteBatch);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
