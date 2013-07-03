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
    public class TextBox : GuiElement
    {
        public TextBox(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
        }

        public string Text = "";
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
            FocusedColor = Color.Gray;
            UnfocusedColor = Color.Aquamarine;
            BgTex = Renderer.Singleton.Content.Load<Texture2D>("TextBox");

            

            base.Initialize();
        }

        public void Measure()
        {
            Rect.Width = (int)Font.MeasureString(Text).X;
            Rect.Height = (int)Font.MeasureString(Text).Y;
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
                        if (k == Keys.Space) Text += " ";
                        else if (k == Keys.Back) Text = Text.Remove(Text.Length - 1);
                        else if (k >= Keys.A && k <= Keys.Z)
                            Text += k.ToString();
                        else if (k >= Keys.D0 && k <= Keys.D9)
                            Text += k.ToString().ToCharArray(1, 1)[0].ToString();

                        switch (k)
                        {
                            case Keys.OemPeriod:
                                Text += ".";
                                break;
                            case Keys.OemComma:
                                Text += ",";
                                break;
                            case Keys.OemMinus:
                                Text += "-";
                                break;
                        }

                    }
                }

                Measure();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                if (IsFocused)
                {
                    spriteBatch.Draw(BgTex, Rect, FocusedColor);
                    spriteBatch.DrawString(Font, Text, GetVectorFromPoint(Rect.Location), UnfocusedColor);
                }
                else
                {
                    spriteBatch.Draw(BgTex, Rect, UnfocusedColor);
                }
            }
            base.Draw(gameTime);
        }

        public static Vector2 GetVectorFromPoint(Point Point)
        {
            return new Vector2(Point.X, Point.Y);
        }

        public override void CatchClick()
        {

        }
    }
}