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
    public class Vertex : DrawableGameComponent, ICloneable
    {
        public static bool MinimapEnabled = true;

        public Vector2 Position;
        public Texture2D Tex;
        SpriteBatch spriteBatch;
        public Rectangle Rect;
        public int Size = 5000;
        public Vector2 BackgroundScale = new Vector2(1.5f, 1.5f);
        public bool IsMenuOpened = false;
        public bool HasShip = false;

        public List<Vertex> Connections;



        public Vertex(Game game, Vector2 Pos, Texture2D Tex)
            : base(game)
        {
            this.Position = Pos;
            this.Tex = Tex;
            this.Rect = new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)Tex.Width, (int)Tex.Height);
            spriteBatch = Renderer.Singleton.batch;
            Connections = new List<Vertex>();




        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 ScreenPosition = (Position) * new Vector2((float)(Renderer.Width * 0.6) / 500, (float)(Renderer.Height * 0.6) / 500) - new Vector2(15, 15) + new Vector2((float)Renderer.Width * 0.2f, (float)Renderer.Height * 0.2f);
            spriteBatch.Draw(Tex, ScreenPosition, null, Color.White, 0.0f, Vector2.One, 1.0f, SpriteEffects.None, 0.5f);


            if (HasShip)
            {
                spriteBatch.Draw(Renderer.Singleton.PlayerIndicator, ScreenPosition + new Vector2(30, 30), Color.White);
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
            Vector2 ScreenPosition = (Position) * new Vector2((float)(Renderer.Width * 0.6) / 500, (float)(Renderer.Height * 0.6) / 500) - new Vector2(15, 15) + new Vector2((float)Renderer.Width * 0.2f, (float)Renderer.Height * 0.2f) + new Vector2(15, 15);

            if (IsMenuOpened)
            {
                if (ScreenPosition.Y < Renderer.Height / 2)
                {
                    // Box of menu
                    spriteBatch.Draw(Renderer.Singleton.VertexMenuBackground, new Rectangle((int)(ScreenPosition.X - Renderer.Width * 0.1), (int)ScreenPosition.Y, (int)(Renderer.Width * 0.1) + 15, (int)(Renderer.Height * 0.2)), null, Color.White, 0.0f, Vector2.One, SpriteEffects.None, 0.4f);
                    // View Button
                    spriteBatch.Draw(Renderer.Singleton.ViewVertexButton, new Rectangle((int)(ScreenPosition.X - Renderer.Width * 0.1), (int)(ScreenPosition.Y + Renderer.Height * 0.03f), (int)(Renderer.Width * 0.1) + 15, (int)(Renderer.Height * 0.03)), null, Color.White, 0.0f, Vector2.One, SpriteEffects.None, 0.3f);

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
                    spriteBatch.Draw(Renderer.Singleton.ViewVertexButton, new Rectangle((int)(ScreenPosition.X - Renderer.Width * 0.1), (int)(ScreenPosition.Y - (Renderer.Width * 0.1) - 15 + Renderer.Height * 0.03f), (int)(Renderer.Width * 0.1) + 15, (int)(Renderer.Height * 0.03)), null, Color.White, 0.0f, Vector2.One, SpriteEffects.None, 0.3f);
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
            
        }

        
    }
}
