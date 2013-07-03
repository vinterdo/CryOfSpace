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
    public class Indicator : GuiElement
    {
        public float LifeTime;
        public float CurrentLife;
        public string Name;
        public float Opacity;
        public float OpacityChange;
        public Vector2 Position;
        public Vector2 Speed;
        public float Scale;
        public float ScaleChange;
        public Color BaseColor;
        public Color TargetColor;
        SpriteFont Font;


        public Indicator(Game game)
            : base(game)
        {
            Font = Renderer.Fonts["Coalition"];
            // TODO: Construct any child components here
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            Position += Speed;
            Opacity += OpacityChange;
            Scale += ScaleChange;
            CurrentLife += gameTime.ElapsedGameTime.Milliseconds;
            if (CurrentLife > LifeTime)
            {
                // TODO - dodaæ usuwanie
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            float Progress = CurrentLife/LifeTime;
            Renderer.Singleton.batch.DrawString(Font, Name, Position, new Color(BaseColor.R * (1f - Progress) + TargetColor.R * Progress, BaseColor.G * (1f - Progress) + TargetColor.G * Progress, BaseColor.B * (1f - Progress) + TargetColor.B * Progress, Opacity), 0f, Font.MeasureString(Name) * Scale / 2f, Scale, SpriteEffects.None, 0.1f);

            base.Draw(gameTime);
        }

        public override void CatchClick()
        {

        }
    }
}