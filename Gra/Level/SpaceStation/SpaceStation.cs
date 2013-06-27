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
    class SpaceStationComponent:VertexComponent
    {
        public SpaceStationMenu Menu;
        public TradeOptions TradeOptions;

        public SpaceStationComponent(Game game)
            : base(game)
        {
            Menu = new SpaceStationMenu(game);
            TradeOptions = new TradeOptions();
        }

        public override void Initialize()
        {
            Tex = Renderer.Singleton.Content.Load<Texture2D>("station");
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (GeneralManager.Singleton.CheckLMB())
            {
                if (GeneralManager.Singleton.CheckCollision(GeneralManager.Singleton.MousePos, GetRect()))
                {
                    //Vertex.MinimapEnabled = !Vertex.MinimapEnabled;
                    OnClick();
                }

                if (Menu.Visible)
                {

                    switch (Menu.MenuMode)
                    {
                        case SpaceStationMenu.Mode.Main:
                            if (Renderer.GetPartialRect(0.6f, 0.3f, 0.15f, 0.05f).Contains((int)GeneralManager.Singleton.MousePos.X, (int)GeneralManager.Singleton.MousePos.Y))
                            {
                                Menu.MenuMode = SpaceStationMenu.Mode.TradeComponents;
                            }

                            if (Renderer.GetPartialRect(0.6f, 0.35f, 0.15f, 0.05f).Contains((int)GeneralManager.Singleton.MousePos.X, (int)GeneralManager.Singleton.MousePos.Y))
                            {
                                Menu.MenuMode = SpaceStationMenu.Mode.TradeMaterials;
                            }

                            break;
                        case SpaceStationMenu.Mode.TradeComponents:

                            if (Renderer.GetPartialRect(0.6f, 0.3f, 0.15f, 0.05f).Contains((int)GeneralManager.Singleton.MousePos.X, (int)GeneralManager.Singleton.MousePos.Y))
                            {
                                Menu.MenuMode = SpaceStationMenu.Mode.Main;
                            }

                            break;
                        case SpaceStationMenu.Mode.TradeMaterials:

                            if (Renderer.GetPartialRect(0.6f, 0.3f, 0.15f, 0.05f).Contains((int)GeneralManager.Singleton.MousePos.X, (int)GeneralManager.Singleton.MousePos.Y))
                            {
                                Menu.MenuMode = SpaceStationMenu.Mode.Main;
                            }

                            break;
                    }



                }
            }
            Angle += gameTime.ElapsedGameTime.Milliseconds / 10000.0f;

            if(GeneralManager.Singleton.CurrentVertex != null)
            {
                if (GeneralManager.Singleton.CurrentVertex.Components.Contains(this))
                {
                    Menu.Update(gameTime, TradeOptions);
                }
            }

            base.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            Renderer.Singleton.batch.Draw(Tex, DrawPosition, null, Color.White, Angle, new Vector2(Tex.Width/2, Tex.Height/2), Vector2.One, SpriteEffects.None, 1.0f);
            base.Draw(gameTime);

            
            if (Menu.Visible)
            {
                Menu.Draw(gameTime);

                switch (Menu.MenuMode)
                {
                    case SpaceStationMenu.Mode.Main:
                        Renderer.Singleton.batch.Draw(Renderer.Textures["TradeComponents"], Renderer.GetPartialRect(0.6f, 0.3f, 0.15f, 0.05f), Color.White);
                        Renderer.Singleton.batch.Draw(Renderer.Textures["TradeMaterials"], Renderer.GetPartialRect(0.6f, 0.35f, 0.15f, 0.05f), Color.White);

                        break;
                    case SpaceStationMenu.Mode.TradeComponents:
                        Menu.DrawComponentsTrade(TradeOptions);
                        Renderer.Singleton.batch.Draw(Renderer.Textures["BackButton"], Renderer.GetPartialRect(0.6f, 0.3f, 0.15f, 0.05f), Color.White);

                        break;
                    case SpaceStationMenu.Mode.TradeMaterials:
                        Renderer.Singleton.batch.Draw(Renderer.Textures["BackButton"], Renderer.GetPartialRect(0.6f, 0.3f, 0.15f, 0.05f), Color.White);

                        Menu.DrawMaterialsTrade();

                        break;
                }
                

                
            }
        }

        public void OnClick()
        {
            Menu.Visible = !Menu.Visible; 
        }
    }
}
