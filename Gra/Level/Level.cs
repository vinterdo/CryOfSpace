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
 
namespace CryOfSpace
{
    [Serializable]
    public class Level:GameScreen
    {

        public int ConnectionsCount = 0;
        public int VertexCount = 0;
        

        public Level(Game game):base(game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                Renderer.Singleton.RenderBackground(gameTime);
                RenderConnections(gameTime);
                RenderVertexes(gameTime);
                Renderer.Singleton.batch.Draw(Renderer.Singleton.ProjectButton, new Rectangle((int)(Renderer.Width * 0.4), (int)(Renderer.Height * 0.88), (int)(Renderer.Width * 0.2), (int)(Renderer.Height * 0.07)), Color.White);
                Renderer.Singleton.batch.Draw(Renderer.Singleton.InventoryButton, Renderer.GetPartialRect(0.85f,0.28f,0.15f,0.07f), Color.White);
            
            }
            //base.Draw(gameTime);
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
                        GeneralManager.Singleton.ClickCatched = true;
                        GeneralManager.SoundManager.PlaySound("beep");
                    }

                    if (Renderer.GetPartialRect(0.85f, 0.28f, 0.15f, 0.07f).Contains(new Point((int)GeneralManager.Singleton.MousePos.X, (int)GeneralManager.Singleton.MousePos.Y)))
                    {
                        Hide();
                        ScreenManager.Singleton.InventoryScreen.Visible = true;
                        GeneralManager.Singleton.ClickCatched = true;
                        GeneralManager.SoundManager.PlaySound("beep");
                    }


                    Point MousePoint = new Point(Math.Abs(Mouse.GetState().X), Mouse.GetState().Y);
                    Rectangle MouseRect = new Rectangle((int)MousePoint.X - 15, (int)MousePoint.Y - 15, 30, 30);

                    bool IsLMBFound = false;
                    
                    foreach (DrawableGameComponent C in Components)
                    {
                        if (C is VertexScreen)
                        {
                            VertexScreen V = C as VertexScreen;
                            if (V.Vertex.IsMenuOpened)
                            {
                                Vector2 ScreenPosition = (V.Vertex.Position) * new Vector2((float)(Renderer.Width * 0.6) / 500, (float)(Renderer.Height * 0.6) / 500) - new Vector2(15, 15) + new Vector2((float)Renderer.Width * 0.2f, (float)Renderer.Height * 0.2f) + new Vector2(15, 15);

                                if (ScreenPosition.Y < Renderer.Height / 2)
                                {
                                    // View Button
                                    if ((new Rectangle((int)(ScreenPosition.X - Renderer.Width * 0.1), (int)(ScreenPosition.Y + Renderer.Height * 0.03f), (int)(Renderer.Width * 0.1) + 15, (int)(Renderer.Height * 0.03))).Contains(MousePoint))
                                    {
                                        IsLMBFound = true;
                                        V.Vertex.IsMenuOpened = false;
                                        
                                        GeneralManager.SoundManager.PlaySound("beep");
                                        GeneralManager.Singleton.ClickCatched = true;
                                        GeneralManager.Singleton.CurrentVertex = C as VertexScreen;
                                        GeneralManager.Singleton.GameState = 2;
                                    }
                                    // FlyTo Button
                                    if (!(V.Equals(GeneralManager.Singleton.CurrentPlayer.Ship.CurrentVertex)) && GeneralManager.Singleton.CurrentPlayer.Ship.State == Ship.ShipState.InVertex && (new Rectangle((int)(ScreenPosition.X - Renderer.Width * 0.1), (int)(ScreenPosition.Y + Renderer.Height * 0.06f), (int)(Renderer.Width * 0.1) + 15, (int)(Renderer.Height * 0.03))).Contains(MousePoint))
                                    {
                                        if (V.Vertex.CheckFlightAbility())
                                        {
                                            IsLMBFound = true;
                                            GeneralManager.Singleton.ClickCatched = true;
                                            GeneralManager.Singleton.CurrentPlayer.Ship.FlyTo(V);
                                            V.Vertex.IsMenuOpened = false;
                                            GeneralManager.SoundManager.PlaySound("beep");
                                        }
                                    }
                                }
                                else
                                {
                                    // View Button
                                    if ((new Rectangle((int)(ScreenPosition.X - Renderer.Width * 0.1), (int)(ScreenPosition.Y - (Renderer.Width * 0.1) - 15 + Renderer.Height * 0.03f), (int)(Renderer.Width * 0.1) + 15, (int)(Renderer.Height * 0.03))).Contains(MousePoint))
                                    {
                                        IsLMBFound = true;
                                        V.Vertex.IsMenuOpened = false;
                                        GeneralManager.Singleton.ClickCatched = true;
                                        GeneralManager.SoundManager.PlaySound("beep");
                                        GeneralManager.Singleton.CurrentVertex = C as VertexScreen;
                                        GeneralManager.Singleton.GameState = 2;
                                    }

                                    // FlyTo Button
                                    if (!(V.Equals(GeneralManager.Singleton.CurrentPlayer.Ship.CurrentVertex)) && GeneralManager.Singleton.CurrentPlayer.Ship.State == Ship.ShipState.InVertex && (new Rectangle((int)(ScreenPosition.X - Renderer.Width * 0.1), (int)(ScreenPosition.Y - (Renderer.Width * 0.1) - 15 + Renderer.Height * 0.06f), (int)(Renderer.Width * 0.1) + 15, (int)(Renderer.Height * 0.03))).Contains(MousePoint))
                                    {
                                        if (V.Vertex.CheckFlightAbility())
                                        {
                                            IsLMBFound = true;
                                            GeneralManager.Singleton.ClickCatched = true;
                                            GeneralManager.Singleton.CurrentPlayer.Ship.FlyTo(V);
                                            V.Vertex.IsMenuOpened = false;
                                            GeneralManager.SoundManager.PlaySound("beep");
                                        }
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
                                    GeneralManager.Singleton.ClickCatched = true;
                                    GeneralManager.SoundManager.PlaySound("beep");

                                    break;
                                }
                            }
                        }
                    }
                }
                if (GeneralManager.Singleton.CheckKey(Keys.Escape))
                {
                    //this.Hide();
                    //ScreenManager.Singleton.InGameMenu.Visible = true;
                    ScreenState = State.FadeOut;
                    Fade = 0;
                    Target = ScreenManager.Singleton.InGameMenu;
                }


                base.Update(gameTime);
                if (FadeOutFinished)
                    Visible = false;


                /*foreach (DrawableGameComponent C in Components)
                {
                    C.Update(gameTime);
                }*/
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
                    (v as Vertex).Draw(gameTime);
            }

            foreach (DrawableGameComponent v in Components)
            {
                if (v is Vertex)
                    (v as Vertex).DrawMenu(gameTime);
            }
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
