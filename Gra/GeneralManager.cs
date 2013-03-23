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
    public sealed class GeneralManager
    {
        static GeneralManager Instance = new GeneralManager();
        Random Random;
        public Level CurrentLevel;
        public bool IsLevelInitalized;
        public KeyboardState keyboardState;
        KeyboardState oldKeyboardState;
        public Vector2 MousePos;
        MouseState OldMouseState;
        MouseState NewMouseState;

        public static Dictionary<string, Player> Players = new Dictionary<string, Player>();

        public Vertex CurrentVertex;

        public int GameState = 1; // 1 - level, 2 - vertex


        private GeneralManager()
        {
            IsLevelInitalized = false;
            Random = new Random();
        }

        public bool CheckLMB()
        {
            if (OldMouseState.LeftButton == ButtonState.Released && NewMouseState.LeftButton == ButtonState.Pressed)
                return true;
            else
                return false;
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
            foreach(KeyValuePair<string, Player> P in Players)
            {
                P.Value.Update(gameTime);
            }

            if(IsLevelInitalized) CurrentLevel.Update(gameTime);
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            OldMouseState = NewMouseState;
            NewMouseState = Mouse.GetState();
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
            var Tmp = new Vector2((float)Math.Sin(Angle), (float)Math.Cos(Angle));
            Tmp.Normalize();
            return Tmp;
        }
    }
}
