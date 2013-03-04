using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
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
    class SaveGameData
    {
        Level LevelToSave;

        public void LoadLevel(Level Level)
        {
            LevelToSave = Level;
        }

        public void Save(StorageDevice device)
        {
            StorageContainer container =
                device.OpenContainer("StorageDemo");

            // Get the path of the save game.
            string filename = Path.Combine(container.Path, "savegame.sav");

            // Open the file, creating it if necessary.
            FileStream stream = File.Open(filename, FileMode.Create);


            // Convert the object to XML data and put it in the stream.
            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
            serializer.Serialize(stream, this);

            // Close the file.
            stream.Close();

            // Dispose the container, to commit changes.
            container.Dispose();
        }
    }
}
