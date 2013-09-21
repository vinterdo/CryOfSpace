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
    public class AgresiveAI : AI
    {
        public Ship AttackTarget;

        int BulletsShoot = 0;

        public AgresiveAI(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (AttackTarget != null)
            {
                Ship.FlyTo(AttackTarget.Position);
            }
            if (true)
            {
                AttackTarget = GeneralManager.Singleton.CurrentPlayer.Ship;
            }
            else
            {
                AttackTarget = null;
            }

            if ((AttackTarget.Position - Ship.Position).Length() < 1000)
            {
                foreach (Slot S in Ship.Hull.Slots)
                {
                    if (S.Component is Weapon)
                    {
                        Weapon Weapon = S.Component as Weapon;
                        if (Weapon is Weapon_GaussCannonB50)
                        {
                            if (BulletsShoot < 10)
                            {
                                Ship.Shoot(Weapon, AttackTarget.Position - Ship.Position);
                                BulletsShoot++;
                            }

                            if (Weapon.Heat < Weapon.MaxHeat / 2)
                            {
                                BulletsShoot = 0;
                            }
                        }
                    }
                }
            }

            base.Update(gameTime);
        }
    }
}