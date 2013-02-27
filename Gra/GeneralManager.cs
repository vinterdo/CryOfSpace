﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gra
{
    public sealed class GeneralManager
    {
        static GeneralManager Instance = new GeneralManager();
        Random Random;
        public Level CurrentLevel;

        private GeneralManager()
        {
            Random = new Random();
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

        public int GetRandom()
        {
            return Random.Next();
        }
    }
}
