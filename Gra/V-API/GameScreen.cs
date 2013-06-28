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
    public abstract class GameScreen :
      Microsoft.Xna.Framework.DrawableGameComponent
    {
        List<DrawableGameComponent> components = new List<DrawableGameComponent>();
        protected Game game;

        public enum State
        {
            FadeIn,
            FadeOut,
            Normal
        }
        public float Fade = 0f;
        public State ScreenState = State.Normal;
        public Color FadeColor = Color.Black;

        public bool FadeOutFinished = false;
        public bool FadeInFinished = false;

        public GameScreen Target;


        public List<DrawableGameComponent> Components
        {
            get { return components; }
        }

        public GameScreen(Game game)
            : base(game)
        {
            this.Visible = false;
            this.game = game;
        }
 
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (GameComponent component in components)
                if (component.Enabled == true)
                    component.Update(gameTime);

            if (FadeOutFinished)
                FadeOutFinished = false;
            if (FadeInFinished)
                FadeInFinished = false;

            if (ScreenState == State.FadeIn)
            {
                Fade -= 0.02f;
                if (Fade <= 0)
                {
                    Fade = 0;
                    ScreenState = State.Normal;
                    FadeInFinished = true;
                }
            }

            if (ScreenState == State.FadeOut)
            {
                Fade += 0.02f;
                if (Fade >= 1)
                {
                    Fade = 1;
                    ScreenState = State.Normal;
                    FadeOutFinished = true;
                }
            }

            if (FadeOutFinished && Target!= null)
            {
                this.Visible = false;
                Target.Visible = true;
                Target.ScreenState = State.FadeIn;
                Target.Fade = 1f;
                Target = null;
            }

        }
 
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (GameComponent component in components)
            {
                if (component is DrawableGameComponent &&
                    ((DrawableGameComponent)component).Visible)
                    ((DrawableGameComponent)component).Draw(gameTime);
            }

            Renderer.Singleton.batch.Draw(Renderer.Singleton.SlotBackground, new Rectangle(0, 0, Renderer.Width, Renderer.Height), new Color(FadeColor, Fade));
        }

        public virtual void Show()
        {
            this.Visible = true;
            this.Enabled = true;
            foreach (GameComponent component in components)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = true;
            }
        }

        public virtual void Hide()
        {
            this.Visible = false;
            this.Enabled = false;
            foreach (GameComponent component in components)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = false;
            }
        }
 
    }

}