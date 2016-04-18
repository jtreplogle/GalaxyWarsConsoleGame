﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyWars.Interfaces
{
    class Sleestak : Species, IReligious
    {
        private string[] _weapons = new string[2] { "Laser Gun", "Electric Drill" };

        public override string Color
        {
            get { return "Green"; }
        }

        public override int NumberOfArms
        {
            get { return 2; }
        }

        public override int Population
        {
            get; set;
        }

        public override string SpeciesDescription
        {
            get
            {
                return "";
            }
        }

        public override string[] Weapons
        {
            get { return _weapons; }
            set { _weapons = value; }
        }

        public string ReligionName
        {
            get { return "Sovereignty"; }
        }

        public Sleestak(int population) : base(population)
        {
        }
    }
}
