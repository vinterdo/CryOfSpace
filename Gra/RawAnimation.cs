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
    public class RawAnimation
    {
        int NoFrames = 1;
        Vector2 FrameSize = Vector2.One;
        float TimePerFrame = 1;
        Vector2 Position = Vector2.Zero;
        //Texture2D Frames;
        public string TextureName = "";

        public void SetProperties(Vector2 FrameSize, float TimePerFrame, int NoFrames)
        {
            this.FrameSize = FrameSize;
            this.TimePerFrame = TimePerFrame;
            this.NoFrames = NoFrames;
        }

        public Animation CreateAnimation()
        {
            Animation tmp = new Animation(Renderer.Singleton.Game);
            tmp.LoadTex(Renderer.Singleton.Content.Load<Texture2D>(TextureName));
            tmp.SetProperties(FrameSize, TimePerFrame, NoFrames);
            return tmp;
        }

        public void RegisterAnimation()
        {
            Renderer.Animations.Add(TextureName, CreateAnimation());
        }
    }
}
