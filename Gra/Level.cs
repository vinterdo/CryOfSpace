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
    public class Level
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

        public Vertex CreateVertex(Vector2 Pos1, Texture2D Tex)
        {
            Vertex Tmp = new Vertex();
            Tmp.Position = Pos1;
            Tmp.Tex = Tex;

            var ItemToAdd = Tmp.Clone();
            
            //AddVertex(ItemToAdd as Vertex);
            return ItemToAdd as Vertex;
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
            Renderer.Singleton.Line(1.0f, Ver[Connections[id].A].Position, Ver[Connections[id].B].Position, Color.Gray);
        }

        public void RenderConnections(SpriteBatch spriteBatch)
        {
            int i = 0;
            foreach (Connection Con in Connections)
            {
                DrawConnection(i, spriteBatch);
                i++;
            }
        }

        public void Generate()
        {
            int VertexNumber = 50 - GeneralManager.Singleton.GetRandom() % 20;
            List<int> AbleToConnect = new List<int>();

            for (int i = 0; i < VertexNumber; i++)
            {
                Vertex v = CreateVertex(new Vector2(GeneralManager.Singleton.GetRandom() % 500+ 10.0f, GeneralManager.Singleton.GetRandom() % 500+10.0f), Renderer.Singleton.Content.Load<Texture2D>("indicator"));

                bool IsGood = true;

                AbleToConnect.Clear();
                for(int j =0 ; j < i; j++)
                {
                    float Lenght = v.GetLenghtFrom(Ver[j]);
                    if (Lenght < 60)
                    {
                        IsGood = false;
                    }

                    if (Lenght < 120)
                    {
                        if (AbleToConnect.Count < 4)
                        {
                            if(!(VertexNumber-1 == i && j == i - 1))AbleToConnect.Add(j);
                        }
                    }
                }

                if (AbleToConnect.Count == 0 && i != 0)
                {
                    IsGood = false;
                }


                if (IsGood) AddVertex(v);
                else
                {
                    i--;
                    continue;
                }

                foreach (int Able in AbleToConnect)
                {
                    AddConnection(i, Able);
                }
            }
            
        }
    }

    public class Connection
    {
        public int A;
        public int B;


    }
}
