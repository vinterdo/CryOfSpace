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
        public int ConnectionsCount = 0;
        public int VertexCount = 0;
        

        public Level(Game game, SpriteBatch spriteBatch):base(game, spriteBatch)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                Renderer.Singleton.RenderBackground(gameTime);
                RenderConnections(gameTime);
                RenderVertexes(gameTime);
                spriteBatch.Draw(Renderer.Singleton.ProjectButton, new Rectangle((int)(Renderer.Width * 0.4),(int)( Renderer.Height * 0.88), (int)(Renderer.Width*0.2), (int)(Renderer.Height*0.07)), Color.White);
            }
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (Visible && GeneralManager.Singleton.GameState == 1)
            {
                if (GeneralManager.Singleton.CheckLMB())
                {
                    if (new Rectangle((int)(Renderer.Width * 0.4), (int)(Renderer.Height * 0.88), (int)(Renderer.Width * 0.2), (int)(Renderer.Height * 0.07)).Contains(new Point((int)GeneralManager.Singleton.MousePos.X, (int)GeneralManager.Singleton.MousePos.Y)))
                    {
                        Hide();
                        ScreenManager.Singleton.ProjectView.Visible = true;
                        GeneralManager.SoundManager.PlaySound("beep");
                    }

                    Point MousePoint = new Point(Math.Abs(Mouse.GetState().X), Mouse.GetState().Y);
                    Rectangle MouseRect = new Rectangle((int)MousePoint.X - 15, (int)MousePoint.Y - 15, 30, 30);

                    bool IsLMBFound = false;

                    foreach (DrawableGameComponent C in Components)
                    {
                        if (C is Vertex)
                        {
                            Vertex V = C as Vertex;
                            if (V.IsMenuOpened)
                            {
                                Vector2 ScreenPosition = (V.Position) * new Vector2((float)(Renderer.Width * 0.6) / 500, (float)(Renderer.Height * 0.6) / 500) - new Vector2(15, 15) + new Vector2((float)Renderer.Width * 0.2f, (float)Renderer.Height * 0.2f) + new Vector2(15, 15);

                                if (ScreenPosition.Y < Renderer.Height / 2)
                                {
                                    // View Button
                                    if ((new Rectangle((int)(ScreenPosition.X - Renderer.Width * 0.1), (int)(ScreenPosition.Y + Renderer.Height * 0.03f), (int)(Renderer.Width * 0.1) + 15, (int)(Renderer.Height * 0.03))).Contains(MousePoint))
                                    {
                                        IsLMBFound = true;
                                        V.IsMenuOpened = false;
                                        GeneralManager.SoundManager.PlaySound("beep");
                                        GeneralManager.Singleton.CurrentVertex = C as Vertex;
                                        GeneralManager.Singleton.GameState = 2;
                                    }
                                    // FlyTo Button
                                    if (!(V.Equals(GeneralManager.Singleton.CurrentPlayer.Ship.CurrentVertex)) && GeneralManager.Singleton.CurrentPlayer.Ship.State == Ship.ShipState.InVertex && (new Rectangle((int)(ScreenPosition.X - Renderer.Width * 0.1), (int)(ScreenPosition.Y + Renderer.Height * 0.06f), (int)(Renderer.Width * 0.1) + 15, (int)(Renderer.Height * 0.03))).Contains(MousePoint))
                                    {
                                        IsLMBFound = true;
                                        GeneralManager.Singleton.CurrentPlayer.Ship.FlyTo(V);
                                        V.IsMenuOpened = false;
                                        GeneralManager.SoundManager.PlaySound("beep");
                                    }
                                }
                                else
                                {
                                    // View Button
                                    if ((new Rectangle((int)(ScreenPosition.X - Renderer.Width * 0.1), (int)(ScreenPosition.Y - (Renderer.Width * 0.1) - 15 + Renderer.Height * 0.03f), (int)(Renderer.Width * 0.1) + 15, (int)(Renderer.Height * 0.03))).Contains(MousePoint))
                                    {
                                        IsLMBFound = true;
                                        V.IsMenuOpened = false;
                                        GeneralManager.SoundManager.PlaySound("beep");
                                        GeneralManager.Singleton.CurrentVertex = C as Vertex;
                                        GeneralManager.Singleton.GameState = 2;
                                    }

                                    // FlyTo Button
                                    if (!(V.Equals(GeneralManager.Singleton.CurrentPlayer.Ship.CurrentVertex)) && GeneralManager.Singleton.CurrentPlayer.Ship.State == Ship.ShipState.InVertex && (new Rectangle((int)(ScreenPosition.X - Renderer.Width * 0.1), (int)(ScreenPosition.Y - (Renderer.Width * 0.1) - 15 + Renderer.Height * 0.06f), (int)(Renderer.Width * 0.1) + 15, (int)(Renderer.Height * 0.03))).Contains(MousePoint))
                                    {
                                        IsLMBFound = true;
                                        GeneralManager.Singleton.CurrentPlayer.Ship.FlyTo(V);
                                        V.IsMenuOpened = false;
                                        GeneralManager.SoundManager.PlaySound("beep");
                                    }
                                }
                            }
                        }
                    }

                    if (!IsLMBFound)
                    {
                        foreach (DrawableGameComponent C in Components)
                        {
                            if (C is Vertex)
                            {
                                Vertex V = C as Vertex;
                                Vector2 IndicatorPosition = V.Position * new Vector2((float)(Renderer.Width * 0.6) / 500, (float)(Renderer.Height * 0.6) / 500) + new Vector2((float)Renderer.Width * 0.2f, (float)Renderer.Height * 0.2f);
                                if (MouseRect.Contains(new Point((int)IndicatorPosition.X, (int)IndicatorPosition.Y)))
                                {
                                    V.IsMenuOpened = !V.IsMenuOpened;
                                    GeneralManager.SoundManager.PlaySound("beep");

                                    break;
                                }
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
            
            foreach (DrawableGameComponent C in Components)
                {
                    C.Update(gameTime);
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

            foreach (DrawableGameComponent v in Components)
            {
                if (v is Vertex)
                    (v as Vertex).DrawMenu(gameTime);
            }
        }

        public Vertex CreateVertex(Game game, Vector2 Pos1, Texture2D Tex)
        {
            Vertex Tmp = new Vertex(game, Pos1, Tex);
            Tmp.Background = Renderer.Singleton.Background;
            Tmp.BackgroundScale = new Vector2(1.3f, 1.4f);

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
                // Vertex generation

                Vertex v = CreateVertex(this.game, new Vector2(GeneralManager.Singleton.GetRandom() % 500+ 10.0f, GeneralManager.Singleton.GetRandom() % 500+10.0f), Renderer.Singleton.Content.Load<Texture2D>("indicator"));

                if (GeneralManager.Singleton.GetRandom() % 3 == 0)
                {
                    SpaceStationComponent Station = new SpaceStationComponent(game);
                    Station.Initialize();
                    Station.Position = new Vector2(GeneralManager.Singleton.GetRandom() % 3000 + 1000, GeneralManager.Singleton.GetRandom() % 3000 + 1000);

                    if (GeneralManager.Singleton.GetRandom() % 3 == 0) Station.TradeOptions.AddBuyOption(new BuyOption(new Engine(Game), 50));
                    if (GeneralManager.Singleton.GetRandom() % 3 == 0) Station.TradeOptions.AddBuyOption(new BuyOption(new Generator(Game), 150));
                    if (GeneralManager.Singleton.GetRandom() % 3 == 0) Station.TradeOptions.AddSellOption(new SellOption(new Engine(Game), 60));
                    if (GeneralManager.Singleton.GetRandom() % 3 == 0) Station.TradeOptions.AddSellOption(new SellOption(new Generator(Game), 180));
                    
                    v.Components.Add(Station);
                }

                //========================

                if (i == 0)
                {
                    GeneralManager.Players["test"].Ship.Position = new Vector2(2000, 2000);
                    GeneralManager.Players["test"].Ship.State = Ship.ShipState.InVertex;
                    GeneralManager.Singleton.CurrentPlayer = GeneralManager.Players["test"];
                    v.Ships.Add(GeneralManager.Players["test"].Ship);
                    GeneralManager.Singleton.CurrentPlayer.Ship.CurrentVertex = v;
                }
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
                    v.Connections.Add(Ver[Able]);
                    Ver[Able].Connections.Add(v);
                }
            }
            foreach (Vertex v in Ver)
            {
                Components.Add(v);
            }
            // Do usuniecia
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Engine(game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Generator(game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Engine(game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Generator(game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Engine(game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Generator(game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Engine(game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Generator(game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Engine(game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Generator(game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Engine(game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Generator(game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Engine(game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Generator(game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Engine(game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Generator(game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Engine(game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Generator(game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Engine(game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Generator(game));

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
            Renderer.Singleton.Line(0.5f, Position1 * new Vector2((float)(Renderer.Width * 0.6) / 500, (float)(Renderer.Height * 0.6) / 500) + new Vector2((float)Renderer.Width * 0.2f, (float)Renderer.Height * 0.2f), Position2 * new Vector2((float)(Renderer.Width * 0.6) / 500, (float)(Renderer.Height * 0.6) / 500) + new Vector2((float)Renderer.Width * 0.2f, (float)Renderer.Height * 0.2f), new Color(Color.White, 0.5f));
            base.Draw(gameTime);
        }
    }
}
