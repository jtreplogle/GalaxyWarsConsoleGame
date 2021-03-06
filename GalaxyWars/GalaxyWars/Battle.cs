﻿using GalaxyWars.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyWars
{
    public class Battle
    {
        //declaration
        private Dalek _dalek;
        private Sleestak _sleestak;
        private Predador _predador;
        private bool _scienceBeatsReligion = true;
        private bool _religionBeatsWarfare = true;
        private bool _warfareBeatsScience = true;
        private const int _anomoloyOccurrence = 25;
        private const int _battlePopulationLossBase = 20000;
        private Random _rnd = new Random();
        int _year = 1;

        //specification of species name every 25 years
        public Species ReligiousSpecies
        {
            get
            {
                if (_dalek is IReligious)
                    return _dalek;
                else if (_sleestak is IReligious)
                    return _sleestak;
                else
                    return _predador;
            }
        }

        public Species SpacefaringSpecies
        {
            get
            {
                if (_dalek is ISpacefaring)
                    return _dalek;
                else if (_sleestak is ISpacefaring)
                    return _sleestak;
                else
                    return _predador;
            }
        }

        public Species WarriorSpecies
        {
            get
            {
                if (_dalek is IWarrior)
                    return _dalek;
                else if (_sleestak is IWarrior)
                    return _sleestak;
                else
                    return _predador;
            }
        }
        //call in program.cs
        //notation for console for the battle is over
        public Species FirstDeadSpecies
        { get; set; }

        public Battle (Dalek dalek, Sleestak sleestak, Predador predador)
        {
            _dalek = dalek;
            _sleestak = sleestak;
            _predador = predador;
        }

        private void CheckYear() // what is happening every 25 years, with the proporties of the species
        {
            //int%int is dividing int with another int and the result is the remainder
            //so 25%25 is 0, 50%25 is 0, 75%25 is 0
            if (_year%_anomoloyOccurrence == 0)
            {
                _scienceBeatsReligion = !_scienceBeatsReligion;
                _religionBeatsWarfare = !_religionBeatsWarfare;
                _warfareBeatsScience = !_warfareBeatsScience;
            }
        }
        //Battle starts here
        public void FightBattle()
        {
            CheckYear();

            int firstBattle = _rnd.Next(1,3);

            if (firstBattle == 1)
            {
                ScienceVsReligion();
                ReligionVsWarfare();
                WarfareVsScience();
            }
            else if (firstBattle == 2)
            {
                ReligionVsWarfare();
                WarfareVsScience();
                ScienceVsReligion();
            }
            else
            {
                WarfareVsScience();
                ScienceVsReligion();
                ReligionVsWarfare();
            }
            
            SpacefaringSpecies.Population += 5000; //spacefearing bonus for each year
            WarriorSpecies.Population -= 2500; //warrior doc for each year

            _year++;
        }
        //checking for a looser, anyone reach 0 or less population
        private void CheckSpeciesDeath()
        {
            if (SpacefaringSpecies.IsSpeciesDead)
                FirstDeadSpecies = SpacefaringSpecies;
            else if (ReligiousSpecies.IsSpeciesDead)
                FirstDeadSpecies = ReligiousSpecies;
            else if (WarriorSpecies.IsSpeciesDead)
                FirstDeadSpecies = WarriorSpecies;
        }
        //define everything that could happend in each battle
        private void ScienceVsReligion()
        {
            SpacefaringSpecies.Population -= _battlePopulationLossBase;
            ReligiousSpecies.Population -= _battlePopulationLossBase;

            CheckSpeciesDeath();

            if (_scienceBeatsReligion)
            {
                ReligiousSpecies.Population -= (int)(ReligiousSpecies.Population * .02);
            }
            else
            {
                SpacefaringSpecies.Population -= (int)(SpacefaringSpecies.Population * .02);
            }

            CheckSpeciesDeath();


            int convertedPopulation = (int)(SpacefaringSpecies.Population * .01);
            ReligiousSpecies.Population += convertedPopulation;
            SpacefaringSpecies.Population -= convertedPopulation;

            CheckSpeciesDeath();
        }

        private void ReligionVsWarfare()
        {
            ReligiousSpecies.Population -= _battlePopulationLossBase;
            WarriorSpecies.Population -= _battlePopulationLossBase;

            CheckSpeciesDeath();

            if (_religionBeatsWarfare)
            {
                WarriorSpecies.Population -= (int)(WarriorSpecies.Population * .02);
                Console.WriteLine("This year could be your last! Fight to the death!");
            }
            else
            {
                ReligiousSpecies.Population -= (int)(ReligiousSpecies.Population * .02);
            }

            ReligiousSpecies.Population -= 10000;

            CheckSpeciesDeath();

            int convertedPopulation = (int)(ReligiousSpecies.Population * .01);
            ReligiousSpecies.Population += convertedPopulation;
            WarriorSpecies.Population -= convertedPopulation;

            CheckSpeciesDeath();
        }

        private void WarfareVsScience()
        {
            WarriorSpecies.Population -= _battlePopulationLossBase;
            SpacefaringSpecies.Population -= _battlePopulationLossBase;

            CheckSpeciesDeath();

            if (_warfareBeatsScience)
            {
                WarriorSpecies.Population -= (int)(WarriorSpecies.Population * .02);
            }
            else
            {
                SpacefaringSpecies.Population -= (int)(SpacefaringSpecies.Population * .02);
            }

            SpacefaringSpecies.Population -= 10000;

            CheckSpeciesDeath();
        }
    }
}
