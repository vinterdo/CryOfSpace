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
    public class SmokeEmmiter : ParticleEmitter
    {
        public SmokeEmmiter(Game game, Vector2 Position)
            : base(game)
        {
            base.GenerationChance = 0.1f;
            base.Opacity = 1f;
            base.OpacityChange = 0.01f;
            base.AngularSpeed = 0.01f;
            base.Direction = 0.0f;
            base.DirectionOffset = (float)(Math.PI * 2);
            base.PartTex = Renderer.Singleton.SmokeParticle;
            base.Position = Position;
            base.Speed = 1f;
            base.SpeedOffset = 0.05f;
            base.ScaleChange = -0.03f;
            base.Scale = 1f;
            base.OpacityOffset = 0.2f;
            base.ScaleOffset = 1.5f;
            
        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
    }
}