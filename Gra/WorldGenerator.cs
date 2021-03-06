﻿using System;
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

namespace CryOfSpace
{
    class WorldGenerator
    {
        static Level GeneratingLevel;


        public static Level GenerateLevel(Game Game)
        {
            GeneratingLevel = new Level(Renderer.Singleton.Game);

            int VertexNumber = 50 - GeneralManager.Singleton.GetRandom() % 20;
            List<int> AbleToConnect = new List<int>();

            List<VertexScreen> Ver = new List<VertexScreen>();

            for (int i = 0; i < VertexNumber; i++)
            {
                // Vertex generation

                VertexScreen v = CreateVertex(Renderer.Singleton.Game, new Vector2(GeneralManager.Singleton.GetRandom() % 500 + 10.0f, GeneralManager.Singleton.GetRandom() % 500 + 10.0f), Renderer.Singleton.Content.Load<Texture2D>("indicator"));

                //========================

                if (i == 0)
                {
                    GeneralManager.Players["test"].Ship.Position = new Vector2(2000, 2000);
                    GeneralManager.Players["test"].Ship.State = Ship.ShipState.InVertex;
                    GeneralManager.Singleton.CurrentPlayer = GeneralManager.Players["test"];
                    v.Ships.Add(GeneralManager.Players["test"].Ship);
                    GeneralManager.Singleton.CurrentPlayer.Ship.CurrentVertex = v;
                }
                bool IsGood = true;

                AbleToConnect.Clear();
                for (int j = 0; j < i; j++)
                {
                    float Lenght = v.Vertex.GetLenghtFrom(Ver[j].Vertex);
                    if (Lenght < 60)
                    {
                        IsGood = false;
                    }

                    if (Lenght < 120)
                    {
                        if (AbleToConnect.Count < 4)
                        {
                            AbleToConnect.Add(j);
                        }
                    }
                }

                if (AbleToConnect.Count == 0 && i != 0)
                {
                    IsGood = false;
                }


                if (IsGood)
                {
                    GeneratingLevel.VertexCount++;
                    Ver.Add(v);
                    GeneratingLevel.Components.Add(v.Vertex);
                }
                else
                {
                    i--;
                    continue;
                }

                foreach (int Able in AbleToConnect)
                {
                    AddConnection(i, Able, v.Vertex.Position, Ver[Able].Vertex.Position);
                    v.Vertex.Connections.Add(Ver[Able].Vertex);
                    Ver[Able].Vertex.Connections.Add(v.Vertex);
                }
            }


            foreach (VertexScreen v in Ver)
            {
                GeneratingLevel.Components.Add(v);
            }

            CreateUserInventory(Game);
            GenerateNPC(Game);


            //GeneralManager.Singleton.IsLevelInitalized = true;
            //GeneralManager.Singleton.CurrentLevel = GeneratingLevel;
            return GeneratingLevel;
        }


        public static VertexScreen CreateVertex(Game game, Vector2 Pos1, Texture2D Tex)
        {
            VertexScreen Tmp = new VertexScreen(game, Pos1, Tex);
            //Tmp.Background = Renderer.Singleton.Background;
            //Tmp.BackgroundScale = new Vector2(1.3f, 1.4f);

            Background Background1 = new Background();
            Background1.Tex = Renderer.Singleton.Background;
            Background1.Scale= new Vector2(1.3f, 1.4f);
            Background1.Color = Color.Gray;
            Tmp.Backgrounds.Add(Background1);

            Background Background2 = new Background();
            Background2.Tex = Renderer.Textures["Background_Clouds_1"];
            Background2.Scale = new Vector2(2.5f, 2.5f);
            Background2.Color = Color.White;
            Tmp.Backgrounds.Add(Background2);

            Background Background3 = new Background();
            Background3.Tex = Renderer.Textures["Background_Clouds_2"];
            Background3.Scale = new Vector2(3.5f, 3.5f);
            Background3.Color = Color.White;
            Tmp.Backgrounds.Add(Background3);



            foreach(VertexComponent C in GenerateAsteroidField(game, Vector2.One * Tmp.Size))
            {
                Tmp.Components.Add(C);
            }

            if (GeneralManager.Singleton.GetRandom() % 3 == 0)
            {
                SpaceStationComponent Station = new SpaceStationComponent(game);
                Station.Initialize();
                Station.Position = new Vector2(GeneralManager.Singleton.GetRandom() % 3000 + 1000, GeneralManager.Singleton.GetRandom() % 3000 + 1000);



                if (GeneralManager.Singleton.GetRandom() % 2 == 0) Station.TradeOptions.AddBuyOption(new BuyOption(new Engine(game), 50));
                if (GeneralManager.Singleton.GetRandom() % 2 == 0) Station.TradeOptions.AddBuyOption(new BuyOption(new Generator(game), 150));
                if (GeneralManager.Singleton.GetRandom() % 2 == 0) Station.TradeOptions.AddSellOption(new SellOption(new Engine(game), 60));
                if (GeneralManager.Singleton.GetRandom() % 2 == 0) Station.TradeOptions.AddSellOption(new SellOption(new Generator(game), 180));

                Tmp.Components.Add(Station);
            }


            var ItemToAdd = Tmp.Clone();

            return ItemToAdd as VertexScreen;
        }




        public static void AddConnection(int A, int B, Vector2 Position1, Vector2 Position2)
        {
            Connection Tmp = new Connection(Renderer.Singleton.Game);
            Tmp.A = A;
            Tmp.B = B;
            Tmp.Position1 = Position1;
            Tmp.Position2 = Position2;
            GeneratingLevel.ConnectionsCount++;
            GeneratingLevel.Components.Add(Tmp);
        }

        public static void CreateUserInventory(Game Game)
        {
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Weapon_GaussCannonB50(Game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Weapon_GaussCannonB50(Game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new MiningLaser(Game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Generator(Game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Engine(Game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Generator(Game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Cargo(Game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Cargo(Game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Engine(Game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Generator(Game));
            GeneralManager.Singleton.CurrentPlayer.ComponentsInventory.Add(new Generator(Game));
            
            //GeneralManager.Singleton.CurrentPlayer.MaterialsInventory.Add(new Plutonium(16));
        }

        public static List<Asteroid> GenerateAsteroidField(Game Game, Vector2 Size)
        {
            List<Asteroid> Asteroids = new List<Asteroid>();

            int NoAsteroids = GeneralManager.Singleton.GetRandom() % 10;
            Vector2 FieldPosition = new Vector2(GeneralManager.Singleton.GetRandom() % (Size.X - 2000) + 1000, GeneralManager.Singleton.GetRandom() % (Size.Y - 2000) + 1000);

            for (int i = 0; i < NoAsteroids; i++)
            {
                Vector2 Position = new Vector2(GeneralManager.Singleton.GetRandom() % 1000, GeneralManager.Singleton.GetRandom() % 1000) + FieldPosition;
                Asteroid Tmp = new Asteroid1(Game, Position);
                Tmp.Angle = (GeneralManager.Singleton.GetRandom() % (int)(Math.PI * 1000)) / 1000f;
                Tmp.AngularSpeed = (GeneralManager.Singleton.GetRandom() % 1000) / 200000f - (GeneralManager.Singleton.GetRandom() % 1000) / 200000f;
                foreach (Type M in RawMaterial.Types)
                {
                    RawMaterial Material = (RawMaterial)Activator.CreateInstance(RawMaterial.Types[0]);
                    if (Material.GenerationChance > (float)(GeneralManager.Singleton.GetRandom() & 1000) / 1000f)
                    {
                        int NoMaterials = GeneralManager.Singleton.GetRandom()% (Material.MaximalCount - Material.MinimalCount) + Material.MinimalCount;
                        for (int j = 0; j < NoMaterials; j++)
                        {
                            Tmp.Materials.Add((RawMaterial)Activator.CreateInstance(M));
                        }
                    }
                    //Tmp.Materials.Add();
                }
                
                Asteroids.Add(Tmp);
            }

            return Asteroids;
        }

        public static void GenerateNPC(Game Game)
        {
            Ship TmpShip = new Ship(Game);
            TmpShip.Hull = new Hull_Cerberus_B24();
            TmpShip.Position = Vector2.One * 1000;
            TmpShip.State = Ship.ShipState.InVertex;
            TmpShip.ShipView = true;
            TmpShip.ShipColor = Color.Red;
            TmpShip.Hull.Slots[0].Component = new Weapon_GaussCannonB50(Game);
            //TmpShip.Hull.Slots[1].Component = new Weapon_GaussCannonB50(Game);

            TmpShip.CurrentVertex = GeneratingLevel.Components[0] as VertexScreen;
            TmpShip.Initialize();

            //(GeneratingLevel.Components[0] as Vertex).Parent.Ships.Add(TmpShip);
            NPC TmpNPC = new NPC_Pirate1(Game, TmpShip, (GeneratingLevel.Components[0] as Vertex).Parent);
        }
    }
}
