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
        Hull ExampleHull;
        Ship USSGruz;

        
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferMultiSampling = true;
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            
            Renderer.Singleton.InitRenderer(this, spriteBatch, Content);
            Renderer.Singleton.LoadContent();


            ScreenManager.Singleton.Initalize(this);
            //GeneralManager.Singleton.CurrentLevel.Hide();
            ScreenManager.Singleton.MainMenu.Visible = true;

            //=============
            ExampleHull = new Hull();
            ExampleHull.Name = "Test";
            ExampleHull.AtmosphereMask = new bool[10][]{new bool[]{false, false, false, false, false, false, false, false, false, false}, 
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false}};
            ExampleHull.BasicHull = 100.0f;
            ExampleHull.Center = new Vector2(5, 5);
            ExampleHull.ComponentsMask = new bool[10][]{new bool[]{false, false, false, false, false, false, false, false, false, false}, 
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false}};
            ExampleHull.ConduitsMask = new bool[10][]{new bool[]{false, false, false, false, false, false, false, false, false, false}, 
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false},
                                                          new bool[]{false, false, false, false, false, false, false, false, false, false}};
            ExampleHull.ConduitsView = new RawAnimation();
            ExampleHull.Explosion = new RawAnimation();
            ExampleHull.HullBreachs = new List<Vector2>();
            ExampleHull.HullModifier = 1.0f;
            ExampleHull.InsideView = new RawAnimation();
            ExampleHull.OutsideView = new RawAnimation();
            ExampleHull.OutsideView.TextureName = "ship2";
            ExampleHull.OutsideView.SetProperties(new Vector2(300, 300), 1000, 1);
            ExampleHull.OutsideView.CreateAnimation();
            ExampleHull.SizeX = 10;
            ExampleHull.SizeY = 10;
            ExampleHull.SpeedModifier = 1.0f;
            ExampleHull.Weight = 100.0f;
            ExampleHull.Wreck = new RawAnimation();

            ExampleHull.Initialize();
            //ExampleHull.Load("ships/ship.xml");

            USSGruz = new Ship(this);
            USSGruz.Hull = ExampleHull;
            USSGruz.Initialize();
            //=============
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
            NetworkManager.Singleton.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            
            spriteBatch.Begin();


            ScreenManager.Singleton.MainMenu.Draw(gameTime);
            ScreenManager.Singleton.SelectionScreen.Draw(gameTime);
            ScreenManager.Singleton.InGameMenu.Draw(gameTime);
            ScreenManager.Singleton.MultiplayerChooseScreen.Draw(gameTime);
            ScreenManager.Singleton.IpSelectionScreen.Draw(gameTime);
            if(GeneralManager.Singleton.IsLevelInitalized) 
                GeneralManager.Singleton.CurrentLevel.Draw(gameTime);

            Renderer.Animations["test"].Draw(gameTime);
            USSGruz.DrawOutside(gameTime);
            
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
