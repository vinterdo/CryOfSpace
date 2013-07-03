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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MenuComponent : GuiElement
    {
        string[] menuItems;
        int selectedIndex;

        Color normal = Color.White;
        Color hilite = Color.Aquamarine;
        public KeyboardState keyboardState;
        public KeyboardState oldKeyboardState;
        SpriteBatch spriteBatch;
        public SpriteFont spriteFont;
        public Vector2 position;
        float width = 0f;
        float height = 0f;
        public bool IsEnterPressed;
        public float Spacing = 5;


        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                if (selectedIndex < 0)
                    selectedIndex = 0;
                if (selectedIndex >= menuItems.Length)
                    selectedIndex = menuItems.Length - 1;
            }
        }
 
        public MenuComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont spriteFont,
            string[] menuItems)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
            this.menuItems = menuItems;
            MeasureMenu();
        }

        private void MeasureMenu()
        {
            height = 0;
            width = 0;

            foreach (string item in menuItems)
            {
                Vector2 size = spriteFont.MeasureString(item);
                if (size.X > width)
                    width = size.X;
                height += spriteFont.LineSpacing + 5;
            }

            position = new Vector2(
                (Renderer.Width - width) / 2,
                (Renderer.Height - height) / 2);
        }
 
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (GeneralManager.Singleton.CheckKey(Keys.Down))
            {
                GeneralManager.SoundManager.PlaySound("beep");
                selectedIndex++;
                if (selectedIndex == menuItems.Length)
                    selectedIndex = 0;
            }

            if (GeneralManager.Singleton.CheckKey(Keys.Up))
            {
                GeneralManager.SoundManager.PlaySound("beep");
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = menuItems.Length - 1;
            }

            if (GeneralManager.Singleton.CheckKey(Keys.Enter))
            {
                IsEnterPressed = true;
                GeneralManager.SoundManager.PlaySound("beep");
            }
            else IsEnterPressed = false;

            base.Update(gameTime);

            oldKeyboardState = keyboardState;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Vector2 location = position;
            Color tint;
            Vector2 Size;

            spriteBatch.Draw(Renderer.Singleton.TextBackground, new Rectangle((int)location.X - 100, (int)location.Y -50, (int)width * 2, (int)(height) + 100) , Color.White);
            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Size = new Vector2(1.2f, 1.1f);
                    tint = hilite;
                }
                else
                {
                    Size = new Vector2(1.0f, 1.0f);
                    tint = normal;
                }
                
                spriteBatch.DrawString(
                    spriteFont,
                    menuItems[i],
                    location,
                    tint,
                    0,
                    Vector2.Zero,
                    Size,
                    SpriteEffects.None,
                    0);
                location.Y += spriteFont.LineSpacing + Spacing;
            }
        }

        public override void CatchClick()
        {

        }
    }
}