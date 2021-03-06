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
    public struct Background
    {
        public Texture2D Tex;
        public Vector2 Scale;
        public Color Color;
    }

    public class VertexScreen : GameScreen, ICloneable
    {
        public static bool MinimapEnabled = true;
        public static bool WeaponsMenuEnabled = true;

        public Texture2D Tex;
        public Rectangle Rect;
        public int Size = 5000;
        public Rectangle Camera = new Rectangle(0, 0, Renderer.Width, Renderer.Height);

        public List<Ship> Ships;
        public Ship SelectedShip;

        public List<Background> Backgrounds;

        public new List<VertexComponent> Components;

        static List<RadioButton> WeaponMode = WeaponMode = new List<RadioButton>();
        static List<Text> WeaponLabel = new List<Text>();
        static List<ProgressBar> WeaponHeat = new List<ProgressBar>();
        


        Vector2 ShipIndicatorPosition;

        public Vertex Vertex;


        public VertexScreen(Game game, Vector2 Pos, Texture2D Tex)
            : base(game)
        {
            Ships = new List<Ship>();
            this.Tex = Tex;
            this.Rect = new Rectangle((int)(Pos.X), (int)(Pos.Y), (int)Tex.Width, (int)Tex.Height);

            

            Vertex = new Vertex(game, Pos, Tex, this);

            Components = new List<VertexComponent>();
            Backgrounds = new List<Background>();


            this.Effect = Renderer.Singleton.Content.Load<Effect>("Glow");

        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {

                foreach (Background B in Backgrounds)
                {
                    Renderer.Singleton.batch.Draw(B.Tex, new Rectangle(0, 0, Camera.Width, Camera.Height), new Rectangle((int)(Camera.X / 10), (int)(Camera.Y / 10), (int)(B.Tex.Width / B.Scale.X), (int)(B.Tex.Height / B.Scale.Y)), B.Color);
                }


                bool IsCurrentPlayerOnVertex = false;


                foreach (VertexComponent C in Components)
                {
                    C.Draw(gameTime);
                }


                foreach (Ship S in Ships)
                {
                    if (S.Equals(GeneralManager.Singleton.CurrentPlayer.Ship) && S.State == Ship.ShipState.InVertex)
                    {
                        IsCurrentPlayerOnVertex = true;
                    }
                    if (S.State == Ship.ShipState.InVertex)
                    {
                        if (S.Equals(SelectedShip))
                        {
                            Renderer.Animations["ShipSelector"].Position = S.DrawPosition - Renderer.Animations["ShipSelector"].FrameSize / 2;
                            Renderer.Animations["ShipSelector"].Draw(gameTime);
                        }
                        if (S.ShipView)
                        {
                            S.DrawOutside(gameTime);
                        }
                        else
                        {
                            S.DrawInside(gameTime);
                        }

                        if (S.Equals(SelectedShip))
                        {
                            Renderer.Singleton.batch.Draw(Renderer.Textures["ShipInfo"], new Rectangle((int)(S.DrawPosition.X), (int)(S.DrawPosition.Y - Renderer.Height * 0.1f), (int)(Renderer.Width * 0.1f), (int)(Renderer.Height * 0.1f)), Color.White);
                            Text Text = new Text(Game);
                            Text.Font = Renderer.Fonts["Visitor"];
                            Text.Name = S.HitPoints.ToString() + " / " + S.Hull.BasicHull.ToString();
                            Text.Rect = new Rectangle((int)(S.DrawPosition.X) + 10, (int)(S.DrawPosition.Y - Renderer.Height * 0.1f + 10), 100, 12);
                            Text.Draw(gameTime);
                        }
                    }

                    if (S.Hull.Mask.CheckCollision(GeneralManager.Singleton.MousePos - S.DrawPosition + S.OutsideView.FrameSize/2) && GeneralManager.Singleton.CheckLMB())
                    {
                        SelectedShip = S;
                    }
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
                                Renderer.Singleton.batch.Draw(Renderer.Singleton.ShipIndicator, ShipIndicatorPosition - new Vector2(Renderer.Singleton.ShipIndicator.Width / 2, Renderer.Singleton.ShipIndicator.Height / 2), Color.White);
                                break;
                            case 1:
                                Renderer.Singleton.batch.Draw(Renderer.Singleton.ShipIndicator, new Vector2((ShipIndicatorPosition.X - (Renderer.Singleton.ShipIndicator.Width / 2)), (ShipIndicatorPosition.Y - Renderer.Singleton.ShipIndicator.Height / 2)), null, Color.White, (float)(0.5f * Math.PI), new Vector2(50, 50), Vector2.One, SpriteEffects.None, 0.0f);
                                break;
                            case 2:
                                Renderer.Singleton.batch.Draw(Renderer.Singleton.ShipIndicator, new Vector2((ShipIndicatorPosition.X - (Renderer.Singleton.ShipIndicator.Width / 2)), (ShipIndicatorPosition.Y - Renderer.Singleton.ShipIndicator.Height / 2)), null, Color.White, (float)(1.0f * Math.PI), new Vector2(50, 50), Vector2.One, SpriteEffects.None, 0.0f);
                                break;
                            case 3:
                                Renderer.Singleton.batch.Draw(Renderer.Singleton.ShipIndicator, new Vector2((ShipIndicatorPosition.X - (Renderer.Singleton.ShipIndicator.Width / 2)), (ShipIndicatorPosition.Y - Renderer.Singleton.ShipIndicator.Height / 2)), null, Color.White, (float)(1.5f * Math.PI), new Vector2(50, 50), Vector2.One, SpriteEffects.None, 0.0f);
                                break;
                        }

                    }

                }

                Renderer.Singleton.batch.Draw(Renderer.Singleton.FromVertexToLevelGUI, Vector2.Zero, Color.White);
                DrawMinimap();
                DrawWeaponsMenu();
                Renderer.Singleton.DrawMoney();
            }
        }


        public object Clone()
        {
            VertexScreen Tmp = new VertexScreen(Game, Vertex.Position, this.Tex);
            Tmp.Components = this.Components;
            Tmp.Ships = this.Ships;
            Tmp.Size = this.Size;
            Tmp.Vertex = this.Vertex;
            Tmp.Backgrounds = this.Backgrounds;


            return Tmp as object;
        }


        public override void Update(GameTime gameTime)
        {
            Vertex.HasShip = Ships.Count > 0;

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
                        ScreenManager.Singleton.InGameMenu.Target = this;
                        this.Visible = false;
                    }

                    UpdateWeaponsMenu();

                    base.Update(gameTime);
                }


                for(int i =0; i < Ships.Count; i++)
                {
                    Ships[i].Update(gameTime);
                }

                Effect.Parameters["BloomIntensity"].SetValue((float)Math.Pow(Effect.Parameters["BloomIntensity"].GetValueSingle(), 0.85)* 1.0f);
            }
        }



        public void DrawMinimap()
        {
            if (MinimapEnabled)
            {
                int SizeX = Renderer.Width * 2 / 10;
                int SizeY = Renderer.Height * 2 / 10;

                Renderer.Singleton.batch.Draw(Renderer.Singleton.MinimapBackground, new Rectangle((int)(Renderer.Width * 0.8), 0, SizeX, SizeY), Color.White);


                foreach (VertexComponent C in Components)
                {
                    Renderer.Singleton.batch.Draw(Renderer.Singleton.IndicatorRed, new Vector2(Renderer.Width * 0.8f, 0) + C.Position / Size * new Vector2(SizeX, SizeY), Color.White);
                }

                foreach (Ship S in Ships)
                {
                    Renderer.Singleton.batch.Draw(Renderer.Singleton.IndicatorGreen, new Vector2(Renderer.Width * 0.8f, 0) + S.Position / Size * new Vector2(SizeX, SizeY), Color.White);

                }

                Renderer.Singleton.batch.Draw(Renderer.Singleton.MinimapOverlay, new Rectangle((int)(Renderer.Width * 0.8), 0, SizeX, SizeY), Color.White);
            }
        }

        public void DrawWeaponsMenu()
        {
            if (WeaponsMenuEnabled)
            {
                Renderer.Singleton.batch.Draw(Renderer.Singleton.WeaponsMenu, Renderer.GetPartialRect(0.8f, 0.2f, 0.2f, 0.6f), Color.White);

                foreach (RadioButton R in WeaponMode)
                {
                    R.Draw(null);
                }

                foreach (ProgressBar B in WeaponHeat)
                {
                    B.Draw(null);
                }

                foreach (Text T in WeaponLabel)
                {
                    T.Draw(null);
                }
            }
        }

        public void UpdateWeaponsMenu()
        {
            int Count = 0;
            foreach (Slot S in GeneralManager.Singleton.CurrentPlayer.Ship.Hull.Slots)
            {
                if (S.Component is Weapon)
                {
                    Count++;
                }
            }

            int i = 0;

            if (WeaponMode.Count == Count)
            {
                foreach (Slot S in GeneralManager.Singleton.CurrentPlayer.Ship.Hull.Slots)
                {
                    if (S.Component is Weapon)
                    {
                        Weapon W = S.Component as Weapon;
                        switch (W.WeaponMode)
                        {
                            case Weapon.Mode.Cursor:
                                WeaponMode[i].Choosen = 0;
                                break;
                            case Weapon.Mode.Auto:
                                WeaponMode[i].Choosen = 1;
                                break;
                            case Weapon.Mode.Off:
                                WeaponMode[i].Choosen = 2;
                                break;
                            case Weapon.Mode.Select:
                                WeaponMode[i].Choosen = 3;
                                break;
                        }
                        WeaponHeat[i].Progress = (float)W.Heat / (float)W.MaxHeat;
                        i++;
                    }
                }
            }

            else
            {
                WeaponMode = new List<RadioButton>();

                int x = 0;
                Weapon CurrentWeapon;

                for (int j = 0; j < Count; j++)
                {

                    while (!(GeneralManager.Singleton.CurrentPlayer.Ship.Hull.Slots[x].Component is Weapon))
                    {

                        x++;
                    }

                    CurrentWeapon = GeneralManager.Singleton.CurrentPlayer.Ship.Hull.Slots[x].Component as Weapon;


                    RadioButton Tmp = new RadioButton(Game);

                    Tmp.Boxes.Add(new CheckBox(Game, Renderer.GetPartialRect(0.91f, 0.25f + 0.02f * j, 0.015f, 0.015f), Renderer.Singleton.CheckBoxOn, Renderer.Singleton.CheckBoxOff));
                    Tmp.Boxes.Add(new CheckBox(Game, Renderer.GetPartialRect(0.93f, 0.25f + 0.02f * j, 0.015f, 0.015f), Renderer.Singleton.CheckBoxOn, Renderer.Singleton.CheckBoxOff));
                    Tmp.Boxes.Add(new CheckBox(Game, Renderer.GetPartialRect(0.95f, 0.25f + 0.02f * j, 0.015f, 0.015f), Renderer.Singleton.CheckBoxOn, Renderer.Singleton.CheckBoxOff));
                    Tmp.Boxes.Add(new CheckBox(Game, Renderer.GetPartialRect(0.97f, 0.25f + 0.02f * j, 0.015f, 0.015f), Renderer.Singleton.CheckBoxOn, Renderer.Singleton.CheckBoxOff));

                    WeaponMode.Add(Tmp);

                    Text Label = new Text(Game);
                    Label.Name = CurrentWeapon.Name;
                    Label.Rect = Renderer.GetPartialRect(0.81f, 0.25f + 0.02f * j, 0.09f, 0.02f);
                    Label.Font = Renderer.Singleton.Content.Load<SpriteFont>("Font");
                    WeaponLabel.Add(Label);

                    ProgressBar Progr = new ProgressBar(Game, Renderer.GetPartialRect(0.81f, 0.25f + 0.02f * j, 0.09f, 0.02f), Renderer.Singleton.ProgressBackground, Renderer.Singleton.HeatGradient, Renderer.Singleton.ProgressOverlay);

                    WeaponHeat.Add(Progr);
                }

            }

            foreach (RadioButton R in WeaponMode)
            {
                R.Update(null);
            }
        }

    }
}