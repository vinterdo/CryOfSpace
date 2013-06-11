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

        float Angle = 0.0f;
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
                    OnClick();
                }
            }
            Angle += gameTime.ElapsedGameTime.Milliseconds / 10000.0f;
            base.Update(gameTime);

        }

        public override void Draw(GameTime gameTime)
        {
            Renderer.Singleton.batch.Draw(Tex, DrawPosition, null, Color.White, Angle, new Vector2(Tex.Width/2, Tex.Height/2), Vector2.One, SpriteEffects.None, 1.0f);
            base.Draw(gameTime);

            Menu.Draw(gameTime);
            if (Menu.Visible)
            {
                Menu.DrawTrade(TradeOptions);
            }
        }

        public void OnClick()
        {
            Menu.Visible = !Menu.Visible;
        }
    }
}
