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
    }

    [Serializable]
    public class SaveGameData
    {
        private Level LevelToSave;

        public RawConnection[] Connections;
        public RawVertex[] Vertex;
        

        public string a = "b";
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
