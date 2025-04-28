using System;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;

namespace Lab_7;

public class Blue_5
{
    public class Sportsman
    {
        private string _name;
        private string _surname;
        private int _place = 0;
        
        public string Name => _name;
        public string Surname => _surname;
        public int Place => _place;

        public Sportsman (string name, string surname)
        {
            _name = name;
            _surname = surname;
        }

        public void SetPlace(int place)
        {
            if (place <= 0 || _place != 0) return;
            _place = place;
        }

        public void Print()
        {
            Console.WriteLine($"{Name} {Surname} - {Place}");
        }
    }

    public abstract class Team
    {
        private string _name;
        private Sportsman[] _sportsmen;
        private int _index = 0;
        public string Name => _name;

        public Sportsman[] Sportsmen => _sportsmen;
        // {
        //     get
        //     {
        //         if(_sportsmen == null) return new Team[0];
        //         Team[] copy = new Team[_sportsmen.Length];
        //         Array.Copy(_sportsmen, copy, _sportsmen.Length);
        //         return copy;
        //     }   
        // }

        public int SummaryScore
        {
            get
            {
                if (_sportsmen == null) return 0;
                int[] scores = new int[]{0,5,4,3,2,1};
                int sum = 0;
                foreach (var x in _sportsmen)
                    if (x != null && 1 <= x.Place && x.Place <= 5)
                    {
                        sum+=scores[x.Place];
                    }
                return sum;
            }
        }

        public int TopPlace
        {
            get
            {
                if (_sportsmen == null || _sportsmen.Length == 0) return 18;
                int minPlace = 18;
                foreach (var x in _sportsmen)
                {
                    if (x != null && x.Place != 0)
                    {
                        minPlace = Math.Min(minPlace, x.Place);
                    }
                }
                return minPlace;
            }
        }                                                                                                                                                                                                         

        public Team(string name)
        {
            _name = name;
            _sportsmen = new Sportsman[6];
        }

        public void Add(Sportsman sportsman)
        {
            if (_sportsmen == null ) return;
            if (_index < _sportsmen.Length)
            {
                _sportsmen[_index] = sportsman;
                _index++;
            }
        }

        public void Add(Sportsman[] sportsmen)
        {
            if (sportsmen == null || _sportsmen == null || sportsmen.Length == 0) return;
            int addCount = Math.Min(_sportsmen.Length - _index, sportsmen.Length);
    
            for (int i = 0; i < addCount; i++)
            {
                if (sportsmen[i] != null)
                {
                    _sportsmen[_index] = sportsmen[i];
                    _index++;
                }
            }
        }

        public static void Sort(Team[] teams)
        {
            if (teams == null || teams.Length == 0) return;
            for (int i = 0; i < teams.Length; i++)
            {
                for (int j = 0; j < teams.Length - i - 1; j++)
                {
                    if (teams[j].SummaryScore < teams[j + 1].SummaryScore)
                        (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                    else if (teams[j].SummaryScore == teams[j + 1].SummaryScore && teams[j].TopPlace > teams[j + 1].TopPlace)
                            (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                }
            }
        }

        public void Print()
        {
            Console.WriteLine($"Team name: {Name}. Team Summary Score: {SummaryScore}. Team Top Place: {TopPlace}");
            Console.WriteLine("Sportsmen:");
            foreach (var x in _sportsmen)
            {
                if (x!= null)
                    Console.WriteLine($"{x.Name} {x.Surname}. Место - {x.Place}");
            }
        }

        protected abstract double GetTeamStrength();

        public static Team GetChampion(Team[] teams)
        {
            if (teams == null || teams.Length == 0) return null;

            double maxStrength = teams[0] != null ? teams[0].GetTeamStrength() : 0;
            int maxIndex = 0;

            for (int i = 1; i < teams.Length; i++)
            {
                if (teams[i] != null && teams[i].GetTeamStrength() > maxStrength)
                {
                    maxStrength = teams[i].GetTeamStrength();
                    maxIndex = i;
                }
            }

            return teams[maxIndex];
        }
    }

    public class ManTeam : Team
    {
        public ManTeam(string name) : base(name) { }
        
        protected override double GetTeamStrength()
        {
            int totalPlaces = 0;
            int count = 0;
            
            foreach (var sportsman in Sportsmen)
            {
                if (sportsman != null)
                {
                    totalPlaces += sportsman.Place;
                    count++;
                }
            }
            
            if (count == 0 || totalPlaces == 0) return 0;
            return 100.0 / totalPlaces / count;
        }
    }

    public class WomanTeam : Team
    {
        public WomanTeam(string name) : base(name) { }
        
        protected override double GetTeamStrength()
        {
            int totalPlaces = 0;
            int count = 0;
            int productPlaces = 1;
            
            foreach (var sportsman in Sportsmen)
            {
                if (sportsman != null)
                {
                    totalPlaces += sportsman.Place;
                    productPlaces *= sportsman.Place;
                    count++;
                }
            }
            
            if (count == 0 || productPlaces == 0) return 0;
            return 100.0 * totalPlaces * count / productPlaces;
        }
    }
}
