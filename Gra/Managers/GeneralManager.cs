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
    public sealed class GeneralManager
    {
        public static SoundManager SoundManager;

        static GeneralManager Instance = new GeneralManager();
        Random Random;
        public Level CurrentLevel;
        public bool IsLevelInitalized;
        public KeyboardState keyboardState;
        KeyboardState oldKeyboardState;
        public Vector2 MousePos;
        MouseState OldMouseState;
        MouseState NewMouseState;
        public Player CurrentPlayer;
        public bool IsLMBDown;
        public bool ClickCatched = false;

        public static Dictionary<string, Player> Players = new Dictionary<string, Player>();
        public static List<NPC> NPCs = new List<NPC>();

        public static List<VertexScreen> Vertexes = new List<VertexScreen>();
        public static List<GuiElement> Gui = new List<GuiElement>();

        public VertexScreen CurrentVertex;

        public int GameState = 1; // 1 - level, 2 - vertex


        private GeneralManager()
        {
            SoundManager = new SoundManager(Renderer.Singleton.Game);
            SoundManager.Initialize();
            IsLevelInitalized = false;
            Random = new Random();

            //    MATERIAL TYPES

            RawMaterial.Types.Add(new Plutonium(0).GetType());
            RawMaterial.Types.Add(new Tungsten(0).GetType());
            RawMaterial.Types.Add(new Hydrogen(0).GetType());
        }

        public bool CheckLMB()
        {
            return OldMouseState.LeftButton == ButtonState.Released && NewMouseState.LeftButton == ButtonState.Pressed && ! ClickCatched;
        }

        static public GeneralManager Singleton
        {
            get
            {
                return Instance;
            }
            set
            {
            }
        }

        public void Update(GameTime gameTime)
        {
            ClickCatched = false;

            foreach(KeyValuePair<string, Player> P in Players)
            {
                P.Value.Update(gameTime);
            }

            if(CurrentVertex != null)
            {
                CurrentVertex.Update(gameTime);
            }

            if(IsLevelInitalized)
            {

                foreach (VertexScreen V in Vertexes)
                {
                    V.Update(gameTime);
                }

            }

            if(IsLevelInitalized) CurrentLevel.Update(gameTime);
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            OldMouseState = NewMouseState;
            NewMouseState = Mouse.GetState();

            IsLMBDown = NewMouseState.LeftButton == ButtonState.Pressed;

            MousePos = new Vector2(NewMouseState.X, NewMouseState.Y);

            UpdateNPC(gameTime);
        }

        public int GetRandom()
        {
            return Random.Next();
        }

        public bool CheckKey(Keys Key)
        {
            return oldKeyboardState.IsKeyUp(Key) && keyboardState.IsKeyDown(Key);
        }

        public Vector2 GetVectorFromAngle(float Angle)
        {
            return new Vector2((float)Math.Sin(Angle), (float)Math.Cos(Angle));
        }

        public float GetAngleFromVector(Vector2 Vec)
        {
            return (float)Math.Atan2(Vec.X, Vec.Y);
        }

        public Vector2 RotateVector(float Angle, Vector2 Vec, Vector2 Center)
        {
            return new Vector2((float)(Math.Cos(Angle) * (Vec.X - Center.X) - Math.Sin(Angle) * (Vec.Y - Center.Y)), (float)(Math.Sin(Angle) * (Vec.X - Center.X) + Math.Cos(Angle) * (Vec.Y - Center.Y))) + Center;
        }

        public bool CheckCollision(Vector2 Vec, Rectangle Rect)
        {
            return Vec.X > Rect.X && Vec.Y > Rect.Y && Vec.X < Rect.X + Rect.Width && Vec.Y < Rect.Y + Rect.Height;
        }

        public bool CheckCollision(Vector2 Vec, Rectangle Rect, float Angle, Vector2 Center)
        {
            return CheckCollision(RotateVector(Angle * -1, Vec, Center), Rect);
        }

        public static void DrawGUI(GameTime gameTime)
        {
            foreach (GuiElement G in Gui)
            {
                G.Draw(gameTime);
            }
        }

        public static void UpdateGUI(GameTime gameTime)
        {
            foreach (GuiElement G in Gui)
            {
                G.Draw(gameTime);
            }
        }

        public static void UpdateNPC(GameTime gameTime)
        {
            foreach(NPC N in NPCs)
            {
                N.Update(gameTime);
                //N.Ship.CreateInsideTex(gameTime);
            }
        }

        public Vector2 TexturesCollide(Color[,] tex1, Matrix mat1, Color[,] tex2, Matrix mat2)
        {
            Matrix mat1to2 = mat1 * Matrix.Invert(mat2);
            int width1 = tex1.GetLength(0);
            int height1 = tex1.GetLength(1);
            int width2 = tex2.GetLength(0);
            int height2 = tex2.GetLength(1);

            for (int x1 = 0; x1 < width1; x1++)
            {
                for (int y1 = 0; y1 < height1; y1++)
                {
                    Vector2 pos1 = new Vector2(x1, y1);
                    Vector2 pos2 = Vector2.Transform(pos1, mat1to2);

                    int x2 = (int)pos2.X;
                    int y2 = (int)pos2.Y;
                    if ((x2 >= 0) && (x2 < width2))
                    {
                        if ((y2 >= 0) && (y2 < height2))
                        {
                            if (tex1[x1, y1].A > 0)
                            {
                                if (tex2[x2, y2].A > 0)
                                {
                                    Vector2 screenPos = Vector2.Transform(pos1, mat1);
                                    return screenPos;
                                }
                            }
                        }
                    }
                }
            }

            return new Vector2(-1, -1);
        }

        public static Vector2 RotateVector(Vector2 Base, float Angle, Vector2 Center)
        {
            return Vector2.Transform(Base - Center, Matrix.CreateRotationZ(Angle)) + Center;
        }
    }
}
