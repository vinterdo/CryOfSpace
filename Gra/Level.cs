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
    public class Level:GameScreen
    {
        SpriteBatch SpriteBatch;
        public int ConnectionsCount = 0;
        public int VertexCount = 0;
        

        public Level(Game game, SpriteBatch spriteBatch):base(game, spriteBatch)
        {
            SpriteBatch = spriteBatch;
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                Renderer.Singleton.RenderBackground(gameTime);
                RenderConnections(gameTime);
                RenderVertexes(gameTime);
            }
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                if (GeneralManager.Singleton.CheckLMB())
                {
                    Point MousePoint = new Point(Mouse.GetState().X, Mouse.GetState().Y);
                    foreach(DrawableGameComponent C in Components)
                    {
                        if (C is Vertex)
                        {
                            if ((C as Vertex).Rect.Contains(MousePoint))
                            {
                                Game.Exit();
                            }
                        }
                    }
                }
                if (GeneralManager.Singleton.CheckKey(Keys.Escape))
                {
                    this.Hide();
                    ScreenManager.Singleton.InGameMenu.Visible = true;
                }
                base.Update(gameTime);
            }
        }

        public override void Show()
        {
            base.Show();
        }

        public void RenderVertexes(GameTime gameTime)
        {
            foreach (DrawableGameComponent v in Components)
            {
                if(v is Vertex)
                    (v as Vertex).DrawOutside(gameTime);
            }
        }

        public Vertex CreateVertex(Game game, Vector2 Pos1, Texture2D Tex)
        {
            Vertex Tmp = new Vertex(game, Pos1, Tex);

            var ItemToAdd = Tmp.Clone();
            
            return ItemToAdd as Vertex;
        }


        public void AddConnection(int A, int B, Vector2 Position1, Vector2 Position2)
        {
            Connection Tmp = new Connection(base.Game);
            Tmp.A = A;
            Tmp.B = B;
            Tmp.Position1 = Position1;
            Tmp.Position2 = Position2;
            ConnectionsCount++;
            Components.Add(Tmp);
        }

        public void RenderConnections(GameTime gameTime)
        {
            int i = 0;
            foreach (DrawableGameComponent Con in Components)
            {
                if(Con is Connection)
                    (Con as Connection).Draw(gameTime);
                i++;
            }
        }

        public void Generate()
        {
            int VertexNumber = 50 - GeneralManager.Singleton.GetRandom() % 20;
            List<int> AbleToConnect = new List<int>();

            List<Vertex> Ver = new List<Vertex>();

            for (int i = 0; i < VertexNumber; i++)
            {
                Vertex v = CreateVertex(this.game, new Vector2(GeneralManager.Singleton.GetRandom() % 500+ 10.0f, GeneralManager.Singleton.GetRandom() % 500+10.0f), Renderer.Singleton.Content.Load<Texture2D>("indicator"));

                bool IsGood = true;

                AbleToConnect.Clear();
                for(int j =0 ; j < i; j++)
                {
                    float Lenght = v.GetLenghtFrom(Ver[j]);
                    if (Lenght < 60)
                    {
                        IsGood = false;
                    }

                    if (Lenght < 120)
                    {
                        if (AbleToConnect.Count < 4)
                        {
                            AbleToConnect.Add(j);
                        }
                    }
                }

                if (AbleToConnect.Count == 0 && i != 0)
                {
                    IsGood = false;
                }


                if (IsGood)
                {
                    VertexCount++;
                    Ver.Add(v);
                }
                else
                {
                    i--;
                    continue;
                }

                foreach (int Able in AbleToConnect)
                {
                    AddConnection(i, Able, v.Position, Ver[Able].Position);
                }
            }
            foreach (Vertex v in Ver)
            {
                Components.Add(v);
            }

            GeneralManager.Singleton.IsLevelInitalized = true;
        }

        
    }
    [Serializable]
    public class Connection : DrawableGameComponent, ICloneable
    {
        public int A;
        public int B;

        public Vector2 Position1;
        public Vector2 Position2;

        public Connection(Game game)
            : base(game)
        {
        }

        public object Clone()
        {
            Connection Tmp = new Connection(Game);
            Tmp.A = this.A;
            Tmp.B = this.B;
            Tmp.Position1 = this.Position1;
            Tmp.Position2 = this.Position2;
            return Tmp;

        }

        public override void Draw(GameTime gameTime)
        {
            Renderer.Singleton.Line(1.0f, Position1 * new Vector2((float)(Renderer.Width - 100) / 500, (float)(Renderer.Height - 100) / 500), Position2 * new Vector2((float)(Renderer.Width - 100) / 500, (float)(Renderer.Height - 100) / 500), Color.Gray);
            base.Draw(gameTime);
        }
    }
}
