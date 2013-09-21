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


namespace CryOfSpace
{
    public sealed class SoundManager : Microsoft.Xna.Framework.GameComponent
    {
        public Dictionary<string, SoundEffect> Sounds;
        public Dictionary<string, SoundEffectInstance> Looped;
        public SoundManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public override void Initialize()
        {
            Sounds = new Dictionary<string, SoundEffect>();
            Looped = new Dictionary<string, SoundEffectInstance>();

            LoadSound("beep");
            LoadSound("Jump");
            LoadSound("Gauss_Cannon");
            LoadSound("Engines");

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }

        public void PlaySound(string Name)
        {
            Sounds[Name].Play();
        }

        public void PlaySound(string Name, float Volume)
        {
            Sounds[Name].Play(Volume, 0f, 0f);
        }

        public void LoadSound(string Name)
        {
            Sounds.Add(Name, Renderer.Singleton.Content.Load<SoundEffect>(Name));
        }

        public void DisposeSound(string Name)
        {
            Sounds.Remove(Name);
        }

        public void PlayInLoop(string Name)
        {
            Looped.Add(Name, Sounds[Name].CreateInstance());
            Looped[Name].IsLooped = true;
            Looped[Name].Play();
        }

        public void PlayInLoop(string Name, float Volume)
        {
            Looped.Add(Name, Sounds[Name].CreateInstance());
            Looped[Name].IsLooped = true;
            Looped[Name].Play();
            Looped[Name].Volume = Volume;
        }

        public void UnLoop(string Name)
        {
            Looped[Name].Stop();
            Looped.Remove(Name);
        }
    }
}