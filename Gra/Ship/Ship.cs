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
    
    public class Ship : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Hull Hull;
        //List<Crew> Crew = new List<Crew>();
        //List<Distaster> Distasters = new List<Distaster>();

        Texture2D InsideTex;


        public VertexScreen CurrentVertex;
        public VertexScreen DestinationVertex;
        public float TimeToArrival = 0.0f;

        public Animation OutsideView;
        public Animation OutsideColor;

        public Animation InsideView;

        public float Angle;
        public Vector2 Position;
        public Vector2 DrawPosition;
        public Vector2 Speed;
        public float AccelerationPercent = 0;

        public ShipState State;

        RenderTarget2D Target;

        public Color ShipColor;

        public SoundEffectInstance EngineSound;

        public int HitPoints;


        public bool ShipView = true;// 0 - outside, 1 - inside

        //===== Statistics: ==========
        float HyperspaceSpeed = 10.0f;

        public Ship(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            OutsideView = Hull.OutsideView.CreateAnimation();
            InsideView = Hull.InsideView.CreateAnimation();

            EngineSound = Renderer.Singleton.Content.Load<SoundEffect>("Engines").CreateInstance();
            EngineSound.IsLooped = true;
            EngineSound.Play();            

            if (Hull.OutsideColor != null)
            {
                OutsideColor = Hull.OutsideColor.CreateAnimation();
            }
            
            base.Initialize();

            HitPoints = (int)Hull.BasicHull;

            PresentationParameters pp = GraphicsDevice.PresentationParameters;
            Target = new RenderTarget2D(GraphicsDevice, Hull.SizeX, Hull.SizeY, 1, SurfaceFormat.Color, pp.MultiSampleType, pp.MultiSampleQuality);


        }

        public override void Update(GameTime gameTime)
        {
            foreach (Slot S in Hull.Slots)
            {
                if (S.Component != null)
                {
                    if (S.Component is Weapon)
                    {
                        
                        Weapon Weapon = (S.Component as Weapon);
                        Weapon.Update(gameTime);
                        Weapon.DetectCollisions(this.CurrentVertex, this);

                        if (Weapon.ShootAnim.CurrentFrame > 0)
                        {
                            Weapon.ShootAnim.Update(gameTime);
                        }

                    }
                    else if (S.Component is MiningLaser)
                    {
                        (S.Component as MiningLaser).Update(gameTime, DrawPosition);
                    }
                    else
                    {
                        S.Component.Update(gameTime);
                    }
                }       
            }

            switch (State)
            {
                case ShipState.InVertex:

                    Position += Speed;
                    if (AccelerationPercent > 0)
                    {
                        Speed += GeneralManager.Singleton.GetVectorFromAngle(- Angle + (float)Math.PI * 0.5f) * GetSpeed * AccelerationPercent;
                    }

                    if (Speed.Length() > GetMaxSpeed * AccelerationPercent)
                    {
                        Speed.Normalize();
                        Speed *= GetMaxSpeed * AccelerationPercent;
                    }

                    OutsideView.Position = DrawPosition;
                    if (OutsideColor != null)
                    {
                        OutsideColor.Position = DrawPosition;
                    }
                    InsideView.Update(gameTime);
                    OutsideView.Update(gameTime);
                    if (OutsideColor != null)
                    {
                        OutsideColor.Update(gameTime);
                    }
                    break;

                case ShipState.Travelling:
                    TimeToArrival -= HyperspaceSpeed;
                    if (TimeToArrival <= 0)
                    {
                        GeneralManager.SoundManager.PlaySound("DroppHSpace");
                        CurrentVertex = DestinationVertex;
                        CurrentVertex.Ships.Add(this);
                        State = ShipState.InVertex;
                    }
                    break;
            }

            SetEngineEmmiters();
            // ====================== HP HAndling ======

            if (HitPoints < 0)
            {
                HitPoints = 0;
                this.Hull.Wreck.Position = this.Position;
                this.Hull.Wreck.Angle = this.Angle;
                CurrentVertex.Components.Add(this.Hull.Wreck);
                CurrentVertex.Ships.Remove(this);
                CurrentVertex.Effect.Parameters["BloomIntensity"].SetValue(1000f);
            }

            //=-===============================
            /*
            if (Angle < Math.PI * 2)
            {
                Angle += (float)Math.PI * 2f;
            }
            if (Angle > Math.PI * 2)
            {
                Angle -= (float)Math.PI * 2f;
            }*/


            //Sound
            EngineSound.Volume = AccelerationPercent;
            //===

            base.Update(gameTime);
        }

        public void SetDrawPosition(Vector2 Offset)
        {
            DrawPosition = Position - Offset;
        }

        public void DrawOutside(GameTime gameTime)
        {
            DrawEngineEmmiters(gameTime);

            foreach (Slot S in Hull.Slots)
            {
                if (S.Component != null)
                {
                    if (S.Component is Weapon)
                    {
                        (S.Component as Weapon).DrawBullets(gameTime, DrawPosition);
                    }
                    if (S.Component is MiningLaser)
                    {
                        (S.Component as MiningLaser).DrawBeam(gameTime);
                    }
                }
            }

            OutsideView.Draw(gameTime, Angle, Hull.Center);
            if (OutsideColor != null)
            {
                OutsideColor.Draw(gameTime, Angle, Hull.Center, ShipColor);
            }
            DrawWeapons(gameTime);
        }

        public void DrawInside(GameTime gameTime)
        {

            DrawEngineEmmiters(gameTime);

            foreach (Slot S in Hull.Slots)
            {
                if (S.Component != null)
                {
                    if (S.Component is Weapon)
                    {
                        (S.Component as Weapon).DrawBullets(gameTime, DrawPosition);
                    }
                    if (S.Component is MiningLaser)
                    {
                        (S.Component as MiningLaser).DrawBeam(gameTime);
                    }
                }
            }

            Renderer.Singleton.batch.Draw(InsideTex, DrawPosition, null, Color.White, Angle, Hull.Center, 1.0f, SpriteEffects.None, 1.0f);

            DrawWeapons(gameTime);
        }

        public void FlyTo(VertexScreen V)
        {
            if (CurrentVertex != null)
            {
                GeneralManager.SoundManager.PlaySound("Jump");
                GeneralManager.SoundManager.PlaySound("WarpJump");
                State = ShipState.Travelling;
                CurrentVertex.Ships.Remove(this);
                CurrentVertex = null;
                DestinationVertex = V;
                TimeToArrival = 10000.0f;
            }
        }

        public enum ShipState
        {
            InVertex,
            WaitingForTravel,
            Travelling
        }

        public void DrawProjectView(GameTime gameTime)
        {
            Renderer.Singleton.batch.Draw(InsideTex, new Vector2(Renderer.Width / 2, Renderer.Height / 2) - Hull.Center, Color.White);
        }

        public void CreateInsideTex(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(0, Target);
            Renderer.Singleton.batch.Begin();
            GraphicsDevice.Clear(Color.TransparentBlack);
            InsideView.Position = Vector2.Zero;

            InsideView.Draw(gameTime);
            foreach (Slot S in Hull.Slots)
            {
                Rectangle DestRect = new Rectangle((int)S.Position.X - 15, (int)S.Position.Y -15, 30, 30);
                Renderer.Singleton.batch.Draw(Renderer.Singleton.SlotBackground, DestRect, Color.White);

                if (S.Component != null)
                {
                    //S.Component.Anim.Draw(gameTime, 0.0f, S.Component.Position);
                    Renderer.Singleton.batch.Draw(S.Component.Tex, DestRect, Color.White);
                }
                
            }

            Renderer.Singleton.batch.End();

            
            GraphicsDevice.SetRenderTarget(0, null);

            InsideTex = Target.GetTexture();
            GraphicsDevice.Clear(Color.Black);
        }

        public void Accelerate()
        {
            //this.Speed += GeneralManager.Singleton.GetVectorFromAngle(-1 * Angle + (float)Math.PI / 2) / 700;
            AccelerationPercent += 0.1f;
            if (AccelerationPercent > 1)
            {
                AccelerationPercent = 1;
            }
        }

        public void TurnLeft()
        {
            Angle -= 0.007f;
        }

        public void TurnRight()
        {
            Angle += 0.007f;
        }

        public void Break()
        {
            AccelerationPercent -= 0.1f;
            if (AccelerationPercent < 0)
            {
                AccelerationPercent = 0;
            }
            //Speed *= 0.95f;
        }

        public void FlyTo(Vector2 Target)
        {
           float Angle = GeneralManager.Singleton.GetAngleFromVector((Target - this.Position));
            float Distance = (Target - Position).Length();

            if (this.Angle > (float)Math.PI / 2 - Angle - 0.5f && this.Angle < (float)Math.PI / 2 - Angle + 0.5f)
            {
                if (Distance > 400)
                {
                    Accelerate();
                }
                if (Distance < 400)
                {
                    Break();
                }
            }
            else
            {
                Break();
            }

            SwitchToAngle((float)Math.PI /2 - Angle);

        }

        public void SwitchToAngle(float Angle)
        {
            if (Angle - this.Angle < 0f && Angle - this.Angle > -1 * Math.PI)
            {
                TurnLeft();
            }
            else
            {
                TurnRight();
            }
        }


        public void Shoot(Weapon Weapon, Vector2 Target)
        {
            Weapon.Shoot(Target);
            Weapon.ShootAnim.CurrentFrame = 1;
        }


        public void SetEngineEmmiters()
        {
            int i = 0;

            foreach (ParticleEmitter P in Hull.AccelerationEngines)
            {
                P.Position = this.DrawPosition + GeneralManager.RotateVector(Hull.AccelerationOffset[i],  this.Angle, Vector2.Zero);
                P.Direction = (float)- Math.PI/2f - this.Angle;
                i++;
                P.GenerationChance = AccelerationPercent;
                P.Speed = AccelerationPercent * 2;
            }

            foreach (ParticleEmitter P in Hull.LeftEngines)
            {
                P.Position = this.DrawPosition;
                //P.Direction = this.Angle + (float)Math.PI;
            }

            foreach (ParticleEmitter P in Hull.RightEngines)
            {
                P.Position = this.DrawPosition;
                //P.Direction = this.Angle + (float)Math.PI;
            }
        }

        public void DrawEngineEmmiters(GameTime gameTime)
        {
            foreach (ParticleEmitter P in Hull.AccelerationEngines)
            {
                P.Draw(gameTime); 
            }

            foreach (ParticleEmitter P in Hull.LeftEngines)
            {
                P.Draw(gameTime); 
            }

            foreach (ParticleEmitter P in Hull.RightEngines)
            {
                P.Draw(gameTime); 
            }
        }

        public void DrawWeapons(GameTime gameTime)
        {
            int i =0;
            foreach (Slot S in Hull.Slots)
            {
                if (S.Component is Weapon)
                {
                    Weapon W = (S.Component as Weapon);
                    W.ShootAnim.Position = this.DrawPosition + GeneralManager.RotateVector(Hull.WeaponPositions[i], this.Angle, Vector2.Zero); ;
                    //W.ShootAnim.Draw(gameTime, this.Angle, this.DrawPosition);
                    W.ShootAnim.Draw(gameTime, this.Angle, Vector2.Zero);
                    i++;
                }
            }
        }

        public float GetSpeed
        {
            get
            {
                return 0.02f;
            }
            set
            {
            }
        }
        public float GetMaxSpeed
        {
            get
            {
                return 2f;
            }
            set
            {
            }
        }

    }
}