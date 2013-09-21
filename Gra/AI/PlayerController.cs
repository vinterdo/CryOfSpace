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
    public class PlayerController: AI
    {


        public PlayerController(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            if (GeneralManager.Singleton.keyboardState.IsKeyDown(Keys.W))
            {
                Ship.Accelerate();
            }
            if (GeneralManager.Singleton.keyboardState.IsKeyDown(Keys.D))
            {
                Ship.TurnLeft();
            }
            if (GeneralManager.Singleton.keyboardState.IsKeyDown(Keys.A))
            {
                Ship.TurnRight();
            }
            if (GeneralManager.Singleton.keyboardState.IsKeyDown(Keys.S))
            {
                Ship.Break();
            }
            if (GeneralManager.Singleton.keyboardState.IsKeyDown(Keys.Q))
            {
                Ship.FlyTo(new Vector2(1000, 3000));
            }

            if (GeneralManager.Singleton.IsLMBDown)
            {
                foreach (Slot S in Ship.Hull.Slots)
                {
                    if (S.Component is Weapon && (S.Component as Weapon).WeaponMode == Weapon.Mode.Cursor)
                    {
                        Ship.Shoot(S.Component as Weapon, GeneralManager.Singleton.MousePos - Ship.DrawPosition);
                    }
                }
            }

            base.Update(gameTime);
        }
    }
}