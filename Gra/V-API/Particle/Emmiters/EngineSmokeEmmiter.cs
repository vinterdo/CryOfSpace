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
    public class EngineSmokeEmmiter : ParticleEmitter
    {
        public EngineSmokeEmmiter(Game game, Vector2 Position, float DirectionAngle)
            : base(game)
        {
            base.GenerationChance = 0.6f;
            base.Opacity = 0.8f;
            base.OpacityChange = 0.015f;
            base.AngularSpeed = 0.01f;
            base.Direction = DirectionAngle;
            base.DirectionOffset = (float)(Math.PI * 0.01);
            base.PartTex = Renderer.Textures["SmokeParticle"];
            base.Position = Position;
            base.Speed = 1f;
            base.SpeedOffset = 0.5f;
            base.ScaleChange = -0.18f;
            base.Scale = 0.3f;
            base.OpacityOffset = 0.5f;
            base.ScaleOffset = 0.1f;
            base.PositionOffset = 1f;
            base.AngleOffset = (float)Math.PI * 2f;
            base.ParticleMaxLife = 5000f;
        }

    }
}