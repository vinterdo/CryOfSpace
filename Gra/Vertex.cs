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
        public Vector2 Position;
        public Texture2D Tex;
        SpriteBatch spriteBatch;
        public Rectangle Rect;
        public int Size = 5000;
        public Vector2 BackgroundScale = new Vector2(1.5f, 1.5f);
        public Texture2D Background;
        public Rectangle Camera = new Rectangle(0,0, Renderer.Width, Renderer.Height);

        public List<Player> Players;

        Vector2 ShipIndicatorPosition;
        

        public Vertex (Game game, Vector2 Pos, Texture2D Tex):base(game)
        {
            Players = new List<Player>();
            this.Position = Pos;
            this.Tex = Tex;
            this.Rect = new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)Tex.Width, (int)Tex.Height);
            spriteBatch = Renderer.Singleton.batch;
        }

        public void DrawOutside(GameTime gameTime)
        {
            spriteBatch.Draw(Tex, (Position - new Vector2(Tex.Width / 2, Tex.Height / 2)) * new Vector2((float)(Renderer.Width - 100) / 500, (float)(Renderer.Height - 100) / 500), Color.White);

            if (Players.Count > 0)
            {
                spriteBatch.Draw(Renderer.Singleton.PlayerIndicator, (Position ) * new Vector2((float)(Renderer.Width - 100) / 500, (float)(Renderer.Height - 100) / 500), Color.White);
            }

            base.Draw(gameTime);
        }

        public void DrawInside(GameTime gameTime)
        {
            spriteBatch.Draw(Renderer.Singleton.Background, new Rectangle(0,0, Camera.Width, Camera.Height),new Rectangle((int)(Camera.X/10),(int)(Camera.Y/10), (int)(Renderer.Singleton.Background.Width/BackgroundScale.X) ,(int)(Renderer.Singleton.Background.Height/BackgroundScale.Y)), Color.White);

            bool IsCurrentPlayerOnVertex = false;

            foreach (Player P in Players)
            {
                if(P.Equals(GeneralManager.Singleton.CurrentPlayer))
                {
                    IsCurrentPlayerOnVertex = true;
                }
                P.Ship.DrawOutside(gameTime);
            }

            if (IsCurrentPlayerOnVertex)
            {
                if (!Camera.Intersects(new Rectangle((int)GeneralManager.Singleton.CurrentPlayer.Ship.Position.X , (int)GeneralManager.Singleton.CurrentPlayer.Ship.Position.Y , (int)GeneralManager.Singleton.CurrentPlayer.Ship.OutsideView.FrameSize.X , (int)GeneralManager.Singleton.CurrentPlayer.Ship.OutsideView.FrameSize.Y)))
                {
                    ShipIndicatorPosition = GeneralManager.Singleton.CurrentPlayer.Ship.Position  - new Vector2(Camera.X, Camera.Y);

                    short Direction = 0; // 0 - top, 1 - right, 2 - bottom, 3- left

                    if (ShipIndicatorPosition.X < 100 )
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
                            spriteBatch.Draw(Renderer.Singleton.ShipIndicator, new Vector2((ShipIndicatorPosition.X - (Renderer.Singleton.ShipIndicator.Width / 2)),(ShipIndicatorPosition.Y -  Renderer.Singleton.ShipIndicator.Height / 2)) ,null, Color.White, (float)(0.5f * Math.PI) ,new Vector2 (50, 50),Vector2.One, SpriteEffects.None, 0.0f);
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
            if (GeneralManager.Singleton.MousePos.X < 100 && Camera.X > 0)
                Camera.X -= (int)(100 - GeneralManager.Singleton.MousePos.X) / 10;
            if (GeneralManager.Singleton.MousePos.X > Renderer.Width - 100 && Camera.X < Size - Renderer.Width)
                Camera.X += (int)(100 - (Renderer.Width - GeneralManager.Singleton.MousePos.X)) / 10;
            if (GeneralManager.Singleton.MousePos.Y < 100 && Camera.Y > 0)
                Camera.Y -= (int)(100 - GeneralManager.Singleton.MousePos.Y)/10;
            if (GeneralManager.Singleton.MousePos.Y > Renderer.Height - 100 && Camera.Y < Size - Renderer.Height)
                Camera.Y += (int)(100 - (Renderer.Height - GeneralManager.Singleton.MousePos.Y)) / 10;

            if (Camera.X < 0) Camera.X = 0;
            if (Camera.Y < 0) Camera.Y = 0;

            foreach (Player P in Players)
            {
                P.Ship.SetDrawPosition(new Vector2(Camera.X, Camera.Y));
            }

            base.Update(gameTime);
        }
    }
}
