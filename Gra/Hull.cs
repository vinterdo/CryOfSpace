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


namespace Gra
{
    
    public class Hull
    {


        public static Dictionary<string, Hull> Hulls = new Dictionary<string,Hull>();

        public string Name;

        public bool[][] ConduitsMask;
        public bool[][] ComponentsMask;
        public bool[][] AtmosphereMask;

        public float BasicHull;
        public float HullModifier;
        public float SpeedModifier;
        public float Weight;

        public RawAnimation OutsideView;
        public RawAnimation InsideView;
        public RawAnimation ConduitsView;
        public RawAnimation Explosion;
        public RawAnimation Wreck;
        public int SizeX;
        public int SizeY;
        public Vector2 Center;

        public List<Vector2> HullBreachs;
        

        public Hull()
        {
            
        }

        public void Initialize()
        {
            Hulls.Add(this.Name, this);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Load(string filename)
        {
            
            if (!File.Exists(filename))
            {
                // If not, dispose of the container and return.
                return;
            }

            // Open the file.
            FileStream stream = File.Open(filename, FileMode.Open);

            XmlSerializer serializer = new XmlSerializer(typeof(Hull));
            Hull data = (Hull)serializer.Deserialize(stream);
            this.ConduitsMask = data.ConduitsMask;
            this.ComponentsMask = data.ComponentsMask;
            this.Center = data.Center;
            this.BasicHull = data.BasicHull;
            this.AtmosphereMask = data.AtmosphereMask;
            this.Explosion = data.Explosion;
            this.HullBreachs = data.HullBreachs;
            this.HullModifier = data.HullModifier;
            this.InsideView = data.InsideView;
            this.OutsideView = data.OutsideView;
            this.SizeX = data.SizeX;
            this.SizeY = data.SizeY;
            this.SpeedModifier = data.SpeedModifier;
            this.Weight = data.Weight;
            this.Wreck = data.Wreck;
            this.ConduitsView = data.ConduitsView;


            // Close the file.
            stream.Close();
        }
    }
}