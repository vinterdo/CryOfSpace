using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class Level
    {
        List<Vertex> Ver;
        List<Connection> Connections;

        public Level()
        {
            Ver = new List<Vertex>();
            Connections = new List<Connection>();
        }

        public void Render(SpriteBatch spriteBatch)
        {
            foreach (Vertex v in Ver)
            {
                v.Render(spriteBatch);
            }
        }

        public void AddVertex(Vertex v)
        {
            Ver.Add(v);
        }

        public void CreateVertex(Vector2 Pos1, Texture2D Tex)
        {
            Vertex Tmp = new Vertex();
            Tmp.Position = Pos1;
            Tmp.Tex = Tex;
            AddVertex(Tmp);
        }

        public void Vertex(Vector2 Position, Texture2D Tex)
        {
            Vertex v = new Vertex();
            v.Tex = Tex;
            v.Position = Position;
            AddVertex(v);
        }

        public void AddConnection(int A, int B)
        {
            Connection Tmp = new Connection();
            Tmp.A = A;
            Tmp.B = B;
            Connections.Add(Tmp);
        }

        public void DrawConnection(int id, SpriteBatch spriteBatch)
        {
            Renderer.Singleton.Line(1.0f, Ver[Connections[id].A].Position, Ver[Connections[id].B].Position, Color.Cyan);
        }
    }

    public class Connection
    {
        public int A;
        public int B;
    }
}
