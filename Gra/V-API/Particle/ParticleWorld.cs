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
    class ParticleWorld
    {
        static List<ParticleEmitter> Emitters = new List<ParticleEmitter>();

        public static int ParticleCount
        {
            get
            {
                int Count = 0;
                foreach (ParticleEmitter E in Emitters)
                {
                    Count += E.ParticleCount;
                }

                return Count;
            }
        }

        public static void Update(GameTime gameTime)
        {
            foreach (ParticleEmitter E in Emitters)
            {
                E.Update(gameTime);
            }
        }

        public static void Register(ParticleEmitter E)
        {
            Emitters.Add(E);
        }
    }
}
