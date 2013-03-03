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
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        private GeneralManager()
        {
            IsLevelInitalized = false;
            Random = new Random();
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
            if(IsLevelInitalized) CurrentLevel.Update(gameTime);
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
        }

        public int GetRandom()
        {
            return Random.Next();
        }

        public bool CheckKey(Keys Key)
        {
            return oldKeyboardState.IsKeyUp(Key) && keyboardState.IsKeyDown(Key);
        }
    }
}
