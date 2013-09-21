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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D Indicator;

        RenderTarget2D ShaderRenderTarget;
        Texture2D ShaderTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferMultiSampling = true;
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            Window.Title = "Cry of Space";
            //graphics.IsFullScreen = true;
            //IsMouseVisible = true;
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

            PresentationParameters pp = GraphicsDevice.PresentationParameters;
            ShaderRenderTarget = new RenderTarget2D(GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 1, SurfaceFormat.Color, pp.MultiSampleType, pp.MultiSampleQuality);
            ShaderTexture = new Texture2D(GraphicsDevice,
                ShaderRenderTarget.Width, ShaderRenderTarget.Height, 1,
                TextureUsage.None, ShaderRenderTarget.Format);


        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            GeneralManager.Singleton.Update(gameTime);
            ScreenManager.Singleton.Update(gameTime);
            Renderer.Singleton.Update(gameTime);
            NetworkManager.Singleton.Update();
            ParticleWorld.Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            foreach(KeyValuePair<string, Player> P in GeneralManager.Players)
            {
                P.Value.Ship.CreateInsideTex(gameTime);
            }

            GraphicsDevice.SetRenderTarget(0, ShaderRenderTarget);
            GraphicsDevice.Clear(Color.Black);
            
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);

                ScreenManager.Singleton.MainMenu.Draw(gameTime);
                ScreenManager.Singleton.SelectionScreen.Draw(gameTime);
                ScreenManager.Singleton.InGameMenu.Draw(gameTime);
                ScreenManager.Singleton.MultiplayerChooseScreen.Draw(gameTime);
                ScreenManager.Singleton.IpSelectionScreen.Draw(gameTime);
                ScreenManager.Singleton.InventoryScreen.Draw(gameTime);
            
            

            if (GeneralManager.Singleton.IsLevelInitalized)
            {
                switch (GeneralManager.Singleton.GameState)
                {
                    case 1:
                        if (GeneralManager.Singleton.CurrentLevel.Visible)
                        {
                            GeneralManager.Singleton.CurrentLevel.Draw(gameTime);
                        }
                        ScreenManager.Singleton.ProjectView.Draw(gameTime);
                        spriteBatch.End();
                        spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.None);
                        Renderer.Singleton.DrawMoney();
                        spriteBatch.End();
                        spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
                        break;
                    case 2:
                        GeneralManager.Singleton.CurrentVertex.Draw(gameTime);
                        break;
                }
                
            }
            spriteBatch.Draw(Renderer.Singleton.CursorTex, GeneralManager.Singleton.MousePos - new Vector2(15, 15), Color.White);


            spriteBatch.End();
            GraphicsDevice.SetRenderTarget(0, null);
            ShaderTexture = ShaderRenderTarget.GetTexture();
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteBlendMode.None, SpriteSortMode.Immediate,
    SaveStateMode.None);
            
            //SHADERKI!
            
            ScreenManager.Singleton.MainMenu.BeginDrawEffects(gameTime);
            ScreenManager.Singleton.SelectionScreen.BeginDrawEffects(gameTime);
            ScreenManager.Singleton.InGameMenu.BeginDrawEffects(gameTime);
            ScreenManager.Singleton.MultiplayerChooseScreen.BeginDrawEffects(gameTime);
            ScreenManager.Singleton.IpSelectionScreen.BeginDrawEffects(gameTime);
            ScreenManager.Singleton.InventoryScreen.BeginDrawEffects(gameTime);
            if (GeneralManager.Singleton.CurrentVertex != null)
            {
                GeneralManager.Singleton.CurrentVertex.BeginDrawEffects(gameTime);
            }

            spriteBatch.Draw(ShaderTexture, Renderer.GetPartialRect(0,0,1,1), Color.White);

            spriteBatch.End();
            ScreenManager.Singleton.MainMenu.EndDrawEffects(gameTime);
            ScreenManager.Singleton.SelectionScreen.EndDrawEffects(gameTime);
            ScreenManager.Singleton.InGameMenu.EndDrawEffects(gameTime);
            ScreenManager.Singleton.MultiplayerChooseScreen.EndDrawEffects(gameTime);
            ScreenManager.Singleton.IpSelectionScreen.EndDrawEffects(gameTime);
            ScreenManager.Singleton.InventoryScreen.EndDrawEffects(gameTime);
            if (GeneralManager.Singleton.CurrentVertex != null)
            {
                GeneralManager.Singleton.CurrentVertex.EndDrawEffects(gameTime);
            }
            

            

        }
    }
}
