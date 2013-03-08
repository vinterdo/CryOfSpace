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
    public class IpSelectionScreen : GameScreen
    {
        TextBox IpTextBox;

        public IpSelectionScreen(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
        }

        public override void Initialize()
        {
            IpTextBox = new TextBox(Game, spriteBatch);
            IpTextBox.Initialize();
            IpTextBox.IsFocused = true;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                IpTextBox.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                IpTextBox.Draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }
}