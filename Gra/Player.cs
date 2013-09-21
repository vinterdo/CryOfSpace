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
    public class Player
    {
        public List<Component> ComponentsInventory;

        public List<RawMaterial> MaterialsInventory;

        public Ship Ship;
        public int Money = 100;
        public AI PlayerController;

        public Player()
        {
            Ship = new Ship(Renderer.Singleton.Game);
            ComponentsInventory = new List<Component>();
            MaterialsInventory = new List<RawMaterial>();
            PlayerController = new PlayerController(Renderer.Singleton.Game);
            PlayerController.Initialize(Ship);
        }

        public void Initalize()
        {
            this.Ship.Hull = new Hull_Cerberus_B24();
            Ship.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            //Ship.Update(gameTime);
            PlayerController.Update(gameTime);
        }
    }
}
