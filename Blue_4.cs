using System;
using System.Linq;

namespace Lab_7;

public class Blue_4
{
    public abstract class Team
    {
        private string _name;
        private int[] _scores;

        public string Name => _name;

        public int[] Scores
        {
            get
            {
                if (_scores == null) return null;
                int[] copy = new int[_scores.Length];
                Array.Copy(_scores, copy, _scores.Length);
                return copy;
            }
        }

        public int TotalScore
        {
            get
            {
                if (_scores == null) return 0;
                return _scores.Sum();
            }
        }

        public Team(string name)
        {
            _name = name;
            _scores = new int[0];
        }

        public void PlayMatch(int result)
        {
            int[] newScores = new int[_scores.Length + 1];
            Array.Copy(_scores, newScores, _scores.Length);
            newScores[_scores.Length] = result;
            _scores = newScores;
        }

        public void Print()
        {
            Console.WriteLine($"Name: {_name}, TotalScore: {TotalScore}");
            Console.WriteLine("Scores:");
            foreach(var score in _scores) Console.WriteLine(score);
        }
    }

    public class ManTeam : Team
    {
        public ManTeam(string name) : base(name)
        {
        }
    }

    public class WomanTeam : Team
    {
        public WomanTeam(string name) : base(name)
        {
        }
    }
    public class Group
    {
        private string _name;
        private Team[] _manTeams;
        private Team[] _womanTeams;
        private int _manTeamIndex = 0;
        private int _womanTeamIndex = 0;
        
        public string Name => _name;
        public Team[] ManTeams => _manTeams;
        public Team[] WomanTeams => _womanTeams;
        public Group(string name)
        {
            _name = name;
            _manTeams = new Team[12];
            _womanTeams = new Team[12];
        }

        public void Add(Team team)
        {
            if (team == null) return;
            if (team is ManTeam manTeam && _manTeamIndex < _manTeams.Length) 
                _manTeams[_manTeamIndex++] = manTeam;
            else if (team is WomanTeam womanTeam && _womanTeamIndex < _womanTeams.Length) 
                _womanTeams[_womanTeamIndex++] = womanTeam;
        }

        public void Add(Team[] teams)
        {
            if (teams == null || teams.Length == 0) return;
            foreach (var team in teams) Add(team);
        }

        public void Sort()
        {
            SortTeams(_manTeams);
            SortTeams(_womanTeams);
        }
        private void SortTeams(Team[] teams)
        {
            if (teams == null) return;
            for (int i = 0; i < teams.Length - 1; i++)
            {
                for (int j = 0; j < teams.Length - i - 1; j++)
                {
                    if (teams[j] != null && teams[j + 1] != null && teams[j].TotalScore < teams[j + 1].TotalScore)
                    {
                        (teams[j], teams[j + 1]) = (teams[j + 1], teams[j]);
                    }
                }
            }
        }

        public static Group Merge(Group group1, Group group2, int size)
        {
            Group result = new Group("Финалисты");
            Team[] men = MergeTeams(group1._manTeams, group2._manTeams, size);
            Team[] women = MergeTeams(group1._womanTeams, group2._womanTeams, size);
            result.Add(men);
            result.Add(women);
            return result;

        } 
        private static Team[] MergeTeams(Team[] teams1, Team[] teams2, int size)
        {
            if (size < 0 || teams1 == null || teams2 == null) return null;
            Team[] result = new Team[size];
            int i = 0, j = 0, c = 0, halfsize = size/2;
            while (i < halfsize && j < halfsize)
            {
                if (teams1[i].TotalScore >= teams2[j].TotalScore)
                    result[c++] = teams1[i++];
                else
                    result[c++] = teams2[j++];
            }
            while (i < halfsize) result[c++] = teams1[i++];
            while (j < halfsize) result[c++] = teams2[j++];
            return result;
        }


        public void Print()
        {
            Console.WriteLine($"{Name}");
            Console.WriteLine("Команды:");
            foreach(var x in WomanTeams) Console.WriteLine($"Woman Team {x.Name}, {x.TotalScore}");
            foreach(var x in ManTeams) Console.WriteLine($" Man Team {x.Name}, {x.TotalScore}");

        }
    }
}
