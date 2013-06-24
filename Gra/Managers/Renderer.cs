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

        public ContentManager Content;

        
        public static int Height, Width;
        public static Dictionary<string, Animation> Animations;
        public Game Game;


        public Texture2D PlayerIndicator;
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
        public Texture2D MinimapOverlay;
        public Texture2D IndicatorRed;
        public Texture2D IndicatorGreen;
        public Texture2D MinimapBackground;
        public Texture2D MoneyBackground;
        public Texture2D ConnectionTex;
        public Texture2D GaussCannonB50;
        public Texture2D GaussBullet;
        public Texture2D WeaponsMenu;
        public Texture2D CheckBoxOff;
        public Texture2D CheckBoxOn;
        public Texture2D HeatGradient;
        public Texture2D ProgressBackground;
        public Texture2D ProgressOverlay;
        public Texture2D Asteroid1;
        public Texture2D MiningBeam;
        public Texture2D MiningLaser;
        public Texture2D LaserBeam;
        public Texture2D SmokeParticle;


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
            Blank = Content.Load<Texture2D>("Blank");
            ConnectionTex = Content.Load<Texture2D>("Connection");
            Background1 = Content.Load<Texture2D>("BackgroundVertex");
            Background2 = Content.Load<Texture2D>("hud");
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
            MinimapOverlay = Content.Load<Texture2D>("MinimapOverlay");
            IndicatorRed = Content.Load<Texture2D>("IndicatorRed");
            IndicatorGreen = Content.Load<Texture2D>("IndicatorGreen");
            MinimapBackground = Content.Load<Texture2D>("MinimapBackground");
            MoneyBackground = Content.Load<Texture2D>("MoneyBackground");
            GaussCannonB50 = Content.Load<Texture2D>("GaussCannonB50");
            GaussBullet = Content.Load<Texture2D>("GaussBullet");
            WeaponsMenu = Content.Load<Texture2D>("WeaponMenu");
            CheckBoxOff = Content.Load<Texture2D>("CheckBoxOff");
            CheckBoxOn = Content.Load<Texture2D>("CheckBoxOn");
            HeatGradient = Content.Load<Texture2D>("HeatGradient");
            ProgressBackground = Content.Load<Texture2D>("ProgressBackground");
            ProgressOverlay = Content.Load<Texture2D>("ProgressOverlay");
            Asteroid1 = Content.Load<Texture2D>("Asteroid1");
            MiningBeam = Content.Load<Texture2D>("MiningBeam");
            MiningLaser = Content.Load<Texture2D>("MiningLaser");
            LaserBeam = Content.Load<Texture2D>("LaserBeam");
            SmokeParticle = Content.Load<Texture2D>("SmokeParticle");

            
            
        }

        public void Line(float width, Vector2 from, Vector2 to, Color color)
        {
            float angle = (float)Math.Atan2(to.Y - from.Y, to.X - from.X);
            float length = Vector2.Distance(from, to);

            batch.Draw(ConnectionTex, from, null, color,
                       angle, Vector2.Zero, new Vector2(length/10, width),
                       SpriteEffects.None, 1);
        }

        static public int GetPartialWidth(float Part)
        {
            return (int)(Width * Part);
        }

        static public int GetPartialHeight(float Part)
        {
            return (int)(Height * Part);
        }

        static public Vector2 GetPartialVector(float PartX, float PartY)
        {
            return new Vector2(Width * PartX, Height * PartY);
        }

        static public Rectangle GetPartialRect(float PartX, float PartY, float PartWidth, float PartHeight)
        {
            return new Rectangle((int)(Width * PartX), (int)(Height * PartY), (int)(Width * PartWidth), (int)(Height * PartHeight));
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

        public void DrawMoney()
        {
            Rectangle Rect = new Rectangle((int)(Width * 0.4), 0, (int)(Width * 0.2), (int)(Height * 0.1));
            batch.Draw(MoneyBackground, Rect, null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.2f);
            Text Money = new Text(Game);
            Money.Rect = Rect;
            Money.Font = Content.Load<SpriteFont>("Font");
            Money.Name = GeneralManager.Singleton.CurrentPlayer.Money.ToString();
            Money.Draw(null);

        }
    }
}
