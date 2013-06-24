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
            base.GenerationChance = 0.6f;
            base.Opacity = 0.7f;
            base.OpacityChange = 0.01f;
            base.AngularSpeed = 0.01f;
            base.Direction = 0.0f;
            base.DirectionOffset = (float)(Math.PI * 2);
            base.PartTex = Renderer.Singleton.SmokeParticle;
            base.Position = Position;
            base.Speed = 1f;
            base.SpeedOffset = 0.5f;
            base.ScaleChange = 0.03f;
            base.Scale = 1f;
            base.OpacityOffset = 0.5f;
            base.ScaleOffset = 1.5f;
            base.PositionOffset = 30f;
            base.AngleOffset = (float)Math.PI * 2f;
            base.ParticleMaxLife = 5000f;
            
        }

    }
}