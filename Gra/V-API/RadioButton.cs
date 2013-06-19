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
    public class RadioButton : Microsoft.Xna.Framework.DrawableGameComponent
    {

        public List<CheckBox> Boxes;
        public int Choosen;


        public RadioButton(Game game)
            : base(game)
        {
            Boxes = new List<CheckBox>();
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach(CheckBox C in Boxes)
            {
                if (GeneralManager.Singleton.CheckCollision(GeneralManager.Singleton.MousePos, C.Rect) && GeneralManager.Singleton.CheckLMB())
                {
                    foreach (CheckBox CB in Boxes)
                    {
                        CB.State = false;
                    }

                    C.State = true;
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (CheckBox C in Boxes)
            {
                C.Draw(gameTime);
            }

            base.Draw(gameTime);
        }
    }
}