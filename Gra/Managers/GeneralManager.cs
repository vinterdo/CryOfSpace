﻿using System;
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
    public sealed class GeneralManager
    {
        public static SoundManager SoundManager;

        static GeneralManager Instance = new GeneralManager();
        Random Random;
        public Level CurrentLevel;
        public bool IsLevelInitalized;
        public KeyboardState keyboardState;
        KeyboardState oldKeyboardState;
        public Vector2 MousePos;
        MouseState OldMouseState;
        MouseState NewMouseState;
        public Player CurrentPlayer;
        public bool IsLMBDown;
        public bool ClickCatched = false;

        public static Dictionary<string, Player> Players = new Dictionary<string, Player>();
        public static List<VertexScreen> Vertexes = new List<VertexScreen>();
        public static List<GuiElement> Gui = new List<GuiElement>();

        public VertexScreen CurrentVertex;

        public int GameState = 1; // 1 - level, 2 - vertex


        private GeneralManager()
        {
            SoundManager = new SoundManager(Renderer.Singleton.Game);
            SoundManager.Initialize();
            IsLevelInitalized = false;
            Random = new Random();

            //    MATERIAL TYPES

            RawMaterial.Types.Add(new Plutonium(0).GetType());
            RawMaterial.Types.Add(new Tungsten(0).GetType());
            RawMaterial.Types.Add(new Hydrogen(0).GetType());
        }

        public bool CheckLMB()
        {
            return OldMouseState.LeftButton == ButtonState.Released && NewMouseState.LeftButton == ButtonState.Pressed && ! ClickCatched;
        }

        static public GeneralManager Singleton
        {
            get
            {
                return Instance;
            }
            set
            {
            }
        }

        public void Update(GameTime gameTime)
        {
            ClickCatched = false;

            foreach(KeyValuePair<string, Player> P in Players)
            {
                P.Value.Update(gameTime);
            }

            if(CurrentVertex != null)
            {
                CurrentVertex.Update(gameTime);
            }

            if(IsLevelInitalized)
            {

                foreach (VertexScreen V in Vertexes)
                {
                    V.Update(gameTime);
                }


                /*foreach (DrawableGameComponent C in CurrentLevel.Components)
                {
                    if (C is Vertex)
                    {
                        Vertex Vert = C as Vertex;
                        if (Vert.Active)
                        {
                            Vert.Update(gameTime);
                        }

                    }
                }*/
            }

            if(IsLevelInitalized) CurrentLevel.Update(gameTime);
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            OldMouseState = NewMouseState;
            NewMouseState = Mouse.GetState();

            IsLMBDown = NewMouseState.LeftButton == ButtonState.Pressed;

            MousePos = new Vector2(NewMouseState.X, NewMouseState.Y);
        }

        public int GetRandom()
        {
            return Random.Next();
        }

        public bool CheckKey(Keys Key)
        {
            return oldKeyboardState.IsKeyUp(Key) && keyboardState.IsKeyDown(Key);
        }

        public Vector2 GetVectorFromAngle(float Angle)
        {
            return new Vector2((float)Math.Sin(Angle), (float)Math.Cos(Angle));
        }

        public float GetAngleFromVector(Vector2 Vec)
        {
            return (float)Math.Atan2(Vec.X, Vec.Y);
        }

        public Vector2 RotateVector(float Angle, Vector2 Vec, Vector2 Center)
        {
            return new Vector2((float)(Math.Cos(Angle) * (Vec.X - Center.X) - Math.Sin(Angle) * (Vec.Y - Center.Y)), (float)(Math.Sin(Angle) * (Vec.X - Center.X) + Math.Cos(Angle) * (Vec.Y - Center.Y))) + Center;
        }

        public bool CheckCollision(Vector2 Vec, Rectangle Rect)
        {
            return Vec.X > Rect.X && Vec.Y > Rect.Y && Vec.X < Rect.X + Rect.Width && Vec.Y < Rect.Y + Rect.Height;
        }

        public bool CheckCollision(Vector2 Vec, Rectangle Rect, float Angle, Vector2 Center)
        {
            return CheckCollision(RotateVector(Angle * -1, Vec, Center), Rect);
        }

        public static void DrawGUI(GameTime gameTime)
        {
            foreach (GuiElement G in Gui)
            {
                G.Draw(gameTime);
            }
        }

        public static void UpdateGUI(GameTime gameTime)
        {
            foreach (GuiElement G in Gui)
            {
                G.Draw(gameTime);
            }
        }
    }
}
