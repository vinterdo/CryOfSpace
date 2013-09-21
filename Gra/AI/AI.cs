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
    public abstract class AI : Microsoft.Xna.Framework.GameComponent
    {
        public Ship Ship;

        public AI(Game game)
            : base(game)
        {
        }

        public void Initialize(Ship Ship)
        {
            this.Ship = Ship;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
    }
}