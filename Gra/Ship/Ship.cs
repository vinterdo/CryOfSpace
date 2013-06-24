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
    
    public class Ship : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Hull Hull;
        //List<Crew> Crew = new List<Crew>();
        //List<Distaster> Distasters = new List<Distaster>();

        Texture2D InsideTex;

        public List<Component> Components;

        public VertexScreen CurrentVertex;
        public VertexScreen DestinationVertex;
        public float TimeToArrival = 0.0f;

        public Animation OutsideView;
        public Animation InsideView;
        public Animation ConduitsView;
        public Animation Explosion;
        public Animation Wreck;

        public float Angle;
        public Vector2 Position;
        public Vector2 DrawPosition;
        public Vector2 Speed;

        public ShipState State;

        static RenderTarget2D Target;

        //object @null = 0x0f as object;

        public bool ShipView = false;// 0 - outside, 1 - inside

        //===== Statistics: ==========
        float HyperspaceSpeed = 10.0f;

        public Ship(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            Hull.OutsideView.RegisterAnimation();
            OutsideView = Renderer.Animations["ship2"];

            Hull.InsideView.RegisterAnimation();
            InsideView = Renderer.Animations["ship2-inside"];
            

            base.Initialize();


            PresentationParameters pp = GraphicsDevice.PresentationParameters;
            Target = new RenderTarget2D(GraphicsDevice, Hull.SizeX, Hull.SizeY, 1, SurfaceFormat.Color, pp.MultiSampleType, pp.MultiSampleQuality);

            Components = new List<Component>();
            Components.Add(new Engine(Game));

        }

        public override void Update(GameTime gameTime)
        {
            if (GeneralManager.Singleton.CheckLMB() && GeneralManager.Singleton.CheckCollision(GeneralManager.Singleton.MousePos, new Rectangle((int)(DrawPosition.X - Hull.Center.X), (int)(DrawPosition.Y - Hull.Center.Y), Hull.SizeX, Hull.SizeY), Angle, DrawPosition))
            {
                ShipView = !ShipView;
            }

            foreach (Slot S in Hull.Slots)
            {
                if (S.Component != null)
                {
                    if (S.Component is Weapon)
                    {
                        (S.Component as Weapon).Update(gameTime, DrawPosition);
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

                    if (GeneralManager.Singleton.keyboardState.IsKeyDown(Keys.W))
                    {
                        Speed += GeneralManager.Singleton.GetVectorFromAngle(-1 * Angle + (float)Math.PI / 2) / 1000;
                    }
                    if (GeneralManager.Singleton.keyboardState.IsKeyDown(Keys.D))
                    {
                        Angle += 0.002f;
                    }
                    if (GeneralManager.Singleton.keyboardState.IsKeyDown(Keys.A))
                    {
                        Angle -= 0.002f;
                    }
                    if (GeneralManager.Singleton.keyboardState.IsKeyDown(Keys.S))
                    {
                        Speed *= 0.95f;
                    }

                    Position += Speed;

                    OutsideView.Position = DrawPosition;
                    InsideView.Update(gameTime);
                    OutsideView.Update(gameTime);
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

            base.Update(gameTime);
        }

        public void SetDrawPosition(Vector2 Offset)
        {
            DrawPosition = Position - Offset;
        }

        public void DrawOutside(GameTime gameTime)
        {
            OutsideView.Draw(gameTime, Angle, Hull.Center);
            foreach (Slot S in Hull.Slots)
            {
                if (S.Component != null)
                {
                    if (S.Component is Weapon)
                    {
                        (S.Component as Weapon).DrawBullets(gameTime, DrawPosition);
                    }
                }
            }
        }

        public void DrawInside(GameTime gameTime)
        {

            Renderer.Singleton.batch.Draw(InsideTex, DrawPosition , null, Color.White, Angle, Hull.Center, 1.0f, SpriteEffects.None, 1.0f);

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
            //InsideView.SetPosition(new Vector2(Renderer.Width/2, Renderer.Height/2) - Hull.Center);
            //InsideView.Draw(gameTime);
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
    }
}