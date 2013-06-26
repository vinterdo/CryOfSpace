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
    public sealed class ScreenManager
    {
        static ScreenManager Instance = new ScreenManager();

        public MainMenu MainMenu;
        public GameSelectionScreen SelectionScreen;
        public InGameMenu InGameMenu;
        public MultiplayerChooseScreen MultiplayerChooseScreen;
        public IpSelectionScreen IpSelectionScreen;
        public ProjectViewScreen ProjectView;
        public InventoryScreen InventoryScreen;



        private ScreenManager()
        {
        }


        public static ScreenManager Singleton
        {
            get
            {
                return Instance;
            }
            set { }
        }

        public void Update(GameTime gameTime)
        {
            MainMenu.Update(gameTime);
            SelectionScreen.Update(gameTime);
            InGameMenu.Update(gameTime);
            MultiplayerChooseScreen.Update(gameTime);
            IpSelectionScreen.Update(gameTime);
            ProjectView.Update(gameTime);
            InventoryScreen.Update(gameTime);
        }


        public void Initalize(Game game)
        {
            MainMenu = new MainMenu(game, Renderer.Singleton.batch);
            MainMenu.Fade = 1f;
            MainMenu.ScreenState = GameScreen.State.FadeIn;
            MainMenu.Initialize();
            SelectionScreen = new GameSelectionScreen(game, Renderer.Singleton.batch);
            SelectionScreen.Initialize();
            InGameMenu = new InGameMenu(game, Renderer.Singleton.batch);
            InGameMenu.Initialize();
            MultiplayerChooseScreen = new MultiplayerChooseScreen(game, Renderer.Singleton.batch);
            MultiplayerChooseScreen.Initialize();
            IpSelectionScreen = new IpSelectionScreen(game, Renderer.Singleton.batch);
            IpSelectionScreen.Initialize();
            ProjectView = new ProjectViewScreen(game, Renderer.Singleton.batch);
            ProjectView.Initialize();
            InventoryScreen = new InventoryScreen(game, Renderer.Singleton.batch);
            InventoryScreen.Initialize();

        }
    }
}
