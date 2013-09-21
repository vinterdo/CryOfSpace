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
    public class ProjectViewScreen : GameScreen
    {

        int Rewind = 0;
        List<Rectangle> Rects;
        public Component Selected;

        public ProjectViewScreen(Game game)
            : base(game)
        {
            Visible = false;
            Rects = new List<Rectangle>();

            for (int i = 0; i < 18; i++)
            {
                Rects.Add(new Rectangle((int)(Renderer.Width * 0.056) * i, (int)(Renderer.Height * 0.91), (int)(Renderer.Width * 0.05), (int)(Renderer.Height * 0.05)));
            }
        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                if (GeneralManager.Singleton.CheckLMB())
                {
                    if (new Rectangle(0, 0, (int)(Renderer.Width * 0.057), (int)(Renderer.Height * 0.14)).Contains((int)GeneralManager.Singleton.MousePos.X, (int)GeneralManager.Singleton.MousePos.Y))
                    {
                        this.Visible = false;
                        GeneralManager.Singleton.CurrentLevel.Show();
                        GeneralManager.SoundManager.PlaySound("beep");
                    }


                    foreach (Slot S in GeneralManager.Singleton.CurrentPlayer.Ship.Hull.Slots)
                    {
                        if (new Rectangle((int)S.Position.X - 15 + Renderer.Width / 2 - GeneralManager.Singleton.CurrentPlayer.Ship.Hull.SizeX / 2, (int)S.Position.Y - 15 + Renderer.Height / 2 - GeneralManager.Singleton.CurrentPlayer.Ship.Hull.SizeY / 2, 30, 30).Contains((int)GeneralManager.Singleton.MousePos.X, (int)GeneralManager.Singleton.MousePos.Y))
                        {
                            if (Selected != null)
                            {

                                GeneralManager.SoundManager.PlaySound("beep");

                                if (S.Component != null)
                                {
                                    GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(S.Component);
                                }

                                S.Component = Selected;
                                GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Remove(Selected);
                                Selected = null;

                            }
                            else
                            {
                                GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(S.Component);
                                S.Component = null;
                            }
                        }
                    }
                }

                if (Rects[0].Contains((int)GeneralManager.Singleton.MousePos.X, (int)GeneralManager.Singleton.MousePos.Y) && GeneralManager.Singleton.CheckLMB())
                {
                    if (Rewind > 0)
                    {
                        Rewind--;
                    }
                    GeneralManager.SoundManager.PlaySound("beep");
                }
                for(int i =1; i <17; i++)
                {
                    if (Rects[i].Contains((int)GeneralManager.Singleton.MousePos.X, (int)GeneralManager.Singleton.MousePos.Y) && GeneralManager.Singleton.CheckLMB())
                    {
                        Selected = GeneralManager.Singleton.CurrentPlayer.ComponentsInventory[i - 1 + Rewind];
                    }

                }
                if (Rects[17].Contains((int)GeneralManager.Singleton.MousePos.X, (int)GeneralManager.Singleton.MousePos.Y) && GeneralManager.Singleton.CheckLMB())
                {
                    if (GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Count - 16 - Rewind >0)
                    {
                        Rewind++;
                    }
                    GeneralManager.SoundManager.PlaySound("beep");
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                // Background
                Vector2 RandomPos = new Vector2((float)Math.Sin(((gameTime.TotalRealTime.Milliseconds + gameTime.TotalRealTime.Seconds * 1000) * Math.PI) / 15250) * 15, (float)Math.Cos(((gameTime.TotalRealTime.Milliseconds + gameTime.TotalRealTime.Seconds * 1000) * Math.PI) / 30500) * 15)
                    + new Vector2(GeneralManager.Singleton.MousePos.X / Renderer.Width * -30 + 15, GeneralManager.Singleton.MousePos.Y / Renderer.Height * -30 + 15);

                Renderer.Singleton.batch.Draw(Renderer.Singleton.ProjectViewBackground, new Rectangle((int)RandomPos.X - 30, (int)RandomPos.Y - 30, Renderer.Width, Renderer.Height), new Color(Color.White, 0.15f));
                //Ship
                GeneralManager.Singleton.CurrentPlayer.Ship.DrawProjectView(gameTime);
                //GUI
                Renderer.Singleton.batch.Draw(Renderer.Singleton.ProjectGUI, new Rectangle(0, 0, Renderer.Width, Renderer.Height), Color.White);
                //Items
                int ItemsCount = GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Count;
                if (ItemsCount > 16) { ItemsCount = 16; }

                for (int i = Rewind; i < ItemsCount + Rewind; i++)
                {
                    if (Selected != null && Selected.Equals(GeneralManager.Singleton.CurrentPlayer.ComponentsInventory[i]))
                    {
                        Renderer.Singleton.batch.Draw(Renderer.Singleton.SelectedBackground, new Rectangle((int)(Renderer.Width * 0.056) * (i + 1 - Rewind) - 3, (int)(Renderer.Height * 0.91) - 3, (int)(Renderer.Width * 0.05) + 6, (int)(Renderer.Height * 0.05) + 6) , Color.White);
                    }
                    Renderer.Singleton.batch.Draw(GeneralManager.Singleton.CurrentPlayer.ComponentsInventory[i].Tex, new Rectangle((int)(Renderer.Width * 0.056) * (i + 1 - Rewind), (int)(Renderer.Height * 0.91), (int)(Renderer.Width * 0.05), (int)(Renderer.Height * 0.05)), Color.White);// Coœ tu jest nie tak (wtf)
                }


                base.Draw(gameTime);
            }
        }
    }
}