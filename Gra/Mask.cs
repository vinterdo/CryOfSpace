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
    public class Mask
    {
        public Color [,] Data;

        public Mask(Texture2D Tex, Vector2 Size)
        {
            Data = new Color[(int)Size.X, (int)Size.Y];

            Color[] Colors = new Color[(int)(Size.X * Size.Y)];
            Tex.GetData(Colors);

            for (int x = 0; x < Size.X; x++)
            {
                for (int y = 0; y < Size.Y; y++)
                {
                    Data[x, y] = Colors[x + y * (int)Size.X];
                }
            }

        }

        public bool CheckCollision(Vector2 Vec)
        {
            if (Vec.X >= 0 && Vec.X < Data.GetLength(0) && Vec.Y >= 0 && Vec.Y < Data.GetLength(1))
            {
                return Data[(int)Vec.X, (int)Vec.Y] == Color.Black;
            }
            else
            {
                return false;
            }
        }

    }
}
