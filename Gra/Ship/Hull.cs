using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Text;


namespace CryOfSpace
{
    [Serializable]

    public class Hull
    {

        public int SlotsNum = 1;
        public Slot[] Slots;

        public static Dictionary<string, Hull> Hulls = new Dictionary<string,Hull>();

        public string Name;

        //public bool[][] AtmosphereMask;

        public float BasicHull;
        public float HullModifier;
        public float SpeedModifier;
        public float Weight;

        public RawAnimation OutsideView;
        public RawAnimation OutsideColor;

        public RawAnimation InsideView;
        public RawAnimation Explosion;
        public Wreck Wreck;
        public int SizeX;
        public int SizeY;
        public Vector2 Center;

        public List<Vector2> HullBreachs;

        public List<ParticleEmitter> AccelerationEngines;
        public List<Vector2> AccelerationOffset;
        public List<ParticleEmitter> LeftEngines;
        public List<Vector2> LeftOffset;
        public List<ParticleEmitter> RightEngines;
        public List<Vector2> RightOffset;

        public List<Vector2> WeaponPositions;

        public Mask Mask;

        public Hull()
        {
            AccelerationEngines = new List<ParticleEmitter>();
            LeftEngines = new List<ParticleEmitter>();
            RightEngines = new List<ParticleEmitter>();

            AccelerationOffset = new List<Vector2>();
            LeftOffset = new List<Vector2>();
            RightOffset = new List<Vector2>();

            WeaponPositions = new List<Vector2>();
        }

        public void Initialize()
        {
            Slots = new Slot[SlotsNum];
            for(int i =0; i < SlotsNum; i++)
            {
                Slots[i] = new Slot();
            }
        }

        public void Update(GameTime gameTime)
        {

        }

    }
}