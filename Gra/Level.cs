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

        void AddConnection(int A, int B)
        {
            Connection Tmp = new Connection();
            Tmp.A = A;
            Tmp.B = B;
            Connections.Add(Tmp);
        }

        public void DrawConnection(int id, SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.RenderState.PointSize = 8.0f;


            var vb = new VertexBuffer(
                spriteBatch.GraphicsDevice,
                VertexPositionColor.SizeInBytes * 2, 
                BufferUsage.None);

            spriteBatch.GraphicsDevice.Vertices[0].SetSource(vb, 0, VertexPositionColor.SizeInBytes);

            VertexPositionColor[] pointList = new VertexPositionColor[3];


            pointList[1] = new VertexPositionColor(new Vector3(1.0f, 1.0f, 0), Color.White);
            pointList[2] = new VertexPositionColor(new Vector3(100.0f, 100.0f, 0), Color.White);

            vb.SetData<VertexPositionColor>(pointList);

            spriteBatch.GraphicsDevice.DrawPrimitives
                (PrimitiveType.PointList,
                 0,
                 1);
                 
        }
    }

    public class Connection
    {
        public int A;
        public int B;
    }
}
