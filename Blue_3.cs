using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;

namespace Lab_6;

public class Blue_3
{
    public class Participant
    {
        
        private string _name;
        private string _surname;
        protected int[] _penaltyTimes;
        
        public string Name => _name;
        public string Surname => _surname;

        public int[] Penalties
        {
            get
            {
                if (_penaltyTimes == null) return null;
                int[]copy = new int[_penaltyTimes.Length];
                Array.Copy( _penaltyTimes, copy, _penaltyTimes.Length);
                return copy;
            }
        }

        public int Total
        {
            get
            {
                if (_penaltyTimes == null) return 0;
                return _penaltyTimes.Sum();
            }
        }

        public virtual bool IsExpelled
        {
            get
            {
                if (_penaltyTimes == null) return false;
                foreach (var time in _penaltyTimes) if (time == 10) return true;
                return false;
            }
        }

        public Participant(string name, string surname)
        {
            _name = name;
            _surname = surname;
            _penaltyTimes = new int[0];
        }

        public virtual void PlayMatch(int time)
        {
            if (_penaltyTimes == null) return;
            int[] newPenalty = new int[_penaltyTimes.Length + 1];
            Array.Copy(_penaltyTimes, newPenalty, _penaltyTimes.Length);
            newPenalty[_penaltyTimes.Length] = time;
            _penaltyTimes = newPenalty;
        }

        public static void Sort(Participant[] array)
        {
            if (array == null) return;
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j].Total > array[j + 1].Total)
                        (array[j], array[j+1]) = (array[j+1], array[j]);
                }
            }
        }

        public void Print()
        {
            Console.WriteLine($"Name: {_name}, Surname: {_surname}, TotalTime: {Total}, IsExpelled: {IsExpelled}, PenaltyTime: ");
            foreach (var x in _penaltyTimes) Console.Write($"{x} ");
            Console.WriteLine();
        }
    }

    public class BasketballPlayer : Participant
    {
        public override bool IsExpelled
        {
            get
            {
                if (_penaltyTimes == null|| _penaltyTimes.Length == 0) return false;
                int fouls = 0;
                foreach (int p in _penaltyTimes)
                {
                    if (p>=5) fouls++;
                }
                if (fouls > 0.1 * _penaltyTimes.Length || Total >= 2 * _penaltyTimes.Length) 
                {
                    return true;
                }
                return false;
            }

        }
        public BasketballPlayer(string name, string surname) : base(name, surname) {}
        
    }

    public class HockeyPlayer : Participant
    {
        public static int _playersCount = 0;
        public static int _allPenalties = 0;

        public override bool IsExpelled
        {
            get
            {
                if (_penaltyTimes == null || _penaltyTimes.Length == 0 || _playersCount == 0) return false;
                foreach (var penalty in _penaltyTimes)
                {
                    if (penalty >= 10) return true;
                    
                }

                if (Total > 0.1 * _allPenalties / _playersCount) return true;
                return false;
            }
        }

        public HockeyPlayer(string name, string surname) : base(name, surname)
        {
            _playersCount++;
        }

        public override void PlayMatch(int time)
        {
            if (_penaltyTimes == null) return;
            base.PlayMatch(time);
            _allPenalties += time;
        }
    }
}