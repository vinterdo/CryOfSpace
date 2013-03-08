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
    public class TextBox : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public TextBox(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
        }

        Rectangle Box;
        string Text = "abc";
        public bool IsFocused;
        Color FocusedColor;
        Color UnfocusedColor;
        Texture2D BgTex;
        KeyboardState oldKeyboardState;
        SpriteBatch spriteBatch;
        SpriteFont Font;

        public override void Initialize()
        {
            Font = Renderer.Singleton.Content.Load<SpriteFont>("Font");
            Visible = true;
            IsFocused = false;
            FocusedColor = Color.White;
            UnfocusedColor = Color.Aquamarine;
            BgTex = Renderer.Singleton.Content.Load<Texture2D>("Blank");
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                oldKeyboardState = Keyboard.GetState();

                foreach (Keys k in Enum.GetValues(typeof(Keys)))
                {
                    if (GeneralManager.Singleton.CheckKey(k))
                    {
                        Text += k.ToString();
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                if (IsFocused)
                {
                    spriteBatch.Draw(BgTex, Box, FocusedColor);
                    spriteBatch.DrawString(Font, Text, GetVectorFromPoint(Box.Location), FocusedColor);
                }
                else
                {
                    spriteBatch.Draw(BgTex, Box, UnfocusedColor);
                }
            }
            base.Draw(gameTime);
        }

        public static Vector2 GetVectorFromPoint(Point Point)
        {
            return new Vector2(Point.X, Point.Y);
        }


    }
}