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
    public class CheckBox : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Texture2D TexOff;
        public Texture2D TexOn;

        public bool State = false;

        public Rectangle Rect;

        public CheckBox(Game game, Rectangle Rect, Texture2D TexOn, Texture2D TexOff)
            : base(game)
        {
            this.Rect = Rect;
            this.TexOff = TexOff;
            this.TexOn = TexOn;
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (GeneralManager.Singleton.CheckCollision(GeneralManager.Singleton.MousePos, Rect) && GeneralManager.Singleton.CheckLMB())
            {
                State = !State;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!State)
            {
                Renderer.Singleton.batch.Draw(TexOff, Rect, Color.White);
            }
            else
            {
                Renderer.Singleton.batch.Draw(TexOn, Rect, Color.White);
            }
            base.Draw(gameTime);
        }
    }
}