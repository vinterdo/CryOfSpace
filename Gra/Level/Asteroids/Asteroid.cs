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
    public abstract class Asteroid : VertexComponent
    {
        public Asteroid(Game game)
            : base(game)
        {
            Materials = new List<RawMaterial>();
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public int MiningChance; // Max 10 000
        public List<RawMaterial> Materials;

        public override void Update(GameTime gameTime)
        {
            if (GeneralManager.Singleton.CheckLMB())
            {
                if (GeneralManager.Singleton.CheckCollision(GeneralManager.Singleton.MousePos, GetRect()))
                {
                    OnClick();
                }

                base.Update(gameTime);
            }
        }

        public void OnClick()
        {
            foreach (Slot S in GeneralManager.Singleton.CurrentPlayer.Ship.Hull.Slots)
            {
                if (S.Component is MiningLaser)
                {
                    if (GeneralManager.Singleton.CurrentVertex != null && GeneralManager.Singleton.CurrentVertex.Components.Contains(this))
                    {
                        (S.Component as MiningLaser).StartMining(this);
                    }
                }
            }
        }

        public RawMaterial GetMaterial()
        {
            int Random = GeneralManager.Singleton.GetRandom() % Materials.Count;
            RawMaterial ReturnMaterial = Materials[Random];
            Materials.Remove(ReturnMaterial);
            return ReturnMaterial;
        }
    }
}