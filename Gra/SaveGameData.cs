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

    class SaveGameManager
    {

        static public void Save()
        {
            IAsyncResult result = Guide.BeginShowStorageDeviceSelector(PlayerIndex.One, null, null);
            StorageDevice device = Guide.EndShowStorageDeviceSelector(result);

            SaveGameData data = new SaveGameData();
            data.Load();
            StorageContainer container = device.OpenContainer("StorageDemo");
            string filename = Path.Combine(container.Path, "savegame.sav");
            FileStream stream = File.Open(filename, FileMode.Create);

            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
            serializer.Serialize(stream, data);

            stream.Close();

            container.Dispose();
        }

        static public void Load(Game game, SpriteBatch spriteBatch)
        {
            IAsyncResult result = Guide.BeginShowStorageDeviceSelector(PlayerIndex.One, null, null);
            StorageDevice device = Guide.EndShowStorageDeviceSelector(result);

            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.OpenContainer("StorageDemo");
            result.AsyncWaitHandle.Close();

            string filename = Path.Combine(container.Path, "savegame.sav");

            if (!File.Exists(filename))
            {
                // If not, dispose of the container and return.
                container.Dispose();
                return;
            }

            // Open the file.
            FileStream stream = File.Open(filename, FileMode.Open);

            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
            SaveGameData data = (SaveGameData)serializer.Deserialize(stream);

            GeneralManager.Singleton.CurrentLevel = new Level(game, spriteBatch);

            using (Connection Tmp = new Connection(game))
            {
                int i = 0;
                foreach (RawConnection v in data.Connections)
                {
                    Tmp.A = v.A;
                    Tmp.B = v.B;
                    Tmp.Position1 = v.Pos1;
                    Tmp.Position2 = v.Pos2;
                    GeneralManager.Singleton.CurrentLevel.Components.Add(Tmp.Clone() as Connection);
                    i++;
                }
                GeneralManager.Singleton.CurrentLevel.ConnectionsCount = i;
            }

            using (Vertex Tmp = new Vertex(game, Vector2.Zero, Renderer.Singleton.Content.Load<Texture2D>("indicator")))
            {
                int i = 0;
                foreach (RawVertex v in data.Vertex)
                {
                    Tmp.Position = v.Position;
                    GeneralManager.Singleton.CurrentLevel.Components.Add(Tmp.Clone() as Vertex);
                    i++;
                }
                GeneralManager.Singleton.CurrentLevel.VertexCount = i;
            }

            GeneralManager.Singleton.IsLevelInitalized = true;

            // Close the file.
            stream.Close();
            container.Dispose();
        }
    }

    [Serializable]
    public class SaveGameData
    {
        private Level LevelToSave;

        public RawConnection[] Connections;
        public RawVertex[] Vertex;

        public void Load()
        {
            LevelToSave = GeneralManager.Singleton.CurrentLevel;

            Connections = new RawConnection[LevelToSave.ConnectionsCount];
            Vertex = new RawVertex[LevelToSave.VertexCount];
            
            
            int iV = 0;
            int iC = 0;
            foreach (GameComponent c in LevelToSave.Components)
            {
                if (c is Connection)
                {
                    Connections[iC] = GetRawFromConnection(c as Connection);
                    iC++;
                }
                else if (c is Vertex)
                {
                    Vertex[iV] = GetRawFromVertex(c as Vertex);
                    iV++;
                }

            }
            
        }
        
        public RawConnection GetRawFromConnection(Connection Connection)
        {
            RawConnection Raw = new RawConnection();
            Raw.A = Connection.A;
            Raw.B = Connection.B;
            Raw.Pos1 = Connection.Position1;
            Raw.Pos2 = Connection.Position2;
            return Raw;
        }

        public RawVertex GetRawFromVertex(Vertex Vertex)
        {
            RawVertex Raw = new RawVertex();
            Raw.Position = Vertex.Position;

            return Raw;
        }
    }

    [Serializable]
    public class RawConnection
    {
        public int A;
        public int B;
        public Vector2 Pos1;
        public Vector2 Pos2;
    }
    [Serializable]
    public class RawVertex
    {
        public Vector2 Position;
    }
}
