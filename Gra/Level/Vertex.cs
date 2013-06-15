using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    [Serializable]
    public class Vertex:DrawableGameComponent,ICloneable
    {
        public static bool MinimapEnabled = true;

        public Vector2 Position;
        public Texture2D Tex;
        SpriteBatch spriteBatch;
        public Rectangle Rect;
        public int Size = 5000;
        public Vector2 BackgroundScale = new Vector2(1.5f, 1.5f);
        public Texture2D Background;
        public Rectangle Camera = new Rectangle(0,0, Renderer.Width, Renderer.Height);
        public bool IsMenuOpened = false;

        public List<Ship> Ships;

        public List<Vertex> Connections;

        public List<VertexComponent> Components;


        Vector2 ShipIndicatorPosition;
        

        public Vertex (Game game, Vector2 Pos, Texture2D Tex):base(game)
        {
            Ships = new List<Ship>();
            this.Position = Pos;
            this.Tex = Tex;
            this.Rect = new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)Tex.Width, (int)Tex.Height);
            spriteBatch = Renderer.Singleton.batch;
            Connections = new List<Vertex>();
            Components = new List<VertexComponent>();

            
            

        }

        public void DrawOutside(GameTime gameTime)
        {
            Vector2 ScreenPosition = (Position) * new Vector2((float)(Renderer.Width * 0.6) / 500, (float)(Renderer.Height * 0.6) / 500) - new Vector2(15, 15) + new Vector2((float)Renderer.Width * 0.2f, (float)Renderer.Height * 0.2f);
            spriteBatch.Draw(Tex,ScreenPosition, null, Color.White, 0.0f, Vector2.One, 1.0f, SpriteEffects.None, 0.5f);


            if (Ships.Count > 0)
            {
                spriteBatch.Draw(Renderer.Singleton.PlayerIndicator,ScreenPosition + new Vector2(30, 30), Color.White);
            }

            base.Draw(gameTime);
        }


        public bool CheckFlightAbility()
        {
            bool HasConnection = false;

            if (!(this.Equals(GeneralManager.Singleton.CurrentPlayer.Ship.CurrentVertex)) && GeneralManager.Singleton.CurrentPlayer.Ship.State == Ship.ShipState.InVertex)
            {
                
                foreach (Vertex V in Connections)
                {
                    if (V.Equals(GeneralManager.Singleton.CurrentPlayer.Ship.CurrentVertex)) HasConnection = true;
                }
                
            }

            return HasConnection;
        }

        public void DrawMenu(GameTime gameTime)
        {
            Vector2 ScreenPosition = (Position) * new Vector2((float)(Renderer.Width * 0.6) / 500, (float)(Renderer.Height * 0.6) / 500) - new Vector2(15, 15) + new Vector2((float)Renderer.Width * 0.2f, (float)Renderer.Height * 0.2f) + new Vector2(15,15);

            if (IsMenuOpened)
            {
                if (ScreenPosition.Y < Renderer.Height / 2)
                {
                    // Box of menu
                    spriteBatch.Draw(Renderer.Singleton.VertexMenuBackground, new Rectangle((int)(ScreenPosition.X - Renderer.Width * 0.1), (int)ScreenPosition.Y, (int)(Renderer.Width * 0.1) + 15, (int)(Renderer.Height * 0.2)), null, Color.White, 0.0f, Vector2.One, SpriteEffects.None, 0.4f);
                    // View Button
                    spriteBatch.Draw(Renderer.Singleton.ViewVertexButton,new Rectangle((int)(ScreenPosition.X - Renderer.Width * 0.1), (int)(ScreenPosition.Y + Renderer.Height * 0.03f), (int)(Renderer.Width * 0.1) + 15, (int)(Renderer.Height * 0.03)), null, Color.White, 0.0f, Vector2.One, SpriteEffects.None, 0.3f);
                    
                        if (CheckFlightAbility()) // ad connection chceck
                        {
                            spriteBatch.Draw(Renderer.Singleton.FlyToVertexButton, new Rectangle((int)(ScreenPosition.X - Renderer.Width * 0.1), (int)(ScreenPosition.Y + Renderer.Height * 0.06f), (int)(Renderer.Width * 0.1) + 15, (int)(Renderer.Height * 0.03)), null, Color.White, 0.0f, Vector2.One, SpriteEffects.None, 0.3f);
                        }
                    
                }
                else
                {
                    // Box of menu
                    spriteBatch.Draw(Renderer.Singleton.VertexMenuBackground, new Rectangle((int)(ScreenPosition.X - Renderer.Width * 0.1), (int)(ScreenPosition.Y - (Renderer.Width * 0.1) - 15), (int)(Renderer.Width * 0.1) + 15, (int)(Renderer.Height * 0.2)), null, Color.White, 0.0f, Vector2.One, SpriteEffects.FlipVertically, 0.4f);
                    // View Button
                    spriteBatch.Draw(Renderer.Singleton.ViewVertexButton, new Rectangle((int)(ScreenPosition.X - Renderer.Width * 0.1), (int)(ScreenPosition.Y - (Renderer.Width * 0.1) - 15  + Renderer.Height * 0.03f), (int)(Renderer.Width * 0.1) + 15, (int)(Renderer.Height * 0.03)), null, Color.White, 0.0f, Vector2.One, SpriteEffects.None, 0.3f);
                    if (!(this.Equals(GeneralManager.Singleton.CurrentPlayer.Ship.CurrentVertex)) && GeneralManager.Singleton.CurrentPlayer.Ship.State == Ship.ShipState.InVertex)
                    {
                        bool HasConnection = false;
                        foreach (Vertex V in Connections)
                        {
                            if (V.Equals(GeneralManager.Singleton.CurrentPlayer.Ship.CurrentVertex)) HasConnection = true;
                        }
                        if (HasConnection)
                        {
                            spriteBatch.Draw(Renderer.Singleton.FlyToVertexButton, new Rectangle((int)(ScreenPosition.X - Renderer.Width * 0.1), (int)(ScreenPosition.Y - (Renderer.Width * 0.1) - 15 + Renderer.Height * 0.06f), (int)(Renderer.Width * 0.1) + 15, (int)(Renderer.Height * 0.03)), null, Color.White, 0.0f, Vector2.One, SpriteEffects.None, 0.3f);
                        }
                    }
                }
            }
        }

        public void DrawInside(GameTime gameTime)
        {
            
            if (Visible)
            {
                spriteBatch.Draw(Renderer.Singleton.Background, new Rectangle(0, 0, Camera.Width, Camera.Height), new Rectangle((int)(Camera.X / 10), (int)(Camera.Y / 10), (int)(Renderer.Singleton.Background.Width / BackgroundScale.X), (int)(Renderer.Singleton.Background.Height / BackgroundScale.Y)), Color.White);

                

                bool IsCurrentPlayerOnVertex = false;

                foreach (Ship S in Ships)
                {
                    if (S.Equals(GeneralManager.Singleton.CurrentPlayer.Ship) && S.State == Ship.ShipState.InVertex)
                    {
                        IsCurrentPlayerOnVertex = true;
                    }
                    if (S.State == Ship.ShipState.InVertex)
                    {
                        if (S.ShipView)
                        {
                            S.DrawOutside(gameTime);
                        }
                        else
                        {
                            S.DrawInside(gameTime);
                        }
                    }
                }


                foreach (VertexComponent C in Components)
                {
                    C.Draw(gameTime);
                }

                if (IsCurrentPlayerOnVertex)
                {
                    if (!Camera.Intersects(new Rectangle((int)GeneralManager.Singleton.CurrentPlayer.Ship.Position.X, (int)GeneralManager.Singleton.CurrentPlayer.Ship.Position.Y, (int)GeneralManager.Singleton.CurrentPlayer.Ship.OutsideView.FrameSize.X, (int)GeneralManager.Singleton.CurrentPlayer.Ship.OutsideView.FrameSize.Y)))
                    {
                        ShipIndicatorPosition = GeneralManager.Singleton.CurrentPlayer.Ship.Position - new Vector2(Camera.X, Camera.Y);

                        short Direction = 0; // 0 - top, 1 - right, 2 - bottom, 3- left

                        if (ShipIndicatorPosition.X < 100)
                        {
                            Direction = 3;
                            ShipIndicatorPosition.X = 100;
                        }
                        else if (ShipIndicatorPosition.X > Camera.Width - 100)
                        {
                            Direction = 1;
                            ShipIndicatorPosition.X = Camera.Width - 100;
                        }
                        if (ShipIndicatorPosition.Y < 100)
                        {
                            Direction = 0;
                            ShipIndicatorPosition.Y = 100;
                        }
                        else if (ShipIndicatorPosition.Y > Camera.Height - 100)
                        {
                            Direction = 2;
                            ShipIndicatorPosition.Y = Camera.Height - 100;
                        }
                        switch (Direction)
                        {
                            case 0:
                                spriteBatch.Draw(Renderer.Singleton.ShipIndicator, ShipIndicatorPosition - new Vector2(Renderer.Singleton.ShipIndicator.Width / 2, Renderer.Singleton.ShipIndicator.Height / 2), Color.White);
                                break;
                            case 1:
                                spriteBatch.Draw(Renderer.Singleton.ShipIndicator, new Vector2((ShipIndicatorPosition.X - (Renderer.Singleton.ShipIndicator.Width / 2)), (ShipIndicatorPosition.Y - Renderer.Singleton.ShipIndicator.Height / 2)), null, Color.White, (float)(0.5f * Math.PI), new Vector2(50, 50), Vector2.One, SpriteEffects.None, 0.0f);
                                break;
                            case 2:
                                spriteBatch.Draw(Renderer.Singleton.ShipIndicator, new Vector2((ShipIndicatorPosition.X - (Renderer.Singleton.ShipIndicator.Width / 2)), (ShipIndicatorPosition.Y - Renderer.Singleton.ShipIndicator.Height / 2)), null, Color.White, (float)(1.0f * Math.PI), new Vector2(50, 50), Vector2.One, SpriteEffects.None, 0.0f);
                                break;
                            case 3:
                                spriteBatch.Draw(Renderer.Singleton.ShipIndicator, new Vector2((ShipIndicatorPosition.X - (Renderer.Singleton.ShipIndicator.Width / 2)), (ShipIndicatorPosition.Y - Renderer.Singleton.ShipIndicator.Height / 2)), null, Color.White, (float)(1.5f * Math.PI), new Vector2(50, 50), Vector2.One, SpriteEffects.None, 0.0f);
                                break;
                        }

                    }

                }
                
                spriteBatch.Draw(Renderer.Singleton.FromVertexToLevelGUI, Vector2.Zero, Color.White);
                DrawMinimap();
                Renderer.Singleton.DrawMoney();
            }
        }


        public object Clone()
        {
            return new Vertex(Game, this.Position, this.Tex);

        }

        public float GetLenghtFrom(Vertex Vertex)
        {
            return Vector2.Distance(this.Position, Vertex.Position);
        }

        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {

                foreach (VertexComponent C in Components)
                {

                    C.SetDrawPosition(new Vector2(Camera.X, Camera.Y));
                    C.Update(gameTime);
                }
                if (GeneralManager.Singleton.GameState == 2)
                {
                    if (GeneralManager.Singleton.CheckLMB() && GeneralManager.Singleton.MousePos.X > 40 && GeneralManager.Singleton.MousePos.X < 170 && GeneralManager.Singleton.MousePos.Y > 25 && GeneralManager.Singleton.MousePos.Y < 45)
                    {
                        GeneralManager.Singleton.GameState = 1;
                        GeneralManager.SoundManager.PlaySound("beep");


                    }

                    if (GeneralManager.Singleton.MousePos.X < 100 && Camera.X > 0)
                        Camera.X -= (int)(100 - GeneralManager.Singleton.MousePos.X) / 10;
                    if (GeneralManager.Singleton.MousePos.X > Renderer.Width - 100 && Camera.X < Size - Renderer.Width)
                        Camera.X += (int)(100 - (Renderer.Width - GeneralManager.Singleton.MousePos.X)) / 10;
                    if (GeneralManager.Singleton.MousePos.Y < 100 && Camera.Y > 0)
                        Camera.Y -= (int)(100 - GeneralManager.Singleton.MousePos.Y) / 10;
                    if (GeneralManager.Singleton.MousePos.Y > Renderer.Height - 100 && Camera.Y < Size - Renderer.Height)
                        Camera.Y += (int)(100 - (Renderer.Height - GeneralManager.Singleton.MousePos.Y)) / 10;

                    if (Camera.X < 0) Camera.X = 0;
                    if (Camera.Y < 0) Camera.Y = 0;

                    foreach (Ship S in Ships)
                    {
                        S.SetDrawPosition(new Vector2(Camera.X, Camera.Y));
                        if (S.Position.X < Size / 100 || S.Position.X > (Size / 100) * 99 || S.Position.Y < Size / 100 || S.Position.Y > (Size / 100) * 99)
                        {
                            S.Speed *= -0.9f;
                            S.Position += 2 * S.Speed;
                        }
                    }

                    if (GeneralManager.Singleton.CheckKey(Keys.Escape))
                    {
                        GeneralManager.Singleton.CurrentLevel.Hide();
                        ScreenManager.Singleton.InGameMenu.Visible = true;
                        this.Visible = false;
                    }

                    base.Update(gameTime);
                }
            }
        }

        public void DrawMinimap()
        {
            if (MinimapEnabled)
            {
                int SizeX = Renderer.Width * 2 / 10;
                int SizeY = Renderer.Height * 2 / 10;

                spriteBatch.Draw(Renderer.Singleton.MinimapBackground, new Rectangle((int)(Renderer.Width * 0.8), 0, SizeX, SizeY), Color.White);


                foreach (VertexComponent C in Components)
                {
                    spriteBatch.Draw(Renderer.Singleton.IndicatorRed, new Vector2(Renderer.Width * 0.8f, 0) + C.Position / Size * new Vector2(SizeX, SizeY), Color.White);
                }

                foreach (Ship S in Ships)
                {
                    spriteBatch.Draw(Renderer.Singleton.IndicatorGreen, new Vector2(Renderer.Width * 0.8f, 0) + S.Position / Size * new Vector2(SizeX, SizeY), Color.White);

                }

                spriteBatch.Draw(Renderer.Singleton.MinimapOverlay, new Rectangle((int)(Renderer.Width * 0.8), 0, SizeX, SizeY), Color.White);
            }
        }
    }
}
