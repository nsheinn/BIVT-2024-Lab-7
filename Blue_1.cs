using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7;

public class Blue_1
{
    public class Response
    {
        private string _name;
        protected int _votes = 0;
        
        public string Name => _name;
        public int Votes => _votes;

        public Response(string name)
        {
            _name = name;
        }

        public virtual int CountVotes(Response[] responses)
        {
            if (responses == null || responses.Length == 0) return 0;
            int count = 0;
            foreach (var response in responses)
            {
                if (response.Name == this.Name)
                {
                    count++;
                }

                _votes = count;
            } 
            return count;
        }
        public virtual void Print()
        {
            Console.WriteLine($"Наименование: {Name}, количество голосов: {Votes}");
        }
    }

    public class HumanResponse : Response
    {
        private string _surname;
        public string Surname => _surname;
        public HumanResponse(string name, string surname) : base(name)
        {
            _surname = surname;
        }

        public override int CountVotes(Response[] responses)
        {
            if (responses == null || responses.Length == 0) return 0;
            int count = 0;
            foreach (var response in responses)
            {
                HumanResponse humanResponse = response as HumanResponse;
                if (humanResponse == null) continue;
                if (humanResponse.Name == this.Name && humanResponse.Surname == this.Surname)
                    _votes++;
            }

            return _votes;
        }

        public override void Print()
        {
            Console.WriteLine($"Имя: {Name}, Фамилия: {Surname}, Количество голосов: {Votes}");
        }
    }
}