using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodoku
{
    class Square
    {
        private List<int> Possibilities;
        private int Certainty;

        public Square(int Certain = 0)
        {
            Certainty = Certain;
            Possibilities = new List<int>();
            AddAll();
        }

        public void AddAll()
        {
            for(int i = 0; i < 9;)
                Possibilities.Add(++i);
        }

        public void AddPossibilities(int Possibility)
        {
            Possibilities.Add(Possibility);
        }

        public void RemovePossibilities(int Possibility)
        {
            Possibilities.Remove(Possibility);
        }

        public void SetCertainty(int Certain)
        {
            Certainty = Certain;
            Possibilities.Clear();
        }

        public int GetCertainty()
        {
            return Certainty;
        }

        public List<int> GetPossibilities()
        {
            return Possibilities;
        }
    }
}
