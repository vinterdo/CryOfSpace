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
    public sealed class Renderer
    {

        public SpriteBatch batch;
        Texture2D Blank;
        Texture2D Background1;
        Texture2D Background2;
        public Texture2D PlayerIndicator;
        public ContentManager Content;
        public static int Height, Width;
        public static Dictionary<string, Animation> Animations;
        public Game Game;
        public Texture2D Background;
        public Texture2D ShipIndicator;
        public Texture2D TextBackground;
        public Texture2D CursorTex;
        public Texture2D VertexMenuBackground;
        public Texture2D ViewVertexButton;
        public Texture2D FlyToVertexButton;
        public Texture2D ProjectButton;
        public Texture2D ProjectGUI;
        public Texture2D ProjectViewBackground;
        public Texture2D SlotBackground;
        public Texture2D SelectedBackground;
        public Texture2D SpaceStationMenuBG;

        public Texture2D FromVertexToLevelGUI;

        private Renderer()
        {
        }

        private static Renderer Instance = new Renderer();

        public static Renderer Singleton
        {
            get
            {
                return Instance;
            }
            set
            {
            }
        }

        public void InitRenderer(Game game, SpriteBatch spriteBatch, ContentManager Content)
        {
            this.Game = game;
            batch = spriteBatch;
            Height = batch.GraphicsDevice.Viewport.Height;
            Width = batch.GraphicsDevice.Viewport.Width;
            Animations = new Dictionary<string, Animation>();
            this.Content = Content;
        }

        public void LoadContent()
        {
            this.Blank = Content.Load<Texture2D>("Blank");
            this.Background1 = Content.Load<Texture2D>("BackgroundVertex");
            this.Background2 = Content.Load<Texture2D>("hud");
            PlayerIndicator = Content.Load<Texture2D>("PlayerIndicator");
            Background = Content.Load<Texture2D>("SpaceBackground1");
            ShipIndicator = Content.Load<Texture2D>("ShipIndicator");
            FromVertexToLevelGUI = Content.Load<Texture2D>("view system gui");
            TextBackground = Content.Load<Texture2D>("TextBackground");
            CursorTex = Content.Load<Texture2D>("Cursor");
            VertexMenuBackground = Content.Load<Texture2D>("VertexMenu");
            ViewVertexButton = Content.Load<Texture2D>("ViewButtonVertex");
            FlyToVertexButton = Content.Load<Texture2D>("FlyToButtonVertex");
            ProjectButton = Content.Load<Texture2D>("ProjectButton");
            ProjectGUI = Content.Load<Texture2D>("ProjectGUI");
            ProjectViewBackground = Content.Load<Texture2D>("ProjectViewBackground");
            SlotBackground = Content.Load<Texture2D>("SlotBackground");
            SelectedBackground = Content.Load<Texture2D>("SelectionBackground");
            SpaceStationMenuBG = Content.Load<Texture2D>("SpaceStationMenu");
            
        }

        public void Line(float width, Vector2 from, Vector2 to, Color color)
        {
            float angle = (float)Math.Atan2(to.Y - from.Y, to.X - from.X);
            float length = Vector2.Distance(from, to);

            batch.Draw(Blank, from, null, color,
                       angle, Vector2.Zero, new Vector2(length/10, width),
                       SpriteEffects.None, 1);
        }

        public void RenderBackground(GameTime gameTime)
        {
            Vector2 RandomPos = new Vector2((float)Math.Sin(((gameTime.TotalRealTime.Milliseconds + gameTime.TotalRealTime.Seconds * 1000) * Math.PI) / 15250) * 15, (float)Math.Cos(((gameTime.TotalRealTime.Milliseconds + gameTime.TotalRealTime.Seconds * 1000) * Math.PI) / 30500) * 15);
            RandomPos += new Vector2(GeneralManager.Singleton.MousePos.X / Renderer.Width * -30 + 15, GeneralManager.Singleton.MousePos.Y / Renderer.Height * -30 + 15);

            batch.Draw(Background1, new Rectangle((int)RandomPos.X - 15, (int)RandomPos.Y - 15 ,Width + 15, Height+ 15), new Color(Color.White, 0.3f));

            batch.Draw(Background2, new Rectangle(0, 0, Width, Height), new Color(Color.White, 1.0f));

        }

        public void CreateAnimation(string name, Vector2 FrameSize, float TimePerFrame, int NoFrames, Texture2D Tex)
        {
            Animation a = new Animation(Game);
            a.LoadTex(Tex);
            a.SetProperties(FrameSize, TimePerFrame, NoFrames);
            Animations.Add(name, a);
        }

        public void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<string, Animation> a in Animations)
            {
                a.Value.Update(gameTime);
            }
        }
    }
}
